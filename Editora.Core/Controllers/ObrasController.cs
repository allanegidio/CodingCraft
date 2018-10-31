using Editora.Core.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Editora.Core.Controllers
{
    public class ObrasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Obras
        public async Task<ActionResult> Index()
        {
            var obras = await db.Obras.Include(o => o.Autor).ToListAsync();
            return View(obras);
        }

        // GET: Obras/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = await db.Obras.FindAsync(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
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
