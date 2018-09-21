using Estamparia.MVC.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Estamparia.MVC.Controllers
{
    public class EstampasController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Estampas/

        public async Task<ActionResult> Indice()
        {
            return View(await context.Estampas.ToListAsync());
        }

        //
        // GET: /Estampas/Detalhes/{id}

        public async Task<ActionResult> Detalhes(int id)
        {
			 if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Estampa estampa = await context.Estampas.SingleAsync(x => x.EstampaId == id);

			if (estampa == null)
				return HttpNotFound();

            return View(estampa);
        }

        //
        // GET: /Estampas/Criar

		public ActionResult Criar()
        {
            return View();
        } 

        //
        // POST: /Estampas/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(Estampa estampa)
        {
            if (ModelState.IsValid)
            {
                context.Estampas.Add(estampa);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Indice));  
            }

            return View(estampa);
        }
        
        //
        // GET: /Estampas/Editar/{id}
 
        public async Task<ActionResult> Editar(int id)
        {
			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Estampa estampa = await context.Estampas.SingleAsync(x => x.EstampaId == id);

			if (estampa == null)
				return HttpNotFound();

            return View(estampa);
        }

        //
        // POST: /Estampas/Editar/{id}

        [HttpPost]
        public async Task<ActionResult> Editar(Estampa estampa)
        {
            if (ModelState.IsValid)
            {
                context.Entry(estampa).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Indice));
            }
            return View(estampa);
        }

        //
        // GET: /Estampas/Excluir/{id}
        public async Task<ActionResult> Excluir(int id)
        {
			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Estampa estampa = await context.Estampas.SingleAsync(x => x.EstampaId == id);

			if (estampa == null)
				return HttpNotFound();

            return View(estampa);
        }

        //
        // POST: /Estampas/Excluir/{id}

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(int id)
        {
            Estampa estampa = await context.Estampas.SingleAsync(x => x.EstampaId == id);
            context.Estampas.Remove(estampa);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Indice));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) 
                context.Dispose();
            
            base.Dispose(disposing);
        }
    }
}