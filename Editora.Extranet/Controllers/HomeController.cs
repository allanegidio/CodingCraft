using System.Web.Mvc;

namespace Editora.Extranet.Controllers
{
    public class HomeController : Core.Controllers.HomeController
    {
        public override ActionResult Index()
        {
            var result = base.Index();

            ViewBag.MainTitle = "Site Extranet";

            return result;
        }
    }
}