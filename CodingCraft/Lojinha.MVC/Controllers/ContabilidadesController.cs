using Lojinha.MVC.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lojinha.MVC.Controllers
{
    public class ContabilidadesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contabilidades
        public async Task<ActionResult> Index()
        {
            return View(await db.Contabilidades.ToListAsync());
        }

        // GET: Contabilidades/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Contabilidade contabilidade = await db.Contabilidades.FindAsync(id);

            if (contabilidade == null)
                return HttpNotFound();
            
            return View(contabilidade);
        }

        // GET: Contabilidades/Create
        public ActionResult Create()
        {
            return View(new Contabilidade {
                VendasLojas = new List<VendaLoja>(),
                ComprasFornecedores = new List<CompraFornecedor>()
            });
        }

        // POST: Contabilidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ContabilidadeId,DataInicio,DataFim,Lastro,Data,Fornecedor,Loja,VendasLojas,ComprasFornecedores")] Contabilidade contabilidade)
        {
            if (ModelState.IsValid)
            {

                //contabilidade.VendasLojas = await db.VendasLojas
                //                                .Where(vl => vl.Data >= contabilidade.DataInicio && vl.Data <= contabilidade.DataFim)
                //                                .ToListAsync() as ICollection<VendaLoja>;

                //contabilidade.ComprasFornecedores = await db.ComprasFornecedores
                //                                    .Where(cf => cf.Data >= contabilidade.DataInicio && cf.Data <= contabilidade.DataFim)
                //                                    .ToListAsync() as ICollection<CompraFornecedor>;

                db.Contabilidades.Add(contabilidade);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(contabilidade);
        }

        // GET: Contabilidades/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contabilidade contabilidade = await db.Contabilidades.FindAsync(id);
            if (contabilidade == null)
            {
                return HttpNotFound();
            }
            return View(contabilidade);
        }

        // POST: Contabilidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ContabilidadeId,DataInicio,DataFim,DataModificacao,UsuarioModificacao,DataCriacao,UsuarioCriacao")] Contabilidade contabilidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contabilidade).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contabilidade);
        }

        // GET: Contabilidades/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contabilidade contabilidade = await db.Contabilidades.FindAsync(id);
            if (contabilidade == null)
            {
                return HttpNotFound();
            }
            return View(contabilidade);
        }

        // POST: Contabilidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contabilidade contabilidade = await db.Contabilidades.FindAsync(id);
            db.Contabilidades.Remove(contabilidade);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Contabilizar(Contabilidade contabilidade)
        {
            contabilidade.VendasLojas = await db.VendasLojas
                                                .Where(vl => vl.Data >= contabilidade.DataInicio && vl.Data <= contabilidade.DataFim)
                                                .ToListAsync() as ICollection<VendaLoja>;

            contabilidade.ComprasFornecedores = await db.ComprasFornecedores
                                                .Where(cf => cf.Data >= contabilidade.DataInicio && cf.Data <= contabilidade.DataFim)
                                                .ToListAsync() as ICollection<CompraFornecedor>;

            return PartialView("_Contabilidade", contabilidade);
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
