namespace MvcMarket.BackEnd
{
    using System.Web.Routing;

    public class CategoryLink : NavLink
    {
        public CategoryLink(string category)
        {
            Text = category ?? "خانه";
            RouteValues = new RouteValueDictionary(new
            {
                controller = "Products",
                action = "List",
                category,
                page = 1
            });
        }
    }
}