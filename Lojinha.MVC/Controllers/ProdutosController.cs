﻿using Lojinha.MVC.Extensions;
using Lojinha.MVC.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace Lojinha.MVC.Controllers
{
    public class ProdutosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Produtos
        public async Task<ActionResult> Index()
        {
            return View(await db.Produtos.Include(p => p.Categoria).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Pesquisar(string filtro)
        {
            if (!filtro.ContainsValue())
                return RedirectToAction("Index");

            var produtos = await db.Produtos
                                .Include(p => p.Categoria)
                                .Where(p => p.Nome.Contains(filtro) || p.Categoria.Nome.Contains(filtro))
                                .ToListAsync();

            return View("Index", produtos);
        }

        // GET: Produtos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Produto produto = await db.Produtos.FindAsync(id);

            if (produto == null)
                return HttpNotFound();
            
            return View(produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProdutoId,CategoriaId,Nome")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Produto produto = await db.Produtos.FindAsync(id);

            if (produto == null)
                return HttpNotFound();
            
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProdutoId,CategoriaId,Nome")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Produto produto = await db.Produtos.FindAsync(id);

            if (produto == null)
                return HttpNotFound();

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var scope = new TransactionScope())
            {
                var produto = await db.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id);

                if (produto == null)
                    return HttpNotFound();

                var compraProdutosFornecedores = await db.ComprasFornecedoresProdutos.Where(pf => pf.ProdutoFornecedor.ProdutoId == id).ToListAsync();
                
                if (compraProdutosFornecedores.Count > 0)
                    db.ComprasFornecedoresProdutos.RemoveRange(compraProdutosFornecedores);


                db.Produtos.Remove(produto);
                await db.SaveChangesAsync();

                scope.Complete();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
