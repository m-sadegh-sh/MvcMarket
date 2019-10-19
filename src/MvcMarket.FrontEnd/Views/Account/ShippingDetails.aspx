<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcMarket.BackEnd.ShippingDetails>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>ثبت جزئیات کاربر جدید</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        ثبت جزئیات کاربر جدید</h1>
    <% if ((bool?)ViewData["lastOperationFailed"] == true)
       { %>
    <div class="Message">
        در ثبت اطلاعات مشکلی رخ داده است. لطفا دوباره تلاش کنید.
    </div>
    <% } %>
    <% using (Html.BeginForm())
       { %>
    <div>
        <%= Html.LabelFor(shD => shD.Name) %>
        <%= Html.EditorFor(shD => shD.Name) %>
    </div>
    <div>
        <%= Html.LabelFor(shD => shD.Country) %>
        <%= Html.EditorFor(shD => shD.Country) %>
    </div>
    <div>
        <%= Html.LabelFor(shD => shD.State) %>
        <%= Html.EditorFor(shD => shD.State) %>
    </div>
    <div>
        <%= Html.LabelFor(shD => shD.City) %>
        <%= Html.EditorFor(shD => shD.City) %>
    </div>
    <div>
        <%= Html.LabelFor(shD => shD.Address) %>
        <%= Html.EditorFor(shD => shD.Address) %>
    </div>
    <div>
        <%= Html.LabelFor(shD => shD.Zip) %>
        <%= Html.EditorFor(shD => shD.Zip) %>
    </div>
    <p>
        <input type="submit" value="پایان" /></p>
    <% } %>
</asp:Content>
