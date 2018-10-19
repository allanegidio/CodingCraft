using Estamparia.MVC.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Lojinha.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Indice()
        {
            Response.SetCookie(CreateLayoutNameCookie());
            Response.SetCookie(CreateLayoutValueCookie());

            return View();
        }

        private HttpCookie CreateLayoutNameCookie()
        {
            HttpCookie cookieLayoutName = new HttpCookie("LayoutName");
            cookieLayoutName.Value = Layout.Bootstrap.ToString();
            cookieLayoutName.Expires = DateTime.Now.AddHours(1);
            return cookieLayoutName;
        }

        private HttpCookie CreateLayoutValueCookie()
        {
            HttpCookie cookieLayoutName = new HttpCookie("CurrentLayout");
            cookieLayoutName.Value = Layout.Bootstrap.ToString("D");
            cookieLayoutName.Expires = DateTime.Now.AddHours(1);
            return cookieLayoutName;
        }
    }
}