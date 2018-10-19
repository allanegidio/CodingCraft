using Estamparia.MVC.Filters;
using System.Web.Mvc;

namespace Estamparia.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LayoutChooserAttribute());
        }
    }
}
