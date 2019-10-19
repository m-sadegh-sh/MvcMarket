<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcMarket.BackEnd.Credit>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>مدیریت : ویرایش
        <%= Model.CreditId %></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        ویرایش
        <%= Model.CreditId %></h1>
    <% using (Html.BeginForm("Credit", "Admin", FormMethod.Post))
       { %>
    <%= Html.Hidden("CreditId") %>
    <p>
        مقدار:
        <%= Html.TextBox("Amount") %>
        <div>
            <%= Html.ValidationMessage("Amount") %></div>
    </p>
    <p>
         فروش رفته:
        <%= Html.CheckBox("Expired") %>
        <div>
            <%= Html.ValidationMessage("Expired") %></div>
    </p>
    <input type="submit" value="ثبت" />
    or
    <%=Html.ActionLink("لفو تغییرات و بازگشت به لیست اعتبارات", "Credits") %>
    <% } %>
</asp:Content>
