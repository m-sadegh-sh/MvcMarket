<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MvcMarket.BackEnd.Cart>>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>مدیریت : اعتبارات</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        اعتبارات</h1>
    <table class="Grid">
        <tr>
            <th>
                شناسه سبد خرید
            </th>
            <th>
                نام خریدار
            </th>
            <th>
                تاریخ خرید
            </th>
            <th>
                وضعیت
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= item.CartId %>
            </td>
            <td>
                <%= item.User.UserName %>
            </td>
            <td>
                <%= item.ShippedOn.ToLongDateString() %>
            </td>
            <td>
                <%= item.Status %>
            </td>
            <td>
                <%= Html.ActionLink("ویرایش", "UCart", new {item.CartId}) %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
