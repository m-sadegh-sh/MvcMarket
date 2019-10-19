namespace MvcMarket.BackEnd
{
    using System.Web.Routing;

    public class NavLink
    {
        public string Text { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
        public bool IsSelected { get; set; }
    }
}