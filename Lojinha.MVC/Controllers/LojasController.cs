﻿using Lojinha.MVC.Extensions;
using Lojinha.MVC.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lojinha.MVC.Controllers
{
    public class LojasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Lojas
        public async Task<ActionResult> Index()
        {
            return View(await db.Lojas.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Pesquisar(string filtro)
        {
            if (!filtro.ContainsValue())
                return RedirectToAction("Index");

            return View("Index", await db.Lojas.Where(l => l.Nome.Contains(filtro)).ToListAsync());
        }

        // GET: Lojas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Loja loja = await db.Lojas.FindAsync(id);

            if (loja == null)
                return HttpNotFound();
            
            return View(loja);
        }

        // GET: Lojas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lojas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LojaId,Nome")] Loja loja)
        {
            if (ModelState.IsValid)
            {
                db.Lojas.Add(loja);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loja);
        }

        // GET: Lojas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Loja loja = await db.Lojas.FindAsync(id);

            if (loja == null)
                return HttpNotFound();
            
            return View(loja);
        }

        // POST: Lojas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LojaId,Nome")] Loja loja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loja).State = EntityState.Modified;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loja);
        }

        // GET: Lojas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            

            Loja loja = await db.Lojas.FindAsync(id);

            if (loja == null)
                return HttpNotFound();
            
            return View(loja);
        }

        // POST: Lojas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Loja loja = await db.Lojas.FindAsync(id);

            db.Lojas.Remove(loja);
            await db.SaveChangesAsync();

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
