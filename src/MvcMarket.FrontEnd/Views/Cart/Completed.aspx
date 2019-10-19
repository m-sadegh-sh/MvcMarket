<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>ام وی سی مارکت : سفارش ثبت شد</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        تشکر!</h2>
    از اینکه از ما خرید کرده اید متشکریم.
    <p>کاربر گرامی برای اطلاع از وضعیت سبد خرید خود، از شناسه <%: ViewData["CartId"] %> استفاده کنید. </p>
</asp:Content>
