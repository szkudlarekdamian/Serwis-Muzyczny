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
    public class plansController : Controller
    {
        private SerwisMuzycznyEntities db = new SerwisMuzycznyEntities();

        // GET: plans
        public ActionResult Index()
        {
            return View(db.plany.ToList());
        }

        // GET: plans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plany plany = db.plany.Find(id);
            if (plany == null)
            {
                return HttpNotFound();
            }
            return View(plany);
        }

        // GET: plans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: plans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "planId,iloscPiosenek,cena,nazwa")] plany plany)
        {
            if (ModelState.IsValid)
            {
                db.plany.Add(plany);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plany);
        }

        // GET: plans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plany plany = db.plany.Find(id);
            if (plany == null)
            {
                return HttpNotFound();
            }
            return View(plany);
        }

        // POST: plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "planId,iloscPiosenek,cena,nazwa")] plany plany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plany).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plany);
        }

        // GET: plans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            plany plany = db.plany.Find(id);
            if (plany == null)
            {
                return HttpNotFound();
            }
            return View(plany);
        }

        // POST: plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            plany plany = db.plany.Find(id);
            db.plany.Remove(plany);
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
