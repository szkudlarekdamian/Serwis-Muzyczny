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
    public class artistsController : Controller
    {
        private SerwisMuzycznyEntities db = new SerwisMuzycznyEntities();
        // GET: artists
        public ActionResult Index()
        {
            return View(db.artysta.ToList());
        }

        // GET: artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artysta artysta = db.artysta.Find(id);
            if (artysta == null)
            {
                return HttpNotFound();
            }
            return View(artysta);
        }

        // GET: artists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "artystaId,pseudonim")] artysta artysta)
        {
            if (ModelState.IsValid)
            {
                db.artysta.Add(artysta);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    if (e.InnerException == null)
                        ViewBag.Exception = "Nieoczekiwany błąd.";
                    else
                        ViewBag.Exception = e.InnerException.InnerException.Message;

                    return View(artysta);
                }
                return RedirectToAction("Index");
            }

            return View(artysta);
        }

        // GET: artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artysta artysta = db.artysta.Find(id);
            if (artysta == null)
            {
                return HttpNotFound();
            }
            return View(artysta);
        }

        // POST: artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "artystaId,pseudonim")] artysta artysta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artysta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artysta);
        }

        // GET: artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artysta artysta = db.artysta.Find(id);
            if (artysta == null)
            {
                return HttpNotFound();
            }
            return View(artysta);
        }

        // POST: artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.usun_artyste(id);
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
        public ActionResult mostPopular(int? id)
        {
            if (id == null)
            {
                return View(db.napopularniejsi_artysci(30).ToList());
            }
            return View(db.napopularniejsi_artysci(id).ToList());
        }
    }
}
