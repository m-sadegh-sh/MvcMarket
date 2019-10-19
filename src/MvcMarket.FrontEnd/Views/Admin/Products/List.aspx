<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MvcMarket.BackEnd.Product>>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>مدیریت : محصولات موجود</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        محصولات موجود</h1>
    <table class="Grid">
        <tr>
            <th>
                نام
            </th>
            <th class="NumericCol">
                قیمت
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= item.Name %>
            </td>
            <td class="NumericCol">
                <%= item.Price.ToString("c") %>
            </td>
            <td>
                <%= Html.ActionLink("ویرایش", "Product", new {item.ProductId}) %>
                <%= Html.ActionLink("حذف", "DeleteProduct", new {item.ProductId})%>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%= Html.ActionLink("ثبت محصول جدید", "CreateProduct")%></p>
</asp:Content>
