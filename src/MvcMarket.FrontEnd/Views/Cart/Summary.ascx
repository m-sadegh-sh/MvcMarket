<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MvcMarket.FrontEnd.Models.Cart>" %>
<% if (Model.Lines.Count > 0)
   { %>
<div id="cart">
    <span class="caption"><b>سبد خرید شما:</b>
        <%= Model.Lines.Sum(x => x.Quantity) %>
        محصول,
        <%= Model.ComputeTotalValue().ToString("c") %>
    </span>
    <%= Html.ActionLink("مشاهده سبد خرید", "Index", "Cart", 
            new { returnUrl = Request.Url.PathAndQuery }, null)%>
</div>
<% } %>