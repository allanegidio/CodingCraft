using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;
using Estamparia.MVC.Models;

namespace Estamparia.MVC.Controllers
{   
    public class CategoriasController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Categorias/

        public async Task<ActionResult> Indice()
        {
            return View(await context.Categorias.Include(categoria => categoria.Produtos).ToListAsync());
        }

        //
        // GET: /Categorias/Detalhes/{id}

        public async Task<ActionResult> Detalhes(System.Guid id)
        {
			 if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Categoria categoria = await context.Categorias.SingleAsync(x => x.CategoriaId == id);

			if (categoria == null)
				return HttpNotFound();

            return View(categoria);
        }

        //
        // GET: /Categorias/Criar

		public ActionResult Criar()
        {
            return View();
        } 

        //
        // POST: /Categorias/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                categoria.CategoriaId = Guid.NewGuid();
                context.Categorias.Add(categoria);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Indice));  
            }

            return View(categoria);
        }
        
        //
        // GET: /Categorias/Editar/{id}
 
        public async Task<ActionResult> Editar(System.Guid id)
        {
			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Categoria categoria = await context.Categorias.SingleAsync(x => x.CategoriaId == id);

			if (categoria == null)
				return HttpNotFound();

            return View(categoria);
        }

        //
        // POST: /Categorias/Editar/{id}

        [HttpPost]
        public async Task<ActionResult> Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                context.Entry(categoria).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Indice));
            }
            return View(categoria);
        }

        //
        // GET: /Categorias/Excluir/{id}
        public async Task<ActionResult> Excluir(System.Guid id)
        {
			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Categoria categoria = await context.Categorias.SingleAsync(x => x.CategoriaId == id);

			if (categoria == null)
				return HttpNotFound();

            return View(categoria);
        }

        //
        // POST: /Categorias/Excluir/{id}

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(System.Guid id)
        {
            Categoria categoria = await context.Categorias.SingleAsync(x => x.CategoriaId == id);
            context.Categorias.Remove(categoria);
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