<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MvcMarket.BackEnd.User>>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>مدیریت : کاربران</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        کاربران موجود</h1>
    <table class="Grid">
        <tr>
            <th>
                نام کاربری
            </th>
            <th>
                آخرین تاریخ فعالیت
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= item.UserName %>
            </td>
            <td>
                <%= item.LastActivityDate.ToLongDateString() %>
            </td>
            <td>
                <%= Html.ActionLink("ویرایش", "User", new {item.UserId}) %>
                <%= Html.ActionLink("حذف", "DeleteUser", new {item.UserId})%>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
