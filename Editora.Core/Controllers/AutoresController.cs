using Editora.Core.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Editora.Core.Controllers
{
    public class AutoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Autores
        public async Task<ActionResult> Index()
        {
            return View(await db.Autores.ToListAsync());
        }

        // GET: Autores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autor autor = await db.Autores.FindAsync(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
