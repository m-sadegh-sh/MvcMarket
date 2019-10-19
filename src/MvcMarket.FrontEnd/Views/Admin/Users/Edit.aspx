<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Security.MembershipUser>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>مدیریت : ویرایش
        <%= Model.UserName %></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        ویرایش
        <%= Model.UserName %></h1>
    <% using (Html.BeginForm("EditUser", "Admin", FormMethod.Post))
       { %>
    <%= Html.Hidden("UserId") %>
    <p>
        ایمیل:
        <%= Html.TextBox("Email") %>
    </p>
    <p>
        وضعیت (قبول شده):
        <%= Html.CheckBox("IsApproved") %>
    </p>
    <input type="submit" value="ثبت" />
    or
    <%=Html.ActionLink("لفو تغییرات و بازگشت به لیست کاربران", "Users") %>
    <% } %>
</asp:Content>
