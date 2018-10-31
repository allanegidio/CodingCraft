using System.Web.Mvc;

namespace Editora.Intranet.Controllers
{
    public class HomeController : Core.Controllers.HomeController
    {
        public override ActionResult Index()
        {
            var result = base.Index();

            ViewBag.MainTitle = "Site Intranet";

            return result;
        }
    }
}