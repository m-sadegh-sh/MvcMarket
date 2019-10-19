namespace MvcMarket.FrontEnd.Controllers {
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;

    using BackEnd;

    public class AccountController : MvcMarketControllerBase {
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Login() {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string username, string password, string returnUrl) {
            if (Membership.ValidateUser(username, password)) {
                FormsAuthentication.SetAuthCookie(username, false);
                //Cart.Clear();
                if (returnUrl != null)
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("List", "Products");
            }
            ViewData["lastLoginFailed"] = true;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult LogOut(string returnUrl) {
            if (Request.IsAuthenticated) {
                FormsAuthentication.SignOut();
                Cart.Clear();
            }
            if (returnUrl != null)
                return Redirect(returnUrl);
            else
                return RedirectToAction("List", "Products");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SignUp() {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SignUp(string username, string password, string email, string returnUrl) {
            var status=MembershipCreateStatus.Success;
            try {
                var membership = Membership.CreateUser(username, password, email, "sss", "qqq", true, out status);
            } catch {}
            if (status == MembershipCreateStatus.Success) {
                FormsAuthentication.SetAuthCookie(username, true);
                return RedirectToAction("ShippingDetails");
            }
            ViewData["lastSignUpFailed"] = true;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get), Authorize]
        public ActionResult ShippingDetails() {
            var userId = UserId;
            var shippingDetails = MvcMarketDataContext.ShippingDetails.FirstOrDefault(sd => sd.UserId == userId);
            if (shippingDetails == null)
                shippingDetails = new ShippingDetails {UserId = userId};
            return View(shippingDetails);
        }

        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult ShippingDetails(ShippingDetails shippingDetails, string returnUrl) {
            if (ModelState.IsValid) {
                try {
                    if (MvcMarketDataContext.ShippingDetails.FirstOrDefault(sd => sd.UserId == UserId) == null) {
                        shippingDetails.UserId = UserId;
                        MvcMarketDataContext.ShippingDetails.InsertOnSubmit(shippingDetails);
                        MvcMarketDataContext.SubmitChanges();
                    } else {
                        var shD = MvcMarketDataContext.ShippingDetails.FirstOrDefault(sd => sd.UserId == UserId);
                        //Update
                        MvcMarketDataContext.SubmitChanges();
                    }
                    if (returnUrl != null)
                        return Redirect(returnUrl);
                    else
                        return View("Success");
                } catch {
                    ViewData["lastOperationFailed"] = true;
                    return View();
                }
            } else {
                ViewData["lastOperationFailed"] = true;
                return View();
            }
        }
    }
}