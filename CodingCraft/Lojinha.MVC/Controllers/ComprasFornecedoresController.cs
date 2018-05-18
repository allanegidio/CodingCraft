using Lojinha.MVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace Lojinha.MVC.Controllers
{
    public class ComprasFornecedoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CompraaFornecedores
        public async Task<ActionResult> Index()
        {
            return View(await db.ComprasFornecedores.ToListAsync());
        }

        // GET: CompraaFornecedores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            CompraFornecedor compraFornecedor = await db.ComprasFornecedores.FindAsync(id);

            if (compraFornecedor == null)
                return HttpNotFound();
            
            return View(compraFornecedor);
        }

        // GET: CompraaFornecedores/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.ProdutoFornecedores = await db.ProdutosFornecedores
                                                .Include(p => p.Produto)
                                                .Include(p => p.Fornecedor)
                                                .ToListAsync();

            ViewBag.Fornecedores = await db.Fornecedores.ToListAsync();

            return View(new CompraFornecedor { CompraFornecedorProdutos = new List<CompraFornecedorProduto>() });
        }

        // POST: CompraaFornecedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompraFornecedorId,FornecedorId,CompraFornecedorProdutos,Data")] CompraFornecedor compraFornecedor)
        {
            if (ModelState.IsValid)
            {
                db.ComprasFornecedores.Add(compraFornecedor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Fornecedores = await db.Fornecedores.ToListAsync();

            ViewBag.ProdutoFornecedores = await db.ProdutosFornecedores
                                                .Include(p => p.Produto)
                                                .Include(p => p.Fornecedor)
                                                .ToListAsync();

            return View(compraFornecedor);
        }

        // GET: CompraaFornecedores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            

            CompraFornecedor compraFornecedor = await db.ComprasFornecedores.FindAsync(id);

            if (compraFornecedor == null)
                return HttpNotFound();
            

            ViewBag.ProdutoFornecedores = await db.ProdutosFornecedores
                                                .Include(pf => pf.Produto)
                                                .Include(pf => pf.Fornecedor)
                                                .Where(pf => pf.FornecedorId == compraFornecedor.FornecedorId)
                                                .ToListAsync();

            return View(compraFornecedor);
        }

        // POST: CompraaFornecedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompraFornecedorId,FornecedorId,CompraFornecedorProdutos,Data")] CompraFornecedor compraFornecedor)
        {
            if (ModelState.IsValid)
            {
                using(var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Produtos Apagados
                    var produtosOriginais = await db.ComprasFornecedoresProdutos
                                                .AsNoTracking()
                                                .Where(c => c.CompraFornecedorId == compraFornecedor.CompraFornecedorId)
                                                .ToListAsync();

                    foreach(var produtoOriginal in produtosOriginais)
                    {
                        var existe = compraFornecedor.CompraFornecedorProdutos
                            .Any(p => p.CompraFornecedorProdutoId == produtoOriginal.CompraFornecedorProdutoId);

                        if (!existe)
                        {
                            var produtoExcluido = await db.ComprasFornecedoresProdutos
                                .FirstOrDefaultAsync(p => p.CompraFornecedorProdutoId == produtoOriginal.CompraFornecedorProdutoId);

                            db.ComprasFornecedoresProdutos.Remove(produtoExcluido);
                        }
                    }

                    await db.SaveChangesAsync();

                    //Produtos Editados
                    foreach(var produto in compraFornecedor.CompraFornecedorProdutos)
                    {
                        //Produtos Editados
                        if(produto.CompraFornecedorProdutoId > 0)
                        {
                            db.Entry(produto).State = EntityState.Modified;
                        }
                        else
                        {
                            //Produtos Criados
                            produto.CompraFornecedorId = compraFornecedor.CompraFornecedorId;
                            db.ComprasFornecedoresProdutos.Add(produto);
                        }
                    }

                    await db.SaveChangesAsync();

                    db.Entry(compraFornecedor).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ProdutoFornecedores = await db.ProdutosFornecedores
                                                .Include(p => p.Produto)
                                                .Include(p => p.Fornecedor)
                                                .ToListAsync();
            
            return View(compraFornecedor);
        }

        // GET: CompraaFornecedores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            CompraFornecedor compraFornecedor = await db.ComprasFornecedores.FindAsync(id);

            if (compraFornecedor == null)
                return HttpNotFound();

            return View(compraFornecedor);
        }

        // POST: CompraaFornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CompraFornecedor compraFornecedor = await db.ComprasFornecedores.FindAsync(id);

            db.ComprasFornecedores.Remove(compraFornecedor);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> NovaLinhaProduto(int idFornecedor)
        {
            ViewBag.ProdutoFornecedores = await db.ProdutosFornecedores
                                                .Include(p => p.Produto)
                                                .Include(p => p.Fornecedor)
                                                .Where(p => p.FornecedorId == idFornecedor)
                                                .ToListAsync();

            return PartialView("_LinhaProduto", new CompraFornecedorProduto());
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
