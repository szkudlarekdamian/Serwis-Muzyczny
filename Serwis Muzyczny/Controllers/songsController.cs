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
    public class songsController : Controller
    {
        private SerwisMuzycznyEntities db = new SerwisMuzycznyEntities();        // GET: songs
        public ActionResult Index()
        {
            return View(db.utwor.ToList());
        }

        // GET: songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utwor utwor = db.utwor.Find(id);
            if (utwor == null)
            {
                return HttpNotFound();
            }
            return View(utwor);
        }

        // GET: songs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "utworId,nazwa,dlugosc")] utwor utwor)
        {
            if (ModelState.IsValid)
            {
                db.utwor.Add(utwor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(utwor);
        }

        // GET: songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utwor utwor = db.utwor.Find(id);
            if (utwor == null)
            {
                return HttpNotFound();
            }
            return View(utwor);
        }

        // POST: songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "utworId,nazwa,dlugosc")] utwor utwor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utwor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utwor);
        }

        // GET: songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utwor utwor = db.utwor.Find(id);
            if (utwor == null)
            {
                return HttpNotFound();
            }
            return View(utwor);
        }

        // POST: songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            utwor utwor = db.utwor.Find(id);
            db.utwor.Remove(utwor);
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
