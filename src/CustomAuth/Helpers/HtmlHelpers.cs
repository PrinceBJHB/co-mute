using System;
using System.Web.Mvc;

namespace CustomAuth.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString RenderHtml(this HtmlHelper helper, string path)
        {
            return new MvcHtmlString (WebGetContent.GetPageData(path));
        }
    }
}