using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Serwis_Muzyczny.Models;

namespace Serwis_Muzyczny.Controllers
{
    public class genreController : Controller
    {
        private SerwisMuzycznyEntities db = new SerwisMuzycznyEntities();        // GET: genre
        public ActionResult Index()
        {
            return View(db.gatunek.ToList());
        }

        // GET: genre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gatunek gatunek = db.gatunek.Find(id);
            if (gatunek == null)
            {
                return HttpNotFound();
            }
            return View(gatunek);
        }

        // GET: genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: genre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "gatunekId,nazwa")] gatunek gatunek)
        {
            if (ModelState.IsValid)
            {
                db.gatunek.Add(gatunek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gatunek);
        }

        // GET: genre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gatunek gatunek = db.gatunek.Find(id);
            if (gatunek == null)
            {
                return HttpNotFound();
            }
            return View(gatunek);
        }

        // POST: genre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "gatunekId,nazwa")] gatunek gatunek)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gatunek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gatunek);
        }

        // GET: genre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gatunek gatunek = db.gatunek.Find(id);
            if (gatunek == null)
            {
                return HttpNotFound();
            }
            return View(gatunek);
        }

        // POST: genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gatunek gatunek = db.gatunek.Find(id);
            db.gatunek.Remove(gatunek);
            db.SaveChanges();
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
