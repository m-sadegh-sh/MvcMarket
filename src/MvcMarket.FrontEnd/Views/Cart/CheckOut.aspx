<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcMarket.BackEnd.ShippingDetails>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>ام وی سی مارکت : اتمام خرید</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        اتمام خرید</h2>
    مشتری گرامی در صورت اطمینان از محصولات خریداری شده بر روی دکمه ثبت سفارش کلیک کنید تا همکاران ما در اسرع وقت نصبت به ارسال سبد خرید شما اقدام نمایند.
    (استاد بعد از ثبت سفارش در صورتی که تنظیمات SMTP Server رو درست ست کرده باشین ایمیلی
    به خریدار ارسال می شه. معمولا تو سیستم لوکال امکانش نیست و تنظیمات بسیاری برای این
    کار می طلبد.)
    <%= Html.ValidationSummary() %>
    <% using (Html.BeginForm())
       { %>
    <h3>
        ارسال به:</h3>
    <div>
        نام:
        <%= Html.DisplayFor(sd => sd.Name) %></div>
    <h3>
        آدرس:</h3>
    <div>
        <%= Html.DisplayFor(sd => sd.Address) %></div>
    <p align="center">
        <input type="submit" value="ثبت سفارش" /></p>
    <% } %>
</asp:Content>
