<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcMarket.BackEnd.Product>" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>مدیریت : ویرایش
        <%= Model.Name %></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        ویرایش
        <%= Model.Name %></h1>
    <% using (Html.BeginForm("Product", "Admin", FormMethod.Post,
                             new { enctype = "multipart/form-data" }))
       { %>
    <%= Html.Hidden("ProductId") %>
    <p>
        نام:
        <%= Html.TextBox("Name") %>
        <div>
            <%= Html.ValidationMessage("Name") %></div>
    </p>
    <p>
        توضیحات:
        <%= Html.TextArea("Description", null, 4, 20, null) %>
        <div>
            <%= Html.ValidationMessage("Description") %></div>
    </p>
    <p>
        قیمت:
        <%= Html.TextBox("Price") %>
        <div>
            <%= Html.ValidationMessage("Price") %></div>
    </p>
    <p>
        دسته:
        <%= Html.DropDownList("Categories", ViewData["Categories"] as IEnumerable<SelectListItem>) %>
        <div>
            <%= Html.ValidationMessage("Category") %></div>
    </p>
    <p>
        تصویر:
        <%--<% if (Model.ImageData == null)
           { %>
        هیچ
        <% }
           else
           { %>
        <img src="<%= Url.Action("GetImage", "Products", 
                                 new { ProductID = Model.ProductId }) %>" style="height: 96px;
            width: 96px" />
        <% } %>--%>
        <div>
            تصویر جدید:
            <input type="file" name="Image" /></div>
    </p>
    <input type="submit" value="ثبت" />
    or
    <%=Html.ActionLink("لفو تغییرات و بازگشت به لیست محصولات", "Products") %>
    <% } %>
</asp:Content>
