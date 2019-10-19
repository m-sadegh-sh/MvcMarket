<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcMarket.FrontEnd.Models.Cart>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>ام وی سی مارکت : سبد خرید شما</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        سبد خرید شما</h2>
    <table width="90%" align="center">
        <thead>
            <tr>
                <th align="center">
                    تعداد
                </th>
                <th align="right">
                    محصول
                </th>
                <th align="left">
                    قیمت واحد
                </th>
                <th align="left">
                    قیمت کل
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var line in Model.Lines)
               { %>
            <tr>
                <td align="center">
                    <%= line.Quantity %>
                </td>
                <td align="right">
                    <%= line.Product.Name %>
                </td>
                <td align="left">
                    <%= line.Product.Price.ToString("c") %>
                </td>
                <td align="left">
                    <%= (line.Quantity * line.Product.Price).ToString("c") %>
                </td>
                <td>
                    <% using (Html.BeginForm("RemoveFromCart", "Cart"))
                       { %>
                    <%= Html.Hidden("ProductID", line.Product.ProductId) %>
                    <%= Html.Hidden("returnUrl", ViewData["returnUrl"]) %>
                    <input type="submit" value="حذف از سبد" />
                    <% } %>
                </td>
            </tr>
            <% } %>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="3" align="right">
                    مجموع کل:
                </td>
                <td align="right">
                    <%= Model.ComputeTotalValue().ToString("c") %>
                </td>
            </tr>
        </tfoot>
    </table>
    <p align="center" class="actionButtons">
        <a href="<%= Html.Encode(ViewData["returnUrl"]) %>">ادامه خرید</a>
        <%= Html.ActionLink("ثبت سبد خرید", "CheckOut") %>
    </p>
</asp:Content>
