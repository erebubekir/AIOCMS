using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AIOCMS.Areas.Yonetim.Data;
using AIOCMS.Models;

namespace AIOCMS.Areas.Yonetim.Controllers
{
    public class IcerikController : Controller
    {
        private CMSDBEntities2 db = new CMSDBEntities2();

        [Yetki(enmYetkiler.Listeleme)]
        public ActionResult Index()
        {
            var tbl_Icerik = db.tbl_Icerik.AsQueryable();
            if (!KullaniciBilgi.YetkiliMi(enmYetkiler.KaliciSilme, RouteData))
                tbl_Icerik = tbl_Icerik.Where(d => d.SilinmeTarihi == null).AsQueryable();
            if (!KullaniciBilgi.YetkiliMi(enmYetkiler.ButunYetkiler, RouteData))
                tbl_Icerik = tbl_Icerik.Where(d => d.KullaniciId == KullaniciBilgi.Kullanici.Id).AsQueryable();

            tbl_Icerik.Include(t => t.tbl_Icerik2).Include(t => t.tbl_Kullanici);
            return View(tbl_Icerik.ToList());
        }
        [Yetki(enmYetkiler.Detay)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Icerik tbl_Icerik = db.tbl_Icerik.Find(id);
            if (tbl_Icerik == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Icerik);
        }

        [Yetki(enmYetkiler.Ekleme)]
        public ActionResult Create()
        {
            ViewBag.UstId = new SelectList(db.tbl_Icerik, "Id", "Baslik");
            return View();
        }

        // POST: Yonetim/Icerik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Yetki(enmYetkiler.Ekleme)]
        public ActionResult Create(tbl_Icerik tbl_Icerik)
        {
            if (ModelState.IsValid)
            {
                tbl_Icerik.KullaniciId = KullaniciBilgi.Kullanici.Id;
                tbl_Icerik.OlusturmaTarihi = DateTime.Now;               
                db.tbl_Icerik.Add(tbl_Icerik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UstId = new SelectList(db.tbl_Icerik, "Id", "Baslik", tbl_Icerik.UstId);            
            return View(tbl_Icerik);
        }

        [Yetki(enmYetkiler.Duzenleme | enmYetkiler.Ekleme)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var icerikSorgum = db.tbl_Icerik.AsQueryable();
            if (!KullaniciBilgi.YetkiliMi(enmYetkiler.ButunYetkiler, RouteData))
               icerikSorgum= icerikSorgum.Where(d=>d.KullaniciId==KullaniciBilgi.Kullanici.Id);
         
            tbl_Icerik tbl_Icerik = icerikSorgum.SingleOrDefault(d => d.Id == id);
            if (tbl_Icerik == null)
            {
                return HttpNotFound();
            }
            ViewBag.UstId = new SelectList(db.tbl_Icerik, "Id", "Baslik", tbl_Icerik.UstId);
            return View(tbl_Icerik);
        }

        // POST: Yonetim/Icerik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Yetki(enmYetkiler.Duzenleme | enmYetkiler.Ekleme)]
        public ActionResult Edit(tbl_Icerik tbl_Icerik)
        {
            if (ModelState.IsValid)
            {
                tbl_Icerik.GuncellemeTarihi = DateTime.Now;
                db.Entry(tbl_Icerik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UstId = new SelectList(db.tbl_Icerik, "Id", "Baslik", tbl_Icerik.UstId);
            return View(tbl_Icerik);
        }

        // GET: Yonetim/Icerik/Delete/5
        [Yetki(enmYetkiler.Silme)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var icerikSorgum = db.tbl_Icerik.AsQueryable();
            if (!KullaniciBilgi.YetkiliMi(enmYetkiler.ButunYetkiler, RouteData))
                icerikSorgum = icerikSorgum.Where(d => d.KullaniciId == KullaniciBilgi.Kullanici.Id);

            tbl_Icerik tbl_Icerik = icerikSorgum.SingleOrDefault(d => d.Id == id);
            if (tbl_Icerik == null)
            {
                return HttpNotFound();
            }
            tbl_Icerik.SilinmeTarihi = DateTime.Now;
            tbl_Icerik.AktifDurumu = false;
            db.Entry(tbl_Icerik).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Icerik Kalıcı olarak silinir Bütün yetkiler varsa kullanıcının kim olduğuna bakılmaz
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Yetki(enmYetkiler.KaliciSilme)]
        public ActionResult KaliciSil(int id)
        {
            var icerikSorgum = db.tbl_Icerik.AsQueryable();
            if (!KullaniciBilgi.YetkiliMi(enmYetkiler.ButunYetkiler, RouteData))
                icerikSorgum = icerikSorgum.Where(d => d.KullaniciId == KullaniciBilgi.Kullanici.Id);
            tbl_Icerik tbl_Icerik = icerikSorgum.SingleOrDefault(d => d.Id == id);
            if (tbl_Icerik == null)
            {
                return HttpNotFound();
            }
            db.tbl_Icerik.Remove(tbl_Icerik);
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
