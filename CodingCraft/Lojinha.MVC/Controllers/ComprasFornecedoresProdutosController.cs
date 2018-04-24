using Lojinha.MVC.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lojinha.MVC.Controllers
{
    public class ComprasFornecedoresProdutosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ComprasFornecedoresProdutos
        public async Task<ActionResult> Index()
        {
            var comprasFornecedoresProdutos = db.ComprasFornecedoresProdutos.Include(c => c.ProdutoFornecedor);
            return View(await comprasFornecedoresProdutos.ToListAsync());
        }

        // GET: ComprasFornecedoresProdutos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraFornecedorProduto compraFornecedorProduto = await db.ComprasFornecedoresProdutos.FindAsync(id);
            if (compraFornecedorProduto == null)
            {
                return HttpNotFound();
            }
            return View(compraFornecedorProduto);
        }

        // GET: ComprasFornecedoresProdutos/Create
        public ActionResult Create()
        {
            ViewBag.ProdutoFornecedorId = new SelectList(db.ProdutosFornecedores
                                                    .Include(p => p.Produto),
                                                    "ProdutoFornecedorId",
                                                    "Produto.Nome");
            return View();
        }

        // POST: ComprasFornecedoresProdutos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompraFornecedorProdutoId,Quantidade,ProdutoFornecedorId")] CompraFornecedorProduto compraFornecedorProduto)
        {

            if (ModelState.IsValid)
            {
                db.ComprasFornecedoresProdutos.Add(compraFornecedorProduto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProdutoFornecedorId = new SelectList(db.ProdutosFornecedores, "ProdutoFornecedorId", "UsuarioModificacao", compraFornecedorProduto.ProdutoFornecedorId);
            return View(compraFornecedorProduto);
        }

        // GET: ComprasFornecedoresProdutos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraFornecedorProduto compraFornecedorProduto = await db.ComprasFornecedoresProdutos.FindAsync(id);
            if (compraFornecedorProduto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProdutoFornecedorId = new SelectList(db.ProdutosFornecedores, "ProdutoFornecedorId", "UsuarioModificacao", compraFornecedorProduto.ProdutoFornecedorId);
            return View(compraFornecedorProduto);
        }

        // POST: ComprasFornecedoresProdutos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompraFornecedorProdutoId,Quantidade,Total,ProdutoFornecedorId")] CompraFornecedorProduto compraFornecedorProduto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compraFornecedorProduto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProdutoFornecedorId = new SelectList(db.ProdutosFornecedores, "ProdutoFornecedorId", "UsuarioModificacao", compraFornecedorProduto.ProdutoFornecedorId);
            return View(compraFornecedorProduto);
        }

        // GET: ComprasFornecedoresProdutos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompraFornecedorProduto compraFornecedorProduto = await db.ComprasFornecedoresProdutos.FindAsync(id);
            if (compraFornecedorProduto == null)
            {
                return HttpNotFound();
            }
            return View(compraFornecedorProduto);
        }

        // POST: ComprasFornecedoresProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompraFornecedorProduto compraFornecedorProduto = await db.ComprasFornecedoresProdutos.FindAsync(id);
            db.ComprasFornecedoresProdutos.Remove(compraFornecedorProduto);
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
