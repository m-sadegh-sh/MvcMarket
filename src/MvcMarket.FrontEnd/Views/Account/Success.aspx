<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Success</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        ورود</h1>
    <div class="Message">
        کاربر جدید با موفقیت به ثبت رسید.
    </div>
    <% Html.RouteLink("Home", "GetProducts"); %>
</asp:Content>
