<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>ثبت اعتبار</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        ثبت اعتبار
    </h1>
    <p>
        کاربر گرامی، در حال حاضر موجودی حساب شما
        <%: ViewData["CurrentAmount"]%>
        می باشد.</p>
    <% using (Html.BeginForm("SubmitCredit", "Cart", FormMethod.Post))
       { %>
    <p>
        شماره اعتبار:
        <%= Html.TextBox("CreditId")%>
    </p>
    <input type="submit" value="ثبت" />
    or
    <%=Html.ActionLink("ثبت سبد خرید", "CheckOut") %>
    <% } %>
</asp:Content>
