<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MvcMarket.BackEnd.Product>" %>
<div class="item">
    <%--<% if (Model.ImageData != null)
       { %>
    <div style="float: right; margin-left: 20px">
        <img style="height: 96; width: 96px" src="<%= Url.Action("GetImage", "Products", 
                          new { ProductID = Model.ProductId }) %>" />
    </div>
    <% } %>--%>
    <h3>
        <%= Model.Name %></h3>
    <%= Model.Description %>
    <% using (Html.BeginForm("AddToCart", "Cart"))
       { %>
    <%= Html.Hidden("ProductID") %>
    <%= Html.Hidden("returnUrl", 
                        ViewContext.HttpContext.Request.Url.PathAndQuery) %>
    <input type="submit" value="+ اضافه به سبد خرید" />
    <% } %>
    <h4>
        <%= Model.Price.ToString("c")%></h4>
</div>
