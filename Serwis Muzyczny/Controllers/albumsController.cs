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
            ViewBag.NazwaAlbumu = db.album.Where(x => x.albumId == id).Select(x => x.nazwa).ToList().ElementAt(0).ToString();

            return View(album);
        }

        public ActionResult SongsFromAlbum(int? id)
        {
            ViewBag.ReturnUrl = db.album.Where(x => x.albumId == id).Select(x => x.nazwa).ToList().ElementAt(0).ToString();
            return View(db.utwory_z_albumu(id));
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
            ViewBag.Exception = null;
            if (ModelState.IsValid)
            {
                db.album.Add(album);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    if (e.InnerException == null)
                        ViewBag.Exception = "Niepoprawne dane albumu!";
                    else
                        ViewBag.Exception = e.InnerException.InnerException.Message;
                    Debug.WriteLine(e.InnerException.InnerException.Message);
                    ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim");
                    ViewBag.gatunekId = new SelectList(db.gatunek, "gatunekId", "nazwa");

                    return View(album);
                }
                return RedirectToAction("Index");
            }
            ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim", album.artystaId);
            ViewBag.gatunekId = new SelectList(db.gatunek, "gatunekId", "nazwa", album.gatunekId);
            return View(album);
        }

        // GET: albums/Create
        public ActionResult CreateByProcedure()
        {
            ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim");
            ViewBag.gatunekId = new SelectList(db.gatunek, "gatunekId", "nazwa");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateByProcedure([Bind(Include = "albumId,nazwa,dataWydania,gatunekId,artystaId")] album album)
        {
            ViewBag.Exception = null;
            if (ModelState.IsValid)
            {
                //var gatun = db.gatunek.Where(x => x.gatunekId == album.gatunekId).Select(y => y.nazwa).ToList().ElementAt(0).ToString();
                //var pseudo = db.artysta.Where(x => x.artystaId == album.artystaId).Select(y => y.pseudonim).ToList().ElementAt(0).ToString();
                //int retCode = db.dodaj_album(album.nazwa, album.dataWydania, gatun, pseudo);
                // To co zakomentowałem było powodem bug'a, nie łapało poprawnie wyjątku z bd 
                db.album.Add(album);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    if (e.InnerException == null)
                        ViewBag.Exception = "Niepoprawne dane albumu!";
                    else
                        ViewBag.Exception = e.InnerException.InnerException.Message;
                    Debug.WriteLine(e.InnerException.InnerException.Message);
                    ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim");
                    ViewBag.gatunekId = new SelectList(db.gatunek, "gatunekId", "nazwa");

                    return View(album);
                }
                return RedirectToAction("Index");
            }
            ViewBag.artystaId = new SelectList(db.artysta, "artystaId", "pseudonim", album.artystaId);
            ViewBag.gatunekId = new SelectList(db.gatunek, "gatunekId", "nazwa", album.gatunekId);
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

            ViewBag.NazwaAlbumu = db.album.Where(x => x.albumId == id).Select(x => x.nazwa).ToList().ElementAt(0).ToString();

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.usun_album(id);
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