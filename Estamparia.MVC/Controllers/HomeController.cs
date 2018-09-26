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
            HttpCookie cookieLayoutName = new HttpCookie("LayoutName", Layout.Bootstrap.ToString());
            cookieLayoutName.Expires = DateTime.Now.AddDays(1);

            HttpCookie cookieCurrentLayout = new HttpCookie("CurrentLayout", Layout.Bootstrap.ToString("D"));
            cookieCurrentLayout.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Add(cookieLayoutName);
            Response.Cookies.Add(cookieCurrentLayout);

            return View();
        }
    }
}