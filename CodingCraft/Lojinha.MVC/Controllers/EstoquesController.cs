using Lojinha.MVC.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lojinha.MVC.Controllers
{
    public class EstoquesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Estoques
        public async Task<ActionResult> Index()
        {
            var estoques = db.Estoques.Include(e => e.Loja).Include(e => e.Produto);
            return View(await estoques.ToListAsync());
        }

        // GET: Estoques/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estoque estoque = await db.Estoques.FindAsync(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            return View(estoque);
        }

        // GET: Estoques/Create
        public ActionResult Create()
        {
            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome");
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome");
            return View();
        }

        // POST: Estoques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EstoqueId,ProdutoId,LojaId,Quantidade")] Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                db.Estoques.Add(estoque);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome", estoque.LojaId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", estoque.ProdutoId);
            return View(estoque);
        }

        // GET: Estoques/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estoque estoque = await db.Estoques.FindAsync(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome", estoque.LojaId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", estoque.ProdutoId);
            return View(estoque);
        }

        // POST: Estoques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EstoqueId,ProdutoId,LojaId,Quantidade")] Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estoque).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome", estoque.LojaId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", estoque.ProdutoId);
            return View(estoque);
        }

        // GET: Estoques/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estoque estoque = await db.Estoques.FindAsync(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            return View(estoque);
        }

        // POST: Estoques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Estoque estoque = await db.Estoques.FindAsync(id);
            db.Estoques.Remove(estoque);
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
