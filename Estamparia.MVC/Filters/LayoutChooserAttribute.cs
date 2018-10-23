using Estamparia.MVC.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Estamparia.MVC.Filters
{
    public class LayoutChooserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           base.OnActionExecuting(filterContext);

            if (filterContext.HttpContext.Request.Cookies["LayoutName"] != null &&
                filterContext.HttpContext.Request.Cookies["CurrentLayout"] != null) return;

            filterContext.HttpContext.Response.SetCookie(CreateLayoutNameCookie());
            filterContext.HttpContext.Response.SetCookie(CreateLayoutValueCookie());
        }


        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            string layoutName = HttpContext.Current.Request.Cookies.Get("LayoutName").Value;
            string layoutPath = string.Format("~/Views/Shared/_Layout{0}.cshtml", layoutName);

            var result = filterContext.Result as ViewResult;


            if (result == null) return;

            switch (layoutName)
            {
                case "Bootstrap":
                    result.MasterName = layoutPath;
                    break;

                case "Ink":
                    result.MasterName = layoutPath;
                    break;

                case "Zurb":
                    result.MasterName = layoutPath;
                    break;
            }
        }

        private HttpCookie CreateLayoutNameCookie()
        {
            HttpCookie cookieLayoutName = new HttpCookie("LayoutName");
            cookieLayoutName.Value = Layout.Bootstrap.ToString();
            cookieLayoutName.Expires = DateTime.Now.AddMonths(1);
            return cookieLayoutName;
        }

        private HttpCookie CreateLayoutValueCookie()
        {
            HttpCookie cookieLayoutName = new HttpCookie("CurrentLayout");
            cookieLayoutName.Value = Layout.Bootstrap.ToString("D");
            cookieLayoutName.Expires = DateTime.Now.AddMonths(1);
            return cookieLayoutName;
        }

    }
}