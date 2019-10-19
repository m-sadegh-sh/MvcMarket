namespace MvcMarket.FrontEnd
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class MvcMarket : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("GetProducts",
                            "",
                            new
                                {
                                    controller = "Products",
                                    action = "List",
                                    category = (string)null,
                                    page = 1
                                }
                );

            routes.MapRoute("GetProductsByPage",
                            "{page}",
                            new { controller = "Products", action = "List", category = (string)null },
                            new { page = @"\d+" }
                );

            routes.MapRoute("GetProductsByCategory",
                            "{category}",
                            new { controller = "Products", action = "List", page = 1 }
                );

            routes.MapRoute("GetProductsByCategoryAndPage",
                            "{category}/{page}",
                            new { controller = "Products", action = "List" },
                            new { page = @"\d+" }
                );

            routes.MapRoute("AdminUsers",
                           "admin/users",
                                           new { controller = "Admin", action = "Users" }
                                           );
            routes.MapRoute("AdminUser",
                           "admin/user",
                                           new { controller = "Admin", action = "EditUser" }
                                           );
            routes.MapRoute("AdminUserDel",
                           "admin/user/del",
                                           new { controller = "Admin", action = "DeleteUser" }
                                           );

            routes.MapRoute("AdminProducts",
                           "admin/products",
                                           new { controller = "Admin", action = "Products" }
                                           );

            routes.MapRoute("AdminProductNew",
                           "admin/product/new",
                                           new { controller = "Admin", action = "CreateProduct" }
                                           );

            routes.MapRoute("AdminProduct",
                           "admin/product",
                                           new { controller = "Admin", action = "Product" }
                                           );
            routes.MapRoute("AdminProductDel",
                           "admin/product/del",
                                           new { controller = "Admin", action = "DeleteProduct" }
                                           );

            routes.MapRoute("AdminCategories",
                           "admin/categories",
                                           new { controller = "Admin", action = "Categories" }
                                           );

            routes.MapRoute("AdminCategoryNew",
                           "admin/category/new",
                                           new { controller = "Admin", action = "CreateCategory" }
                                           );

            routes.MapRoute("AdminCategory",
                           "admin/category",
                                           new { controller = "Admin", action = "Category" }
                                           );
            routes.MapRoute("AdminCategoryDel",
                           "admin/category/del",
                                           new { controller = "Admin", action = "DeleteCategory" }
                                           );

            routes.MapRoute("AdminCredits",
                           "admin/credits",
                                           new { controller = "Admin", action = "Credits" }
                                           );

            routes.MapRoute("AdminCreditNew",
                           "admin/credit/new",
                                           new { controller = "Admin", action = "CreateCredit" }
                                           );

            routes.MapRoute("AdminCredit",
                           "admin/credit",
                                           new { controller = "Admin", action = "Credit" }
                                           );
            routes.MapRoute("AdminCreditDel",
                           "admin/credit/del",
                                           new { controller = "Admin", action = "DeleteCredit" }
                                           );

            routes.MapRoute("AdminCarts",
                           "admin/carts",
                                           new { controller = "Admin", action = "Carts" }
                                           );

            routes.MapRoute("AdminCart",
                   "admin/cart",
                                   new { controller = "Admin", action = "Cart" }
                                   );

            routes.MapRoute("RouteByControllerAndAction",
                "{controller}/{action}",
                                new { controller = "Products", action = "List" }
                                );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }
    }
}