using Editora.Core.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Editora.Core.Areas.Admin.Controllers
{
    public class ObrasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Obras
        public async Task<ActionResult> Index()
        {
            var obras = db.Obras.Include(o => o.Autor);
            return View(await obras.ToListAsync());
        }

        // GET: Admin/Obras/Details/5
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

        // GET: Admin/Obras/Create
        public ActionResult Create()
        {
            ViewBag.AutorId = new SelectList(db.Autores, "AutorId", "Nome");
            return View();
        }

        // POST: Admin/Obras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ObraId,AutorId,Titulo,Descricao")] Obra obra)
        {
            if (ModelState.IsValid)
            {
                db.Obras.Add(obra);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AutorId = new SelectList(db.Autores, "AutorId", "Nome", obra.AutorId);
            return View(obra);
        }

        // GET: Admin/Obras/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            ViewBag.AutorId = new SelectList(db.Autores, "AutorId", "Nome", obra.AutorId);
            return View(obra);
        }

        // POST: Admin/Obras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ObraId,AutorId,Titulo,Descricao")] Obra obra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obra).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AutorId = new SelectList(db.Autores, "AutorId", "Nome", obra.AutorId);
            return View(obra);
        }

        // GET: Admin/Obras/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Admin/Obras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Obra obra = await db.Obras.FindAsync(id);
            db.Obras.Remove(obra);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
