using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Web;
using File = System.IO.File;

namespace UmbracoProjectTemplate.Web.Helpers
{
    /// <summary>
    ///     This is where you'd add you own HtmlHelper methods, look below to see how to
    ///     attach a method to the HtmlHelper class
    /// </summary>
    public static class HtmlHelpers
    {
        /// <summary>
        ///     Writes a recursive page title, of page name if no page title is found is tree
        /// </summary>
        /// <param name="helper">Used to attach the method to the @Html helper used in mvc views</param>
        /// <param name="page">Page to look for title in</param>
        /// <returns>A Html string</returns>
        public static MvcHtmlString PageTitle(this HtmlHelper helper, IPublishedContent page)
        {
            var title = page.GetPropertyValue<string>("title", recurse: true);
            if (string.IsNullOrEmpty(title))
                title = page.Name;
            return MvcHtmlString.Create(title);
        }

        /// <summary>
        ///     Writes out at link to the css url provided, with a ?v=*assemblyversionnumber*
        ///     appended for cache busting
        /// </summary>
        /// <param name="helper">Used to attach the method to the @Html helper used in mvc views</param>
        /// <param name="resUrl">The relative url for the css file</param>
        /// <returns>A Html string inserted via @Html.VersionedCssUrl("/path/to/css")</returns>
        public static MvcHtmlString VersionedCssUrl(this HtmlHelper helper, string resUrl)
        {
            if (string.IsNullOrWhiteSpace(resUrl) || !File.Exists(HttpContext.Current.Server.MapPath(resUrl)))
            {
                LogHelper.Warn(typeof(HtmlHelpers), $@"Couldn't find: {resUrl}");
                return MvcHtmlString.Create("");
            }

            var version = typeof(HtmlHelpers).Assembly.GetName().Version.ToString();
            var result = $@"<link rel=""stylesheet"" href=""{resUrl}?v={version}"" />";
            return MvcHtmlString.Create(result);
        }

        /// <summary>
        ///     Writes out a javascript tag using the provided url as src, with a ?v=*assemblyversionnumber*
        ///     appended for cache busting
        /// </summary>
        /// <param name="helper">Used to attach the method to the @Html helper used in mvc views</param>
        /// <param name="resUrl">The relative url for the js file</param>
        /// <returns>A Html string inserted via @Html.VersionedJsUrl("/path/to/css")</returns>
        public static MvcHtmlString VersionedJsUrl(this HtmlHelper helper, string resUrl)
        {
            if (string.IsNullOrWhiteSpace(resUrl) || !File.Exists(HttpContext.Current.Server.MapPath(resUrl)))
            {
                LogHelper.Warn(typeof(HtmlHelpers), $@"Couldn't find: {resUrl}");
                return MvcHtmlString.Create("");
            }

            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var result = $@"<script src=""{resUrl}?v={version}""></script>";
            return MvcHtmlString.Create(result);
        }

        /// <summary>
        ///     Writes out an img tag using the provided url as src, with a ?v=*assemblyversionnumber*
        ///     appended for cache busting
        /// </summary>
        /// <param name="helper">Used to attach the method to the @Html helper used in mvc views</param>
        /// <param name="resUrl">The relative url for the js file</param>
        /// <param name="altTxt">Optional, used as content in alt attribute</param>
        /// <param name="width">Optional, used as content in width attribute</param>
        /// <param name="height">Optional, used as content in height attribute</param>
        /// <returns>A Html string inserted via @Html.VersionedJsUrl("/path/to/css")</returns>
        public static MvcHtmlString VersionedImgUrl(this HtmlHelper helper, string resUrl, string altTxt = null,
            string width = null, string height = null)
        {
            if (string.IsNullOrWhiteSpace(resUrl) || !File.Exists(HttpContext.Current.Server.MapPath(resUrl)))
            {
                LogHelper.Warn(typeof(HtmlHelpers), $"Couldn't find: {resUrl}");
                return MvcHtmlString.Create("");
            }

            var version = typeof(HtmlHelpers).Assembly.GetName().Version.ToString();
            var attrs = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(altTxt))
                attrs.AppendFormat(@" alt=""{0}""", altTxt);
            if (!string.IsNullOrWhiteSpace(width))
                attrs.AppendFormat(@" width=""{0}""", width);
            if (!string.IsNullOrWhiteSpace(height))
                attrs.AppendFormat(@" height=""{0}""", height);
            var result = $@"<img src=""{resUrl}?v={version}""{attrs} />";
            return MvcHtmlString.Create(result);
        }

        /// <summary>
        ///     Writes an image tag from a media id
        /// </summary>
        /// <param name="helper">Used to attach method to the @Html helper in mvc views</param>
        /// <param name="mediaId">The id of the media item as an integer</param>
        /// <param name="renderDimensions">Boolean value indicating if you want to write out dimension attributes</param>
        /// <param name="width">Set resize width</param>
        /// <param name="height">Set resize height</param>
        /// <param name="bgcolor">Set background color in HEX for canvas mode</param>
        /// <param name="mode">Set mode: crop, pad</param>
        /// <param name="fallbackImageUrl">string url for fallback image</param>
        /// <returns>A Html string to insert in the output</returns>
        public static MvcHtmlString UmbMediaImg(this HtmlHelper helper, int mediaId, bool renderDimensions = false,
            int? width = null, int? height = null, string bgcolor = null, string mode = "crop",
            string fallbackImageUrl = null)
        {
            return UmbMediaImg(helper, Convert.ToString(mediaId), renderDimensions, width, height, bgcolor, mode,
                fallbackImageUrl);
        }

        /// <summary>
        ///     Writes an image tag from a media id
        /// </summary>
        /// <param name="helper">Used to attach method to the @Html helper in mvc views</param>
        /// <param name="mediaId">The id of the media item as a string</param>
        /// <param name="renderDimensions">Boolean value indicating if you want to write out dimension attributes</param>
        /// <param name="width">Set resize width</param>
        /// <param name="height">Set resize height</param>
        /// <param name="bgcolor">Set background color in HEX for canvas mode</param>
        /// <param name="mode">Set mode: crop, pad</param>
        /// <param name="fallbackImageUrl">string url for fallback image</param>
        /// <returns>A Html string to insert in the output</returns>
        public static MvcHtmlString UmbMediaImg(this HtmlHelper helper, string mediaId, bool renderDimensions = false,
            int? width = null, int? height = null, string bgcolor = null, string mode = "crop",
            string fallbackImageUrl = null)
        {
            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            var image = null as IPublishedContent;
            try
            {
                image = umbHelper.TypedMedia(mediaId ?? "0");
            }
            catch (ArgumentNullException)
            {
            }
            if (image != null)
                return UmbMediaImg(helper, image, renderDimensions, width, height, bgcolor, mode);

            if (!string.IsNullOrWhiteSpace(fallbackImageUrl))
            {
                var result = $@"<img src={fallbackImageUrl} alt=""no image available"" />";
                return MvcHtmlString.Create(result);
            }

            return new MvcHtmlString("");
        }

        /// <summary>
        ///     Writes an image tag from a media item as a IPublishedContent item
        /// </summary>
        /// <param name="helper">Used to attach method to the @Html helper in mvc views</param>
        /// <param name="image">The media item as IPublishedContent</param>
        /// <param name="renderDimensions">Boolean value indicating if you want to write out dimension attributes</param>
        /// <param name="width">Set resize width</param>
        /// <param name="height">Set resize height</param>
        /// <param name="bgcolor">Set background color in HEX for canvas mode</param>
        /// <param name="mode">Set mode: crop, pad</param>
        /// <param name="anchor">
        ///     Set anchor: topleft, topcenter, topright, middleleft, middlecenter, middleright, bottomleft,
        ///     bottomcenter, and bottomright
        /// </param>
        /// <param name="fallbackImageUrl">
        ///     Set fallback image in case the image doesn't exist and you want to display a default
        ///     image
        /// </param>
        /// <returns>A Html string to insert in the output</returns>
        public static MvcHtmlString UmbMediaImg(this HtmlHelper helper, IPublishedContent image,
            bool renderDimensions = false, int? width = null, int? height = null, string bgcolor = null,
            string mode = "crop", string anchor = "middlecenter", string fallbackImageUrl = null)
        {
            if (image != null)
            {
                var nvc = new NameValueCollection();
                if (width.HasValue)
                    nvc.Add("w", Convert.ToString(width.Value));
                if (height.HasValue)
                    nvc.Add("h", Convert.ToString(height.Value));
                if (width.HasValue || height.HasValue)
                    nvc.Add("mode", mode);
                if (width.HasValue || height.HasValue)
                    nvc.Add("anchor", anchor);
                if (width.HasValue || height.HasValue)
                    nvc.Add("scale", "canvas");
                if (width.HasValue || height.HasValue)
                    nvc.Add("quality", "100");
                if (bgcolor != null)
                    nvc.Add("bgcolor", bgcolor);
                var qs = string.Join("&",
                    nvc.AllKeys.Select(
                        key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(nvc.Get(key))}"));

                var src = string.IsNullOrEmpty(qs) ? image.Url : image.Url + "?" + qs;
                var alt = string.Empty;
                try
                {
                    alt = image.GetPropertyValue<string>("altText");
                }
                catch (ArgumentNullException)
                {
                }
                var title = string.Empty;
                try
                {
                    title = image.GetPropertyValue<string>("titleText");
                }
                catch (ArgumentNullException)
                {
                }

                title = string.IsNullOrWhiteSpace(title) ? string.Empty : $@" title=""{title}""";
                var widthAttr = string.Empty;
                var heightAttr = string.Empty;

                if (renderDimensions)
                    if (width.HasValue || height.HasValue)
                    {
                        if (width.HasValue)
                            widthAttr = $@" width=""{Convert.ToString(width.Value)}""";
                        if (height.HasValue)
                            heightAttr = $@" height=""{Convert.ToString(height.Value)}""";
                    }
                    else
                    {
                        widthAttr = $@" width=""{image.GetPropertyValue<string>("umbracoWidth")}""";
                        heightAttr = $@" height=""{image.GetPropertyValue<string>("umbracoHeight")}""";
                    }

                var result = $@"<img src=""{src}"" alt=""{alt}""{title}{widthAttr}{heightAttr} />";
                return MvcHtmlString.Create(result);
            }

            if (!string.IsNullOrWhiteSpace(fallbackImageUrl))
            {
                var result = $@"<img src={fallbackImageUrl} alt=""no image available"" />";
                return MvcHtmlString.Create(result);
            }

            return MvcHtmlString.Empty;
        }

        /// <summary>
        ///     Writes out the robots meta tag based on the content page passed to the method
        /// </summary>
        /// <param name="helper">Used to attach method to the @Html helper in mvc views</param>
        /// <param name="content">The content page to base the meta tag content on</param>
        /// <returns>A Html string to insert in the output</returns>
        public static MvcHtmlString RobotsTag(this HtmlHelper helper, IPublishedContent content)
        {
            var showNoindex = content.GetPropertyValue<bool>("seoNoindex");
            var showNofollow = content.GetPropertyValue<bool>("seoNofollow");
            var showRobotsTag = showNoindex || showNofollow;
            if (!showRobotsTag)
                return MvcHtmlString.Create("");
            var robotsTagContent = showNoindex ? "noindex" : "";
            if (showNoindex && showNofollow)
                robotsTagContent += ",";
            robotsTagContent += showNofollow ? "nofollow" : "";
            robotsTagContent = $@"<meta name=""robots"" content=""{robotsTagContent}"" />";
            return MvcHtmlString.Create(robotsTagContent);
        }

        /// <summary>
        ///     Writes out a canonical tag base on the content page passed to the method
        /// </summary>
        /// <param name="helper">Used to attach method to the @Html helper in mvc views</param>
        /// <param name="content">The content page to base the meta tag content on</param>
        /// <returns>A Html string to insert in the output</returns>
        public static MvcHtmlString CanonicalTag(this HtmlHelper helper, IPublishedContent content)
        {
            var canonicalId = content.GetPropertyValue<string>("seoCanonical");
            if (string.IsNullOrWhiteSpace(canonicalId))
                canonicalId = Convert.ToString(content.Id);
            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            var canonicalPageUrl = umbHelper.TypedContent(canonicalId).Url;
            canonicalPageUrl = $@"<link rel=""canonical"" href=""{canonicalPageUrl}"" />";
            return MvcHtmlString.Create(canonicalPageUrl);
        }

        /// <summary>
        ///     Builds a link to a file with icon classes
        /// </summary>
        /// <param name="helper">Extending</param>
        /// <param name="link">Url for the link</param>
        /// <param name="linkTxt">Text inside a tag</param>
        /// <param name="title">Title attribute</param>
        /// <param name="fileName">Optional filename to look for filetype</param>
        /// <param name="target">Window target</param>
        /// <returns></returns>
        public static MvcHtmlString FileLink(this HtmlHelper helper, string link, string linkTxt, string title = "",
            string fileName = "", string target = "")
        {
            const string linkTmpl =
                @"<span class=""{3}"">&nbsp;</span><a href=""{0}"" target=""{1}"" title=""{2}"">{4}</a>";
            var knownfileTypes = new Dictionary<string, string>
            {
                {".default", "icon-file"},
                {".pdf", "icon-file-pdf"},
                {".ppt", "icon-file-powerpoint"},
                {".pptx", "icon-file-powerpoint"},
                {".doc", "icon-file-word"},
                {".docx", "icon-file-word"},
                {".xls", "icon-file-excel"},
                {".xlsx", "icon-file-excel"},
                {".zip", "icon-file-zip"},
                {".7z", "icon-file-zip"}
            };
            var fileEnding = string.Empty;
            if (!string.IsNullOrWhiteSpace(fileName))
                fileEnding = Path.GetExtension(fileName);
            if (string.IsNullOrWhiteSpace(fileEnding))
                fileEnding = Path.GetExtension(link);
            if (string.IsNullOrWhiteSpace(fileEnding))
                fileEnding = "default";
            var cssClass = knownfileTypes[fileEnding];
            return new MvcHtmlString(string.Format(linkTmpl, link, target, title, cssClass, linkTxt));
        }
    }
}