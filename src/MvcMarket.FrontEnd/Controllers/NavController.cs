namespace MvcMarket.FrontEnd.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using BackEnd;
    using System.Linq;
    using System.Web.Routing;

    public class NavController : MvcMarketControllerBase
    {
        public ViewResult Menu(string highlightCategory, bool right = true)
        {
            var navLinks = new List<NavLink>();
            if (right)
            {

                navLinks.Add(new CategoryLink(null)
                              {
                                  IsSelected = (highlightCategory == null)
                              });

                if (string.IsNullOrWhiteSpace(highlightCategory))
                    highlightCategory = "Home";
                navLinks.Add(new NavLink
                                {
                                    Text = "وضعیت سبد خرید",
                                    RouteValues = new RouteValueDictionary { { "Controller", "Cart" }, { "Action", "Status" } },
                                    IsSelected = (string.Compare(ControllerContext.RouteData.Values["action"] as string, "status", true) == 0)
                                });
                if (Request.IsAuthenticated && ControllerContext.HttpContext.User.Identity.Name == "admin")
                {
                    navLinks.Add(new NavLink
                                {
                                    Text = "کاربران",
                                    RouteValues = new RouteValueDictionary { { "Controller", "Admin" }, { "Action", "Users" } },
                                    IsSelected = (string.Compare(ControllerContext.RouteData.Values["action"] as string, "users", true) == 0)
                                });
                    navLinks.Add(new NavLink
                                {
                                    Text = "محصولات",
                                    RouteValues = new RouteValueDictionary { { "Controller", "Admin" }, { "Action", "Products" } },
                                    IsSelected = (string.Compare(ControllerContext.RouteData.Values["action"] as string, "Products", true) == 0)
                                });
                    navLinks.Add(new NavLink
                                {
                                    Text = "دسته بندی ها",
                                    RouteValues = new RouteValueDictionary { { "Controller", "Admin" }, { "Action", "Categories" } },
                                    IsSelected = (string.Compare(ControllerContext.RouteData.Values["action"] as string, "Categories", true) == 0)
                                });
                    navLinks.Add(new NavLink
                                {
                                    Text = "اعتبارات",
                                    RouteValues = new RouteValueDictionary { { "Controller", "Admin" }, { "Action", "Credits" } },
                                    IsSelected = (string.Compare(ControllerContext.RouteData.Values["action"] as string, "Credits", true) == 0)
                                });
                    navLinks.Add(new NavLink
                                {
                                    Text = "سبدهای خرید",
                                    RouteValues = new RouteValueDictionary { { "Controller", "Admin" }, { "Action", "Carts" } },
                                    IsSelected = (string.Compare(ControllerContext.RouteData.Values["action"] as string, "Carts", true) == 0)
                                });
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(highlightCategory))
                    highlightCategory = "Home";
                var categories = MvcMarketDataContext.Categories.Select(x => x.Name);
                navLinks.AddRange(categories.Distinct().OrderBy(x => x).Select(category => new CategoryLink(category)
                                                                                               {
                                                                                                   IsSelected =
                                                                                                       (category ==
                                                                                                        highlightCategory)
                                                                                               }));
            }
            return View(navLinks);
        }
    }
}