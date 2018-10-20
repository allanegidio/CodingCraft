using System.Web;
using System.Web.Mvc;

namespace Estamparia.MVC.Filters
{
    public class LayoutChooserAttribute : ActionFilterAttribute
    {
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
    }
}