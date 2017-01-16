using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbracoProjectTemplate.Library.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString RenderDatalayerScript(this HtmlHelper helper, IPublishedContent content, string dlProperty)
        {
            if (!content.HasValue(dlProperty))
                return MvcHtmlString.Create("");

            var datalayerProps = content.GetPropertyValue<Dictionary<string, string>>(dlProperty);
            var result = new StringBuilder();
            result.Append(@"<script>dataLayer = [{");
            result.Append(string.Join(",", datalayerProps.Select(i => $"'{i.Key}':'{i.Value}'")));
            result.Append("}]</script>");
            return MvcHtmlString.Create(result.ToString());
        }
    }
}