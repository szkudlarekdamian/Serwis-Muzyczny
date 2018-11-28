using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Serwis_Muzyczny.Models;

namespace Serwis_Muzyczny.Controllers
{
    public class plansAndUsersController : Controller
    {
        private SerwisMuzycznyEntities db = new SerwisMuzycznyEntities();        // GET: plansAndUsers
        public ActionResult Index()
        {
            var planUzytkownik = db.planUzytkownik.Include(p => p.plany).Include(p => p.uzytkownik);
            return View(planUzytkownik.ToList());
        }

        // GET: plansAndUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planUzytkownik planUzytkownik = db.planUzytkownik.Find(id);
            if (planUzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(planUzytkownik);
        }

        // GET: plansAndUsers/Create
        public ActionResult Create()
        {
            ViewBag.planId = new SelectList(db.plany, "planId", "nazwa");
            ViewBag.uzytkownikId = new SelectList(db.uzytkownik, "uzytkownikId", "uzytkownikId");
            return View();
        }

        // POST: plansAndUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "planUzytkownikid,uzytkownikId,planId")] planUzytkownik planUzytkownik)
        {
            if (ModelState.IsValid)
            {
                planUzytkownik.dataKupnaPakietu = DateTime.Now;
                Debug.WriteLine("czas " + planUzytkownik.dataKupnaPakietu);
                Debug.WriteLine("Plan Id " + planUzytkownik.planId);
                Debug.WriteLine("uzytkownikID " + planUzytkownik.uzytkownikId);
                
                db.planUzytkownik.Add(planUzytkownik);
                try
                {
                    db.SaveChanges();
                }
                catch(Exception e)
                {
                    if(e.InnerException == null)
                        ViewBag.Exception = "Nieoczekiwany błąd.";
                    else
                        ViewBag.Exception = e.InnerException.InnerException.Message;

                    ViewBag.planId = new SelectList(db.plany, "planId", "nazwa", planUzytkownik.planId);
                    ViewBag.uzytkownikId = new SelectList(db.uzytkownik, "uzytkownikId", "uzytkownikId", planUzytkownik.uzytkownikId);
                    return View(planUzytkownik);
                }
                return RedirectToAction("Index");
            }

            ViewBag.planId = new SelectList(db.plany, "planId", "nazwa", planUzytkownik.planId);
            ViewBag.uzytkownikId = new SelectList(db.uzytkownik, "uzytkownikId", "uzytkownikId", planUzytkownik.uzytkownikId);
            return View(planUzytkownik);
        }

        // GET: plansAndUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planUzytkownik planUzytkownik = db.planUzytkownik.Find(id);
            if (planUzytkownik == null)
            {
                return HttpNotFound();
            }
            ViewBag.planId = new SelectList(db.plany, "planId", "nazwa", planUzytkownik.planId);
            ViewBag.uzytkownikId = new SelectList(db.uzytkownik, "uzytkownikId", "uzytkownikId", planUzytkownik.uzytkownikId);
            return View(planUzytkownik);
        }

        // POST: plansAndUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "planUzytkownikid,uzytkownikId,planId,dataKupnaPakietu,rabat")] planUzytkownik planUzytkownik)
        {
            var x = planUzytkownik.rabat;
            if (ModelState.IsValid)
            {
                db.Entry(planUzytkownik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.planId = new SelectList(db.plany, "planId", "nazwa", planUzytkownik.planId);
            ViewBag.uzytkownikId = new SelectList(db.uzytkownik, "uzytkownikId", "uzytkownikId", planUzytkownik.uzytkownikId);
            return View(planUzytkownik);
        }

        // GET: plansAndUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planUzytkownik planUzytkownik = db.planUzytkownik.Find(id);
            if (planUzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(planUzytkownik);
        }

        // POST: plansAndUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            planUzytkownik planUzytkownik = db.planUzytkownik.Find(id);
            db.planUzytkownik.Remove(planUzytkownik);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteConfirmedView(string id)
        {
            planUzytkownik planUzytkownik = db.planUzytkownik.Find(int.Parse(id));
            var z = planUzytkownik.uzytkownikId;
            db.planUzytkownik.Remove(planUzytkownik);
            db.SaveChanges();
            return RedirectToAction("PlansHistory","users", new { id = z });
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
