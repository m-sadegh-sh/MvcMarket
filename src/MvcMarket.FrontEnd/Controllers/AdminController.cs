namespace MvcMarket.FrontEnd.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using BackEnd;
    using System;
    using System.Web.Security;
    using System.Data.Linq;

    [Authorize(Users = "admin")]
    public class AdminController : MvcMarketControllerBase
    {

        public ViewResult Users()
        {
            return View("Users/List", MvcMarketDataContext.Users.Where(user => user.UserName != "admin").ToList());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditUser(Guid userId)
        {
            MembershipUser user = null;
            if (userId != null && userId != Guid.Empty)
                user = Membership.GetUser(userId);
            if (user == null)
                return Content("User not found");

            return View("Users/Edit", user);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditUser(Guid userId, string email, bool isApproved)
        {
            MembershipUser user = null;
            if (userId != null && userId != Guid.Empty)
                user = Membership.GetUser(userId);
            if (user == null)
                return Content("User not found");
            if (ModelState.IsValid)
            {
                user.Email = email;
                user.IsApproved = isApproved;
                Membership.UpdateUser(user);
                TempData["message"] = user.UserName + " با موفقیت ذخیره شد.";
                return RedirectToAction("Users");
            }
            return View("Users/Edit", user);
        }

        public ActionResult DeleteUser(Guid userId)
        {
            MembershipUser user = null;
            if (userId != null && userId != Guid.Empty)
                user = Membership.GetUser(userId);
            if (user == null)
                return Content("User not found");

            var userCarts = MvcMarketDataContext.Carts.Where(c => c.UserId == userId);

            if (userCarts != null)
            {
                foreach (var cart in userCarts)
                    MvcMarketDataContext.CartLines.DeleteAllOnSubmit(MvcMarketDataContext.CartLines.Where(cl => cl.CartId == cart.CartId));
                MvcMarketDataContext.Carts.DeleteAllOnSubmit(userCarts);
            }

            var shD = MvcMarketDataContext.ShippingDetails.FirstOrDefault(sd => sd.UserId == userId);
            if (shD != null)
            {
                MvcMarketDataContext.ShippingDetails.DeleteOnSubmit(shD);
                MvcMarketDataContext.SubmitChanges();
            }
            Membership.DeleteUser(user.UserName, true);
            TempData["message"] = user.UserName + " با موفقیت حذف شد.";

            return RedirectToAction("Users");
        }

        public ViewResult Products()
        {
            return View("Products/List", (from prod in MvcMarketDataContext.Products
                                          join catprod in MvcMarketDataContext.CategoriezedProducts on prod.ProductId equals
                                              catprod.ProductId
                                          join cat in MvcMarketDataContext.Categories on catprod.CategoryId equals cat.CategoryId
                                          select
                                              new ProductPlus
                                                  {
                                                      ProductId = prod.ProductId,
                                                      Name = prod.Name,
                                                      Description = prod.Description,
                                                      Price = prod.Price,
                                                      ImageData = prod.ImageData,
                                                      ImageMimeType = prod.ImageMimeType,
                                                      Category = cat.Name
                                                  }).ToList());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Product(Guid productId)
        {
            Product product = null;
            if (productId != null && productId != Guid.Empty)
            {
                product = ((from prod in MvcMarketDataContext.Products
                            select prod).Where((prod => prod.ProductId == productId))).FirstOrDefault();
            }
            if (product == null)
                product = new Product();
            var categories = (from cats in MvcMarketDataContext.Categories
                              select cats);
            var selectedCat = MvcMarketDataContext.Categories.Where(cat => cat.CategoryId == (from catprod in MvcMarketDataContext.CategoriezedProducts
                                                                                              where catprod.ProductId == product.ProductId
                                                                                              select catprod).First().CategoryId)
                                                                                              .Select(c => new { c.Name, c.CategoryId }).FirstOrDefault();
            ViewData["Categories"] = new SelectList(categories, "CategoryId", "Name", selectedCat);
            return View("Products/Edit", product);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Product(Guid productId, string name, string description, decimal price, string[] categories, HttpPostedFileBase image)
        {
            Product product = null;
            var isNew = false;
            if (productId != null && productId != Guid.Empty)
                product = MvcMarketDataContext.Products.FirstOrDefault(pr => pr.ProductId == productId);
            if (product == null)
            {
                isNew = true;
                product = new Product();
            }
            if (ModelState.IsValid)
            {
                Guid oldCatId = Guid.Empty;
                if (!isNew)
                    oldCatId = (from prod in MvcMarketDataContext.Products
                                join catprod in MvcMarketDataContext.CategoriezedProducts on prod.ProductId equals
                                    catprod.ProductId
                                join cat in MvcMarketDataContext.Categories on catprod.CategoryId equals cat.CategoryId
                                where prod.Name == name
                                select cat.CategoryId).FirstOrDefault();
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new Binary(new byte[image.ContentLength]);
                    image.InputStream.Read(product.ImageData.ToArray(), 0, image.ContentLength);
                }
                product.Name = name;
                product.Description = description;
                product.Price = price;
                var catId = Guid.Parse(categories[0]);
                if (!isNew && catId != oldCatId)
                {
                    var cp1 =
                        MvcMarketDataContext.CategoriezedProducts.FirstOrDefault(
                            cp => cp.CategoryId == oldCatId && cp.ProductId == productId);
                    if (cp1 != null)
                        MvcMarketDataContext.CategoriezedProducts.DeleteOnSubmit(cp1);
                }
                if (isNew)
                {
                    product.ProductId = Guid.NewGuid();
                    MvcMarketDataContext.Products.InsertOnSubmit(product);
                }
                if (MvcMarketDataContext.CategoriezedProducts.FirstOrDefault(c => c.ProductId == productId &&
                    c.CategoryId == catId) == null)
                    MvcMarketDataContext.CategoriezedProducts.InsertOnSubmit(new CategoriezedProduct { ProductId = product.ProductId, CategoryId = catId });
                MvcMarketDataContext.SubmitChanges();
                TempData["message"] = product.Name + " با موفقیت ذخیره شد.";
                return RedirectToAction("Products");
            }
            return View("Products/Edit", product);
        }

        public ActionResult CreateProduct()
        {
            return RedirectToAction("Product", new { productId = Guid.Empty });
        }

        public RedirectToRouteResult DeleteProduct(Guid productId)
        {
            var product = (from p in MvcMarketDataContext.Products
                           where p.ProductId == productId
                           select p).FirstOrDefault();
            if (product != null)
            {
                var catsOfProd = MvcMarketDataContext.CategoriezedProducts.Where(catprod => catprod.ProductId == productId);
                MvcMarketDataContext.CategoriezedProducts.DeleteAllOnSubmit(catsOfProd);
                var prodOnCartLines = MvcMarketDataContext.CartLines.Where(cl => cl.ProductId == productId);
                MvcMarketDataContext.CartLines.DeleteAllOnSubmit(prodOnCartLines);
                MvcMarketDataContext.Products.DeleteOnSubmit(product);
                MvcMarketDataContext.SubmitChanges();
                TempData["message"] = product.Name + " با موفقیت حذف شد.";
            }
            TempData["message"] = product.Name + " با موفقیت حذف نشد.";
            return RedirectToAction("Products");
        }

        public ViewResult Categories()
        {
            return View("Categories/List", (from cats in MvcMarketDataContext.Categories
                                            select cats).ToList());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Category(Guid categoryId)
        {
            var category = ((from cats in MvcMarketDataContext.Categories
                             select cats).Where((cat => cat.CategoryId == categoryId))).First();
            return View("Categories/Edit", category);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Category(Guid categoryId, string name)
        {
            var category = categoryId == Guid.Empty ? new Category() : MvcMarketDataContext.Categories.First(cat => cat.CategoryId == categoryId);
            if (ModelState.IsValid)
            {
                category.Name = name;
                if (category.CategoryId == Guid.Empty)
                {
                    category.CategoryId = Guid.NewGuid();
                    MvcMarketDataContext.Categories.InsertOnSubmit(category);
                }
                MvcMarketDataContext.SubmitChanges();
                TempData["message"] = category.Name + " با موفقیت ذخیره شد.";
                return RedirectToAction("Categories");
            }
            return View("Categories/Edit", category);
        }

        public ViewResult CreateCategory()
        {
            return View("Categories/Edit", new Category());
        }

        public RedirectToRouteResult DeleteCategory(Guid categoryId)
        {
            var category = (from cat in MvcMarketDataContext.Categories
                            where cat.CategoryId == categoryId
                            select cat).First();
            var catsOfProd = MvcMarketDataContext.CategoriezedProducts.Where(catprod => catprod.CategoryId == categoryId);
            MvcMarketDataContext.CategoriezedProducts.DeleteAllOnSubmit(catsOfProd);
            MvcMarketDataContext.Categories.DeleteOnSubmit(category);
            MvcMarketDataContext.SubmitChanges();
            TempData["message"] = category.Name + " با موفقیت حذف شد.";
            return RedirectToAction("Categories");
        }

        public ViewResult Credits()
        {
            return View("Credits/List", (from cred in MvcMarketDataContext.Credits
                                         select cred).ToList());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Credit(Guid creditId)
        {
            var credit = ((from creds in MvcMarketDataContext.Credits
                           select creds).Where((cat => cat.CreditId == creditId))).FirstOrDefault();
            return View("Credits/Edit", credit);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Credit(Guid creditId, decimal amount, bool expired)
        {
            var credit = creditId == Guid.Empty ? new Credit() : MvcMarketDataContext.Credits.FirstOrDefault(cat => cat.CreditId == creditId);
            if (ModelState.IsValid)
            {
                credit.Amount = amount;
                credit.Expired = expired;
                if (credit.CreditId == Guid.Empty)
                {
                    credit.CreditId = Guid.NewGuid();
                    MvcMarketDataContext.Credits.InsertOnSubmit(credit);
                }
                MvcMarketDataContext.SubmitChanges();
                TempData["message"] = credit.CreditId + " با موفقیت ذخیره شد.";
                return RedirectToAction("Credits");
            }
            return View("Credits/Edit", credit);
        }

        public ViewResult CreateCredit()
        {
            return View("Credits/Edit", new Credit());
        }

        public RedirectToRouteResult DeleteCredit(Guid creditId)
        {
            var credit = (from cat in MvcMarketDataContext.Credits
                          where cat.CreditId == creditId
                          select cat).FirstOrDefault();
            MvcMarketDataContext.Credits.DeleteOnSubmit(credit);
            MvcMarketDataContext.SubmitChanges();
            TempData["message"] = credit.CreditId + " با موفقیت حذف شد.";
            return RedirectToAction("Credits");
        }

        public ViewResult Carts()
        {
            return View("Carts/List", (from cred in MvcMarketDataContext.Carts
                                       select cred).ToList());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult UCart(Guid cartId)
        {
            var cart = ((from carts in MvcMarketDataContext.Carts
                         select carts).Where((cat => cat.CartId == cartId))).FirstOrDefault();
            cart.User = MvcMarketDataContext.Users.FirstOrDefault(u => u.UserId == cart.UserId);
            return View("Carts/Edit", cart);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UCart(Guid cartId, short status)
        {
            var cart = MvcMarketDataContext.Carts.FirstOrDefault(cat => cat.CartId == cartId);
            if (cart != null)
            {
                cart.Status = status;
                MvcMarketDataContext.SubmitChanges();
                TempData["message"] = cart.CartId + " با موفقیت ذخیره شد.";
                return RedirectToAction("Carts");
            }
            return View("Carts/Edit", cart);
        }
    }
}