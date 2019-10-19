<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MvcMarket.BackEnd.Credit>>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>مدیریت : اعتبارات</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        اعتبارات</h1>
    <table class="Grid">
        <tr>
            <th>
                شماره
            </th>
            <th>
                مقدار
            </th>
            <th>
                فروش رفته
            </th>
            <th>
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= item.CreditId %>
            </td>
            <td>
                <%= item.Amount %>
            </td>
            <td>
                <%= item.Expired %>
            </td>
            <td>
                <%= Html.ActionLink("ویرایش", "Credit", new {item.CreditId}) %>
                <%= Html.ActionLink("حذف", "DeleteCredit", new {item.CreditId})%>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%= Html.ActionLink("ثبت اعتبار جدید", "CreateCredit")%></p>
</asp:Content>
