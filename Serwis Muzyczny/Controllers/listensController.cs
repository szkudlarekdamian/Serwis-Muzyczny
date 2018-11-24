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
    public class listensController : Controller
    {
        private SerwisMuzycznyEntities db = new SerwisMuzycznyEntities();        // GET: listens
        public ActionResult Index()
        {
            var odsluch = db.odsluch.Include(o => o.utwor).Include(o => o.uzytkownik);
            return View(odsluch.ToList());
        }

        // GET: listens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            odsluch odsluch = db.odsluch.Find(id);
            if (odsluch == null)
            {
                return HttpNotFound();
            }
            return View(odsluch);
        }

        // GET: listens/Create
        public ActionResult Create()
        {
            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa");
            ViewBag.uzytkownikId = new SelectList(db.uzytkownik, "uzytkownikId", "imie");
            return View();
        }

        // POST: listens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "odsluchId,uzytkownikId,utworId,dataOdtworzenia")] odsluch odsluch)
        {
            if (ModelState.IsValid)
            {
                db.odsluch.Add(odsluch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa", odsluch.utworId);
            ViewBag.uzytkownikId = new SelectList(db.uzytkownik, "uzytkownikId", "imie", odsluch.uzytkownikId);
            return View(odsluch);
        }

        // GET: listens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            odsluch odsluch = db.odsluch.Find(id);
            if (odsluch == null)
            {
                return HttpNotFound();
            }
            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa", odsluch.utworId);
            ViewBag.uzytkownikId = new SelectList(db.uzytkownik, "uzytkownikId", "imie", odsluch.uzytkownikId);
            return View(odsluch);
        }

        // POST: listens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "odsluchId,uzytkownikId,utworId,dataOdtworzenia")] odsluch odsluch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(odsluch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa", odsluch.utworId);
            ViewBag.uzytkownikId = new SelectList(db.uzytkownik, "uzytkownikId", "imie", odsluch.uzytkownikId);
            return View(odsluch);
        }

        // GET: listens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            odsluch odsluch = db.odsluch.Find(id);
            if (odsluch == null)
            {
                return HttpNotFound();
            }
            return View(odsluch);
        }

        // POST: listens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            odsluch odsluch = db.odsluch.Find(id);
            db.odsluch.Remove(odsluch);
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
