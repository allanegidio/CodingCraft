using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lojinha.MVC.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string Controller(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }

        public static string Action(this HtmlHelper htmlHelper)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
                return (string)routeValues["action"];

            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string ActionQueryLink(this HtmlHelper htmlHelper,
            string linkText, string action, string controller)
        {
            return ActionQueryLink(htmlHelper, linkText, action, controller, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText"></param>
        /// <param name="action"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static string ActionQueryLink(this HtmlHelper htmlHelper,
            string linkText, string action, string controller, object routeValues)
        {
            var queryString =
                htmlHelper.ViewContext.HttpContext.Request.QueryString;

            var newRoute = routeValues == null
                ? htmlHelper.ViewContext.RouteData.Values
                : new RouteValueDictionary(routeValues);

            foreach (string key in queryString.Keys)
            {
                if (!newRoute.ContainsKey(key))
                    newRoute.Add(key, queryString[key]);
            }

            return HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext,
                htmlHelper.RouteCollection, linkText, null /* routeName */,
                action, controller, newRoute, null);
        }
        public static string ActionReferrerQuery(this HtmlHelper htmlHelper,
            string linkText, string action, string controller, object routeValues)
        {
            var referrer = htmlHelper.ViewContext.HttpContext.Request.UrlReferrer;
            if (referrer.Query == null) return HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext,
                htmlHelper.RouteCollection, linkText, "Default", action, controller, null, null);

            var queryString = referrer.Query.Replace("?", "");

            var newRoute = routeValues == null
                ? htmlHelper.ViewContext.RouteData.Values
                : new RouteValueDictionary(routeValues);

            if (!string.IsNullOrEmpty(queryString))
            {
                foreach (string key in queryString.Split('&'))
                {
                    var keyValuePair = key.Split('=');
                    if (!newRoute.ContainsKey(keyValuePair[0]))
                        newRoute.Add(keyValuePair[0], keyValuePair[1]);
                }
            }

            return HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext,
                htmlHelper.RouteCollection, linkText, "Default", action, controller, newRoute, null);
        }
    }

}