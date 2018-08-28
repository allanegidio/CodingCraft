using Estamparia.MVC.Models;
using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Estamparia.MVC.Controllers
{
    public class ProdutosController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        //
        // GET: /Produtos/

        public async Task<ActionResult> Indice()
        {
            return View(await context.Produtos.Include(produto => produto.Categoria)
                .Include(produto => produto.Tamanho)
                .Include(produto => produto.Gola).ToListAsync());
        }

        //
        // GET: /Produtos/Detalhes/{id}

        public async Task<ActionResult> Detalhes(System.Guid id)
        {
			 if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Produto produto = await context.Produtos.SingleAsync(x => x.ProdutoId == id);

			if (produto == null)
				return HttpNotFound();

            return View(produto);
        }

        //
        // GET: /Produtos/Criar

        public async Task<ActionResult> Criar()
        {
            ViewBag.Categorias = await context.Categorias.ToListAsync();
            ViewBag.Tamanhos = await context.Tamanhos.ToListAsync();
            ViewBag.Golas = await context.Golas.ToListAsync();

            return View();
        } 

        //
        // POST: /Produtos/Criar

        [HttpPost]
        public async Task<ActionResult> Criar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.ProdutoId = Guid.NewGuid();
                context.Produtos.Add(produto);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Indice));  
            }

            ViewBag.Categorias = await context.Categorias.ToListAsync();
            ViewBag.Tamanhos = await context.Tamanhos.ToListAsync();
            ViewBag.Golas = await context.Golas.ToListAsync();

            return View(produto);
        }
        
        //
        // GET: /Produtos/Editar/{id}
 
        public async Task<ActionResult> Editar(System.Guid id)
        {
			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Produto produto = await context.Produtos.SingleAsync(x => x.ProdutoId == id);

			if (produto == null)
				return HttpNotFound();

            ViewBag.Categorias = await context.Categorias.ToListAsync();
            ViewBag.Tamanhos = await context.Tamanhos.ToListAsync();
            ViewBag.Golas = await context.Golas.ToListAsync();

            return View(produto);
        }

        //
        // POST: /Produtos/Editar/{id}

        [HttpPost]
        public async Task<ActionResult> Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                context.Entry(produto).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Indice));
            }

            ViewBag.Categorias = await context.Categorias.ToListAsync();
            ViewBag.Tamanhos = await context.Tamanhos.ToListAsync();
            ViewBag.Golas = await context.Golas.ToListAsync();

            return View(produto);
        }

        //
        // GET: /Produtos/Excluir/{id}
        public async Task<ActionResult> Excluir(System.Guid id)
        {
			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Produto produto = await context.Produtos.SingleAsync(x => x.ProdutoId == id);

			if (produto == null)
				return HttpNotFound();

            return View(produto);
        }

        //
        // POST: /Produtos/Excluir/{id}

        [HttpPost, ActionName(nameof(Excluir))]
        public async Task<ActionResult> ConfirmarExclusao(System.Guid id)
        {
            Produto produto = await context.Produtos.SingleAsync(x => x.ProdutoId == id);
            context.Produtos.Remove(produto);
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