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
    public class performancesController : Controller
    {
        private SerwisMuzycznyEntities db = new SerwisMuzycznyEntities();

        // GET: performances
        public ActionResult Index()
        {
            var wykonanie = db.wykonanie.Include(w => w.artysta).Include(w => w.utwor);
            return View(wykonanie.ToList());
        }

        // GET: performances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wykonanie wykonanie = db.wykonanie.Find(id);
            if (wykonanie == null)
            {
                return HttpNotFound();
            }
            return View(wykonanie);
        }

        // GET: performances/Create
        public ActionResult Create()
        {
            ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim");
            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa");
            return View();
        }

        // POST: performances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "wykonanieId,artystaId,utworId")] wykonanie wykonanie)
        {
            if (ModelState.IsValid)
            {
                db.wykonanie.Add(wykonanie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim", wykonanie.artystaId);
            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa", wykonanie.utworId);
            return View(wykonanie);
        }

        // GET: performances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wykonanie wykonanie = db.wykonanie.Find(id);
            if (wykonanie == null)
            {
                return HttpNotFound();
            }
            ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim", wykonanie.artystaId);
            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa", wykonanie.utworId);
            return View(wykonanie);
        }

        // POST: performances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "wykonanieId,artystaId,utworId")] wykonanie wykonanie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wykonanie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim", wykonanie.artystaId);
            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa", wykonanie.utworId);
            return View(wykonanie);
        }

        // GET: performances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wykonanie wykonanie = db.wykonanie.Find(id);
            if (wykonanie == null)
            {
                return HttpNotFound();
            }
            return View(wykonanie);
        }

        // POST: performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            wykonanie wykonanie = db.wykonanie.Find(id);
            db.wykonanie.Remove(wykonanie);
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
