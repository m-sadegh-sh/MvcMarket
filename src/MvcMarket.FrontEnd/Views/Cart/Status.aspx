<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>ام وی سی مارکت : وضعیت سفارش</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        وضعیت سفارش</h2>
    برای مشاهده وضعیت سفارش، شناسه سبد خرید را در قیلد زیر وارد کرده و دکمه جستجو را
    کلیک کنید..
    <% using (Html.BeginForm("Status", "Cart", FormMethod.Post))
       { %>
    <p>
        <%= Html.TextBox("CartId") %></p>
    <p>
        <input type="submit" value="جستجو" /></p>
    <%} %>
</asp:Content>
