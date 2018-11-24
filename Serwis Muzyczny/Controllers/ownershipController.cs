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
    public class ownershipController : Controller
    {
        private SerwisMuzycznyEntities db = new SerwisMuzycznyEntities();        // GET: ownership
        public ActionResult Index()
        {
            var przynaleznosc = db.przynaleznosc.Include(p => p.album).Include(p => p.utwor);
            return View(przynaleznosc.ToList());
        }

        // GET: ownership/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            przynaleznosc przynaleznosc = db.przynaleznosc.Find(id);
            if (przynaleznosc == null)
            {
                return HttpNotFound();
            }
            return View(przynaleznosc);
        }

        // GET: ownership/Create
        public ActionResult Create()
        {
            ViewBag.albumId = new SelectList(db.album, "albumId", "nazwa");
            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa");
            return View();
        }

        // POST: ownership/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "przynaleznoscId,albumId,utworId")] przynaleznosc przynaleznosc)
        {
            if (ModelState.IsValid)
            {
                db.przynaleznosc.Add(przynaleznosc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.albumId = new SelectList(db.album, "albumId", "nazwa", przynaleznosc.albumId);
            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa", przynaleznosc.utworId);
            return View(przynaleznosc);
        }

        // GET: ownership/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            przynaleznosc przynaleznosc = db.przynaleznosc.Find(id);
            if (przynaleznosc == null)
            {
                return HttpNotFound();
            }
            ViewBag.albumId = new SelectList(db.album, "albumId", "nazwa", przynaleznosc.albumId);
            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa", przynaleznosc.utworId);
            return View(przynaleznosc);
        }

        // POST: ownership/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "przynaleznoscId,albumId,utworId")] przynaleznosc przynaleznosc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przynaleznosc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.albumId = new SelectList(db.album, "albumId", "nazwa", przynaleznosc.albumId);
            ViewBag.utworId = new SelectList(db.utwor, "utworId", "nazwa", przynaleznosc.utworId);
            return View(przynaleznosc);
        }

        // GET: ownership/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            przynaleznosc przynaleznosc = db.przynaleznosc.Find(id);
            if (przynaleznosc == null)
            {
                return HttpNotFound();
            }
            return View(przynaleznosc);
        }

        // POST: ownership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            przynaleznosc przynaleznosc = db.przynaleznosc.Find(id);
            db.przynaleznosc.Remove(przynaleznosc);
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
