namespace MvcMarket.FrontEnd.Controllers
{
    using System.Web.Mvc;
    using BackEnd;
    using System;
    using System.Web.Security;

    public class MvcMarketControllerBase : Controller
    {
        private const string CartSessionKey = "cartKey";

        public Guid UserId
        {
            get
            {
                if (Request.IsAuthenticated)
                {
                    var userId = (Guid)Membership.GetUser(ControllerContext.HttpContext.User.Identity.Name).ProviderUserKey;
                    return userId;
                }
                else
                    return Guid.Empty;
            }
        }

        private MvcMarketDataContext _mvcMarketDataContext;
        public MvcMarketDataContext MvcMarketDataContext
        {
            get { return _mvcMarketDataContext ?? (_mvcMarketDataContext = new MvcMarketDataContext()); }
        }

        public FrontEnd.Models.Cart Cart
        {
            get
            {
                var cart = (FrontEnd.Models.Cart)ControllerContext.HttpContext.Session[CartSessionKey];
                if (cart == null)
                {
                    cart = new FrontEnd.Models.Cart();
                    ControllerContext.HttpContext.Session[CartSessionKey] = cart;
                }
                return cart;
            }
        }

    }
}
