﻿using Lojinha.MVC.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lojinha.MVC.Controllers
{
    public class FornecedoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fornecedores
        public async Task<ActionResult> Index()
        {
            return View(await db.Fornecedores.ToListAsync());
        }

        // GET: Fornecedores/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = await db.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FornecedorId,Nome,DataModificacao,UsuarioModificacao,DataCriacao,UsuarioCriacao")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Fornecedores.Add(fornecedor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(fornecedor);
        }

        // GET: Fornecedores/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = await db.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FornecedorId,Nome,DataModificacao,UsuarioModificacao,DataCriacao,UsuarioCriacao")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fornecedor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = await db.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Fornecedor fornecedor = await db.Fornecedores.FindAsync(id);
            db.Fornecedores.Remove(fornecedor);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
