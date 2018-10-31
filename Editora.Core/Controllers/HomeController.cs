using System.Web.Mvc;

namespace Editora.Core.Controllers
{
    public class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            ViewBag.MainTitle = "Site Institucional";

            return View();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}