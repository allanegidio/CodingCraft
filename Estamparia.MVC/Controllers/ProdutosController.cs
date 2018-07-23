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
    public class ProdutosController : Controller
    {
        private EstampariaMVCContext context = new EstampariaMVCContext();

        //
        // GET: /Produtos/

        public ViewResult Indice()
        {
            return View(context.Produtoes.Include(produto => produto.Categoria).ToList());
        }

        //
        // GET: /Produtos/Detalhes/{id}

        public ViewResult Detalhes(System.Guid id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // GET: /Produtos/Criar

        public ActionResult Criar()
        {
            ViewBag.PossibleCategorias = context.Categorias.ToList();
            return View();
        } 

        //
        // POST: /Produtos/Criar

        [HttpPost]
        public ActionResult Criar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.ProdutoId = Guid.NewGuid();
                context.Produtoes.Add(produto);
                context.SaveChanges();
                return RedirectToAction(nameof(Indice));  
            }

            ViewBag.PossibleCategorias = context.Categorias.ToList();
            return View(produto);
        }
        
        //
        // GET: /Produtos/Editar/{id}
 
        public ActionResult Editar(System.Guid id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            ViewBag.PossibleCategorias = context.Categorias.ToList();
            return View(produto);
        }

        //
        // POST: /Produtos/Editar/{id}

        [HttpPost]
        public ActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                context.Entry(produto).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction(nameof(Indice));
            }
            ViewBag.PossibleCategorias = context.Categorias.ToList();
            return View(produto);
        }

        //
        // GET: /Produtos/Excluir/{id}
 
        public ActionResult Excluir(System.Guid id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // POST: /Produtos/Excluir/{id}

        [HttpPost, ActionName(nameof(Excluir))]
        public ActionResult ConfirmarExclusao(System.Guid id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            context.Produtoes.Remove(produto);
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