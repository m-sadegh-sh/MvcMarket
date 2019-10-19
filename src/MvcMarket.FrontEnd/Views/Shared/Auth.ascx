<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="cart">
    <% if (Request.IsAuthenticated)
       {%>
    <span class="caption"><b>کاربر:</b>
        <%: Context.User.Identity.Name %>
    </span>
    <%= Html.ActionLink("خروج", "LogOut", "Account")%>
    <% }
       else
       { %>
    <span class="caption"><b>کاربر:</b> مهمان </span>
    <%= Html.ActionLink("ورود", "LogIn", "Account")%><%= Html.ActionLink("ثبت نام", "SignUp", "Account")%>
    <%} %>
</div>
