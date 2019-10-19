<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>ثبت کاربر جدید</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        ثبت کاربر جدید</h1>
    <% if ((bool?)ViewData["lastSignUpFailed"] == true)
       { %>
    <div class="Message">
        در ثبت کاربر جدید مشکلی رخ داده است. لطفا دوباره تلاش کنید.
    </div>
    <% } %>
    <% using (Html.BeginForm())
       { %>
    <div>
        نام کاربری:
        <%= Html.TextBox("username") %>
    </div>
    <div>
        کلمه عبور:
        <%= Html.Password("password") %>
    </div>
    <div>
        ایمیل:
        <%= Html.TextBox("email") %>
    </div>
    <p>
        <input type="submit" value="ادامه" /></p>
    <% } %>
</asp:Content>
