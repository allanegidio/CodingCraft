using Lojinha.MVC.Extensions;
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
    public class VendasLojasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VendasLojas
        public async Task<ActionResult> Index()
        {
            var vendaLojas = db.VendasLojas.Include(v => v.Loja);
            return View(await vendaLojas.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Pesquisar(string filtro)
        {
            if (!filtro.ContainsValue())
                return RedirectToAction("Index");

            var lojas = await db.VendasLojas
                            .Include(vl => vl.Loja)
                            .Where(vl => vl.Loja.Nome.Contains(filtro))
                            .ToListAsync();

            return View("Index", lojas);
        }


        // GET: VendasLojas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            VendaLoja vendaLoja = await db.VendasLojas.FindAsync(id);

            if (vendaLoja == null)
                return HttpNotFound();
            
            return View(vendaLoja);
        }

        // GET: VendasLojas/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Lojas = await db.Lojas.ToListAsync();

            ViewBag.ProdutosLojas = await db.ProdutosLojas
                                            .Include(pj => pj.Loja)
                                            .Include(pj => pj.Produto)
                                            .ToListAsync();

            return View(new VendaLoja { VendaLojaProdutos = new List<VendaLojaProduto>() });
        }

        // POST: VendasLojas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VendaLojaId,LojaId,VendaLojaProdutos,Data")] VendaLoja vendaLoja)
        {
            if (ModelState.IsValid)
            {
                db.VendasLojas.Add(vendaLoja);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Lojas = await db.Lojas.ToListAsync();

            ViewBag.ProdutosLojas = await db.ProdutosLojas
                                            .Include(pj => pj.Loja)
                                            .Include(pj => pj.Produto)
                                            .ToListAsync();

            return View(vendaLoja);
        }

        // GET: VendasLojas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            VendaLoja vendaLoja = await db.VendasLojas.FindAsync(id);

            if (vendaLoja == null)
                return HttpNotFound();

            ViewBag.ProdutosLojas = await db.ProdutosLojas
                                        .Include(pj => pj.Produto)
                                        .Include(pj => pj.Loja)
                                        .Where(pj => pj.LojaId == vendaLoja.LojaId)
                                        .ToListAsync();

            return View(vendaLoja);
        }

        // POST: VendasLojas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VendaLojaId,LojaId,Data,VendaLojaProdutos")] VendaLoja vendaLoja)
        {
            if (ModelState.IsValid)
            {
                using(var scope = new TransactionScope())
                {
                    //Produtos Apagados
                    var produtosOriginais = await db.VendasLojasProdutos
                                                .AsNoTracking()
                                                .Where(vlp => vlp.VendaLojaId == vendaLoja.VendaLojaId)
                                                .ToListAsync();

                    foreach(var produto in produtosOriginais)
                    {
                        var existe = vendaLoja.VendaLojaProdutos.Any(vlp => vlp.VendaLojaProdutoId == produto.VendaLojaProdutoId);

                        if (!existe)
                        {
                            var produtoExcluido = await db.VendasLojasProdutos
                                .FirstOrDefaultAsync(vlp => vlp.VendaLojaProdutoId == produto.VendaLojaProdutoId);

                            db.VendasLojasProdutos.Remove(produtoExcluido);
                        }
                    }

                    await db.SaveChangesAsync();

                    foreach (var produto in vendaLoja.VendaLojaProdutos)
                    {
                        //Produtos Editados
                        if (produto.VendaLojaProdutoId > 0)
                        {
                            db.Entry(produto).State = EntityState.Modified;
                        }
                        else
                        {
                            //Produtos Criados
                            produto.VendaLojaId = vendaLoja.VendaLojaId;
                            db.VendasLojasProdutos.Add(produto);
                        }
                    }

                    await db.SaveChangesAsync();

                    db.Entry(vendaLoja).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    scope.Complete();

                    return RedirectToAction("Index");
                }
            }

            ViewBag.ProdutosLojas = db.ProdutosLojas
                                    .Include(pj => pj.Produto)
                                    .Include(pj => pj.Loja)
                                    .Where(pj => pj.LojaId == vendaLoja.LojaId)
                                    .ToListAsync();


            return View(vendaLoja);
        }

        // GET: VendasLojas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            VendaLoja vendaLoja = await db.VendasLojas.FindAsync(id);

            if (vendaLoja == null)
                return HttpNotFound();
            
            return View(vendaLoja);
        }

        // POST: VendasLojas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VendaLoja vendaLoja = await db.VendasLojas.FindAsync(id);

            db.VendasLojas.Remove(vendaLoja);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> NovaLinhaProduto(int lojaId)
        {
            ViewBag.ProdutosLojas = await db.ProdutosLojas
                                            .Include(p => p.Produto)
                                            .Include(p => p.Loja)
                                            .Where(p => p.LojaId == lojaId)
                                            .ToListAsync();

            return PartialView("_LinhaProduto", new VendaLojaProduto());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            
            base.Dispose(disposing);
        }
    }
}
