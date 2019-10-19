<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>ورود</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        ورود</h1>
    <% if ((bool?)ViewData["lastLoginFailed"] == true)
       { %>
    <div class="Message">
        در ورود شما به سیستم کاربری مشکلی رخ داده است. لطفا دوباره تلاش کنید.
    </div>
    <% } %>    
    <% using (Html.BeginForm())
       { %>
    <div>
        نام کاربری:
        <%= Html.TextBox("username") %></div>
    <div>
        کلمه عبور:
        <%= Html.Password("password") %></div>
    <p>
        <input type="submit" value="ورود" /></p>
    <% } %>
</asp:Content>
