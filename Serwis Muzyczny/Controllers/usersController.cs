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
    public class usersController : Controller
    {
        private SerwisMuzycznyEntities db = new SerwisMuzycznyEntities();        // GET: users
        public ActionResult Index()
        {
            return View(db.uzytkownik.ToList());
        }


        public ActionResult PlansHistory(string id)
        {
            return View(db.planyIUzytkownik(id).ToList());
        }

        // GET: users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uzytkownik uzytkownik = db.uzytkownik.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // GET: users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "uzytkownikId,imie,nazwisko,email,kraj,dataUrodzenia,miejscowosc,rodzajMiejscowosci,plec,0")] uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                db.uzytkownik.Add(uzytkownik);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewBag.Exception = e.InnerException.InnerException.Message;
                    return View(uzytkownik);
                }
                return RedirectToAction("Index");
            }

            return View(uzytkownik);
        }

        // GET: users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uzytkownik uzytkownik = db.uzytkownik.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "uzytkownikId,imie,nazwisko,email,kraj,dataUrodzenia,miejscowosc,rodzajMiejscowosci,plec,PozostalaIlosc,row_version")] uzytkownik uzytkownik)
        { 
            if (ModelState.IsValid)
            {
                ViewBag.Exception = null; 
                try
                {
                    db.edycja_uzytkownik(uzytkownik.uzytkownikId, uzytkownik.imie, uzytkownik.nazwisko, uzytkownik.email, uzytkownik.kraj,
                        uzytkownik.dataUrodzenia, uzytkownik.miejscowosc, uzytkownik.rodzajMiejscowosci, uzytkownik.plec,
                        uzytkownik.PozostalaIlosc, uzytkownik.row_version);
                }
                catch(Exception e)
                {
                    if (e.InnerException == null)
                        ViewBag.Exception = "Błąd edycji uzytkownika!";
                    else
                        ViewBag.Exception = e.InnerException.Message;
                    
                    ModelState.Remove("imie");
                    ModelState.Remove("nazwisko");
                    ModelState.Remove("email");
                    ModelState.Remove("kraj");

                    ModelState.Remove("dataUrodzenia");
                    ModelState.Remove("miejscowosc");
                    ModelState.Remove("rodzajMiejscowosci");
                    ModelState.Remove("plec");

                    ModelState.Remove("PozostalaIlosc");
                    ModelState.Remove("row_version");

                    var uzytkownikZaktualizowany = db.uzytkownik.Find(uzytkownik.uzytkownikId);

                    if (uzytkownikZaktualizowany != null)
                    {
                        return View(uzytkownikZaktualizowany);
                    }
                    else
                        return View(uzytkownik);
                    
                    
                }
                return RedirectToAction("Index");
            }
            return View(uzytkownik);
        }

        // GET: users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uzytkownik uzytkownik = db.uzytkownik.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // POST: users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            db.usun_uzytkownika(id);
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
