<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcMarket.BackEnd.Category>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>مدیریت : ویرایش
        <%= Model.Name %></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        ویرایش
        <%= Model.Name %></h1>
    <% using (Html.BeginForm("Category", "Admin", FormMethod.Post))
       { %>
    <%= Html.Hidden("CategoryId") %>
    <p>
        نام:
        <%= Html.TextBox("Name") %>
        <div>
            <%= Html.ValidationMessage("Name") %></div>
    </p>
    <input type="submit" value="ثبت" />
    or
    <%=Html.ActionLink("لفو تغییرات و بازگشت به لیست دسته ها", "Categories") %>
    <% } %>
</asp:Content>
