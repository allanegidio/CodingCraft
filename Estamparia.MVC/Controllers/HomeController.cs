using System.Web.Mvc;

namespace Lojinha.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Indice()
        {
            return View();
        }
    }
}