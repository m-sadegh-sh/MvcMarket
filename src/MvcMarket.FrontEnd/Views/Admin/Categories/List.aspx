<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MvcMarket.BackEnd.Category>>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>مدیریت : دسته بندی</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        دسته های نوجود</h1>
    <table class="Grid">
        <tr>
            <th>
                نام
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
            <td>
                <%= Html.ActionLink("ویرایش", "Category", new {item.CategoryId}) %>
                <%= Html.ActionLink("حذف", "DeleteCategory", new {item.CategoryId})%>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%= Html.ActionLink("ثبت دسته جدید", "CreateCategory")%></p>
</asp:Content>
