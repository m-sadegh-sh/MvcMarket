namespace MvcMarket.FrontEnd.HtmlHelpers
{
    using System;
    using System.Text;
    using System.Web.Mvc;

    public static class PagingHelpers
    {
        public static string PageLinks(this HtmlHelper html, int currentPage,
                                       int totalPages, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();
            for (var i = 1; i <= totalPages; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == currentPage)
                    tag.AddCssClass("selected");
                result.AppendLine(tag.ToString());
            }

            return result.ToString();
        }
    }
}