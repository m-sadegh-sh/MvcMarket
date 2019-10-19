<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>خرید اعتبار</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        خرید اعتبار
    </h1>
    <p>
        کاربر گرامی، در حال حاضر موجودی حساب شما
        <%: ViewData["CurrentAmount"]%>
        می باشد.</p>
    <% using (Html.BeginForm("BuyCredit", "Cart", FormMethod.Post))
       { %>
    <p>
        شماره حساب:
        <%= Html.TextBox("AccountId")%>
    </p>
    <p>
        رمز کارت اعتباری:
        <%= Html.TextBox("Password")%>
    </p>
    <p>
        مقدار اعتبار:
        <%= Html.DropDownList("Amount", new List<SelectListItem> {new SelectListItem { Text="5000" },
                                                                  new SelectListItem { Text="1000" },
                                                                  new SelectListItem { Text="2500" },
                                                                  new SelectListItem { Text="50000" }})%>
    </p>
    <input type="submit" value="خرید" />
    or
    <%=Html.ActionLink("ثبت اعتبار", "SubmitCredit") %>
    <% } %>
</asp:Content>
