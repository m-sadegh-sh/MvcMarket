namespace MvcMarket.FrontEnd.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using BackEnd;
    using FrontEnd.Models;

    public class CartController : MvcMarketControllerBase
    {
        private static readonly EmailOrderSubmitter OrderSubmitter = new EmailOrderSubmitter("smtp.mvcmarket.net:2510", "noreply@mvcmarket.net", "someone@gmail.com");

        public RedirectToRouteResult AddToCart(Guid productId, string returnUrl)
        {
            var product = MvcMarketDataContext.Products
                .FirstOrDefault(p => p.ProductId == productId);
            Cart.AddItem(product, 1);
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Guid productId, string returnUrl)
        {
            var product = MvcMarketDataContext.Products
                .FirstOrDefault(p => p.ProductId == productId);
            Cart.RemoveLine(product);
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            ViewData["CurrentCategory"] = "سبد خرید";
            return View(Cart);
        }

        public ViewResult Summary()
        {
            return View(Cart);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CheckOut()
        {
            if (Request.IsAuthenticated)
            {
                var shippingDetails = Cart.ShippingDetails(UserId);
                if (shippingDetails != null)
                    return View(shippingDetails);
                else
                    return RedirectToAction("ShippingDetails", "Account", new { returnUrl = Url.Action("CheckOut", "Cart", null) });
            }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("CheckOut", "Cart", null) });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CheckOut(FormCollection form)
        {
            if (Cart.Lines.Count == 0)
            {
                ModelState.AddModelError("Cart", "سبد خرید خالیست.");
                return View();
            }
            try
            {
                var ca = MvcMarketDataContext.ShippingDetails.FirstOrDefault(sd => sd.UserId == UserId).CreditAmount;
                if (ca < Cart.ComputeTotalValue())
                    return RedirectToAction("BuyCredit", new { returnUrl = Url.Action("CheckOut") });
                OrderSubmitter.SubmitOrder(Cart, UserId);
                ViewData["CartId"] = Cart.Submit(UserId);
                Cart.Clear();
                return View("Completed");
            }
            catch
            {
                return View("Failed");
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult BuyCredit()
        {
            if (Request.IsAuthenticated)
            {
                var shippingDetails = Cart.ShippingDetails(UserId);
                if (shippingDetails != null)
                {
                    ViewData["CurrentAmount"] = shippingDetails.CreditAmount;
                    return View();
                }
                else
                    return RedirectToAction("ShippingDetails", "Account", new { returnUrl = Url.Action("CheckOut", "Cart", null) });
            }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("CheckOut", "Cart", null) });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BuyCredit(string accountId, string password, string amount)
        {
            var credit = MvcMarketDataContext.Credits.FirstOrDefault(c => c.Amount == Convert.ToDecimal(amount) && c.Expired == false);
            if (credit != null)
            {
                TempData["message"] = string.Format("کاربر گرامی با وارد کردن کد {0} در بخش ثبت اعتبار، اعتبار خریداری شده توسط شما به حسابتان واریز خواهد شد.", credit.CreditId);
                return View();
            }
            TempData["message"] = "در حال حاضر اعتباری با مبلغ مذکور در صف اعتبارات ثبت شده توسط مدیر سایت وجود ندارد. شما می توانید اعتباری با مبلغ متفاوت را درخواست کنید.";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SubmitCredit()
        {
            if (Request.IsAuthenticated)
            {
                var shippingDetails = MvcMarketDataContext.ShippingDetails.FirstOrDefault(sd => sd.UserId == UserId);
                if (shippingDetails != null)
                {
                    ViewData["CurrentAmount"] = shippingDetails.CreditAmount;
                    return View();
                }
                else
                    return RedirectToAction("ShippingDetails", "Account", new { returnUrl = Url.Action("CheckOut", "Cart", null) });
            }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("CheckOut", "Cart", null) });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitCredit(Guid creditId)
        {
            var credit = MvcMarketDataContext.Credits.FirstOrDefault(c => c.CreditId == creditId && c.Expired == false);
            var shippingDetails = MvcMarketDataContext.ShippingDetails.FirstOrDefault(sd => sd.UserId == UserId);
            if (credit != null)
            {
                shippingDetails.CreditAmount += credit.Amount;
                credit.Expired = true;
                MvcMarketDataContext.SubmitChanges();
                ViewData["CurrentAmount"] = shippingDetails.CreditAmount;
                TempData["message"] = string.Format("کاربر گرامی مبلغ {0} دلار به حساب شما واریز شد.", credit.Amount);
                return View();
            }
            ViewData["CurrentAmount"] = shippingDetails.CreditAmount;
            TempData["message"] = "اعتباری با شماره مذکور یافت نشد.";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Status()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Status(Guid CartId)
        {
            var cart = MvcMarketDataContext.Carts.FirstOrDefault(c => c.CartId == CartId);
            if (cart != null)
            {
                TempData["message"] = string.Format("سبد خرید به شناسه {0} در وضعیت <b>{1}</b> قرار دارد.", cart.CartId, StatusDesc(cart.Status));
                return View();
            }
            TempData["message"] = "اعتباری با شماره مذکور یافت نشد.";
            return View();
        }

        private string StatusDesc(short p)
        {
            switch (p)
            {
                case 0:
                    return "در حال اقدام";
                case 1:
                    return "در حال ارسال";
                case 2:
                    return "ارسال شده";
                default:
                    return "نامعلوم";
            }
        }
    }
}