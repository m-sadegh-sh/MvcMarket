<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcMarket.BackEnd.Cart>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>مدیریت : ویرایش
        <%= Model.CartId %></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        ویرایش
        <%= Model.CartId %></h1>
    <% using (Html.BeginForm("UCart", "Admin", FormMethod.Post))
       { %>
    <%= Html.Hidden("CartId") %>
    <p>
        تاریخ:
        <%= Html.DisplayFor(c=>c.ShippedOn) %>
    </p>
    <p>
        وضعیت:
        <%= Html.DropDownList("Status", new List<SelectListItem> {new SelectListItem { Text="در حال اقدام", Value="0" },
                                                                  new SelectListItem { Text="در حال ارسال", Value="1" },
                                                                  new SelectListItem { Text="ارسال شده", Value="2" }})%>
    </p>
    <input type="submit" value="ثبت" />
    or
    <%=Html.ActionLink("لفو تغییرات و بازگشت به لیست سبدهای خرید", "Carts") %>
    <% } %>
</asp:Content>
