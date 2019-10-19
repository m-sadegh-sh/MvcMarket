namespace MvcMarket.FrontEnd.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class ProductsController : MvcMarketControllerBase
    {
        public int PageSize = 4;

        public ViewResult List(string category, int page)
        {
            var products = (from prod in MvcMarketDataContext.Products
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
                               });

            if (!string.IsNullOrWhiteSpace(category))
                products = products.Where(prods => prods.Category == category);

            var numProducts = products.Count();
            ViewData["TotalPages"] = (int)Math.Ceiling((double)numProducts / PageSize);
            ViewData["CurrentPage"] = page;
            ViewData["CurrentCategory"] = category;

            return View(products
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize)
                            .ToList()
                );
        }

        public ActionResult GetImage(Guid productId)
        {
            var product = (from p in MvcMarketDataContext.Products
                           where p.ProductId == productId
                           select p).FirstOrDefault();
            if (product != null)
                return File(product.ImageData.ToArray(), product.ImageMimeType);
            return Content("عکسی یافت نشد!");
        }
    }
}