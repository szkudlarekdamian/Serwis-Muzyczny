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
    public class albumsController : Controller
    {
        private SerwisMuzycznyEntities db = new SerwisMuzycznyEntities();

        // GET: albums
        public ActionResult Index()
        {
            var album = db.album.Include(a => a.artysta).Include(a => a.gatunek);
            return View(album.ToList());
        }

        // GET: albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            album album = db.album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        
        // GET: albums/Create
        public ActionResult Create()
        {
            ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim");
            ViewBag.gatunekId = new SelectList(db.gatunek, "gatunekId", "nazwa");
            return View();
        }

        // POST: albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "albumId,nazwa,dataWydania,gatunekId,artystaId")] album album)
        {
            if (ModelState.IsValid)
            {
                db.album.Add(album);
                ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim", album.artystaId);
                ViewBag.gatunekId = new SelectList(db.gatunek, "gatunekId", "nazwa", album.gatunekId);
                try
                {
                    db.SaveChanges();
                    //LINQ lub procedura
                    
                    int retCode = db.dodaj_album(album.nazwa, album.dataWydania, ViewBag.gatunekId, ViewBag.artystaId);
                }
                catch(Exception e)
                {
                    if(e.InnerException == null)
                        ViewBag.Exception = "Niepoprawne dane albumu!";
                    else
                        ViewBag.Exception = e.InnerException.InnerException.Message;
                }
                return RedirectToAction("Index");
            }

            
            return View(album);
        }

        // GET: albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            album album = db.album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim", album.artystaId);
            ViewBag.gatunekId = new SelectList(db.gatunek, "gatunekId", "nazwa", album.gatunekId);
            return View(album);
        }

        // POST: albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "albumId,nazwa,dataWydania,gatunekId,artystaId")] album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim", album.artystaId);
            ViewBag.gatunekId = new SelectList(db.gatunek, "gatunekId", "nazwa", album.gatunekId);
            return View(album);
        }

        // GET: albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            album album = db.album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            album album = db.album.Find(id);
            db.album.Remove(album);
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
