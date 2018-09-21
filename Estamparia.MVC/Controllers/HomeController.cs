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
            var cookie = new HttpCookie("LayoutName", Layout.Bootstrap.ToString());

            cookie.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Add(cookie);

            return View();
        }
    }
}