using Lojinha.MVC.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lojinha.MVC.Controllers
{
    public class ProdutosLojasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProdutosLojas
        public async Task<ActionResult> Index()
        {
            var produtosLojas = db.ProdutosLojas
                                .Include(p => p.Loja)
                                .Include(p => p.Produto);
            return View(await produtosLojas.ToListAsync());
        }

        // GET: ProdutosLojas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoLoja produtoLoja = await db.ProdutosLojas.FindAsync(id);
            if (produtoLoja == null)
            {
                return HttpNotFound();
            }
            return View(produtoLoja);
        }

        // GET: ProdutosLojas/Create
        public ActionResult Create()
        {
            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome");
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome");
            return View();
        }

        // POST: ProdutosLojas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProdutoLojaId,ProdutoId,LojaId,Estoque")] ProdutoLoja produtoLoja)
        {
            if (ModelState.IsValid)
            {
                db.ProdutosLojas.Add(produtoLoja);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome", produtoLoja.LojaId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", produtoLoja.ProdutoId);
            return View(produtoLoja);
        }

        // GET: ProdutosLojas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoLoja produtoLoja = await db.ProdutosLojas.FindAsync(id);
            if (produtoLoja == null)
            {
                return HttpNotFound();
            }
            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome", produtoLoja.LojaId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", produtoLoja.ProdutoId);
            return View(produtoLoja);
        }

        // POST: ProdutosLojas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProdutoLojaId,ProdutoId,LojaId,Estoque")] ProdutoLoja produtoLoja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produtoLoja).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome", produtoLoja.LojaId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", produtoLoja.ProdutoId);
            return View(produtoLoja);
        }

        // GET: ProdutosLojas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProdutoLoja produtoLoja = await db.ProdutosLojas.FindAsync(id);
            if (produtoLoja == null)
            {
                return HttpNotFound();
            }
            return View(produtoLoja);
        }

        // POST: ProdutosLojas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProdutoLoja produtoLoja = await db.ProdutosLojas.FindAsync(id);
            db.ProdutosLojas.Remove(produtoLoja);
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
