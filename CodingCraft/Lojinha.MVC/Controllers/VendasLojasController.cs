using Lojinha.MVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lojinha.MVC.Controllers
{
    public class VendasLojasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VendasLojas
        public async Task<ActionResult> Index()
        {
            var vendaLojas = db.VendaLojas.Include(v => v.Loja);
            return View(await vendaLojas.ToListAsync());
        }

        // GET: VendasLojas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            VendaLoja vendaLoja = await db.VendaLojas.FindAsync(id);

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
                db.VendaLojas.Add(vendaLoja);
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
            
            VendaLoja vendaLoja = await db.VendaLojas.FindAsync(id);

            if (vendaLoja == null)
                return HttpNotFound();
            
            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome", vendaLoja.LojaId);
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
                db.Entry(vendaLoja).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome", vendaLoja.LojaId);
            return View(vendaLoja);
        }

        // GET: VendasLojas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            VendaLoja vendaLoja = await db.VendaLojas.FindAsync(id);

            if (vendaLoja == null)
                return HttpNotFound();
            
            return View(vendaLoja);
        }

        // POST: VendasLojas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            VendaLoja vendaLoja = await db.VendaLojas.FindAsync(id);

            db.VendaLojas.Remove(vendaLoja);
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
