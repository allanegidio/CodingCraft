using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Estamparia.MVC.Models;

namespace Estamparia.MVC.Controllers
{   
    public class CategoriasController : Controller
    {
        private EstampariaMVCContext context = new EstampariaMVCContext();

        //
        // GET: /Categorias/

        public ViewResult Indice()
        {
            return View(context.Categorias.Include(categoria => categoria.Produtos).ToList());
        }

        //
        // GET: /Categorias/Detalhes/{id}

        public ViewResult Detalhes(System.Guid id)
        {
            Categoria categoria = context.Categorias.Single(x => x.CategoriaId == id);
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
        public ActionResult Criar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                categoria.CategoriaId = Guid.NewGuid();
                context.Categorias.Add(categoria);
                context.SaveChanges();
                return RedirectToAction(nameof(Indice));  
            }

            return View(categoria);
        }
        
        //
        // GET: /Categorias/Editar/{id}
 
        public ActionResult Editar(System.Guid id)
        {
            Categoria categoria = context.Categorias.Single(x => x.CategoriaId == id);
            return View(categoria);
        }

        //
        // POST: /Categorias/Editar/{id}

        [HttpPost]
        public ActionResult Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                context.Entry(categoria).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction(nameof(Indice));
            }
            return View(categoria);
        }

        //
        // GET: /Categorias/Excluir/{id}
 
        public ActionResult Excluir(System.Guid id)
        {
            Categoria categoria = context.Categorias.Single(x => x.CategoriaId == id);
            return View(categoria);
        }

        //
        // POST: /Categorias/Excluir/{id}

        [HttpPost, ActionName(nameof(Excluir))]
        public ActionResult ConfirmarExclusao(System.Guid id)
        {
            Categoria categoria = context.Categorias.Single(x => x.CategoriaId == id);
            context.Categorias.Remove(categoria);
            context.SaveChanges();
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