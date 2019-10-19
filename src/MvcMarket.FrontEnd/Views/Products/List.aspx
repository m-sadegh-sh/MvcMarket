<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MvcMarket.FrontEnd.Controllers.ProductPlus>>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>ام وی سی مارکت :
        <%= string.IsNullOrEmpty((string)ViewData["CurrentCategory"])
	            ? "لیست محصولات موجود"
	            : Html.Encode(ViewData["CurrentCategory"])   
        %>
    </title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <% foreach (var product in Model)
       { %>
    <% Html.RenderPartial("ProductSummary", product); %>
    <% } %>
    <div class="pager">
        صفحه:
        <%= Html.PageLinks((int)ViewData["CurrentPage"],
                           (int)ViewData["TotalPages"],
                           x => Url.Action("List", new { page = x, category = ViewData["CurrentCategory"] })) %>
    </div>
</asp:Content>
