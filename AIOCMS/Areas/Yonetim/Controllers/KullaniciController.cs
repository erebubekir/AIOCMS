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
    [Yetki(enmYetkiler.ButunYetkiler)]
    public class KullaniciController : Controller
    {
        private CMSDBEntities db = new CMSDBEntities();

        // GET: Yonetim/Kullanici
        public ActionResult Index()
        {
            var tbl_Kullanici = db.tbl_Kullanici.Include(t => t.tbl_KullaniciGrubu);
            return View(tbl_Kullanici.ToList());
        }

        // GET: Yonetim/Kullanici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Kullanici tbl_Kullanici = db.tbl_Kullanici.Find(id);
            if (tbl_Kullanici == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Kullanici);
        }

        // GET: Yonetim/Kullanici/Create
        public ActionResult Create()
        {
            ViewBag.KullaniciGrupId = new SelectList(db.tbl_KullaniciGrubu, "Id", "Adi");
            return View();
        }

        // POST: Yonetim/Kullanici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,KullaniciAdi,Sifre,AdiSoyadi,EPosta,OlusturmaTarihi,GuncellemeTarihi,SilinmeTarihi,AktifDurumu,Resim,KullaniciGrupId")] tbl_Kullanici tbl_Kullanici)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Kullanici.Add(tbl_Kullanici);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch 
                {
                    ModelState.AddModelError("KullaniciAdi", "Aynı isimde Kullanıcı adı olamaz");
                }
            }

            ViewBag.KullaniciGrupId = new SelectList(db.tbl_KullaniciGrubu, "Id", "Adi", tbl_Kullanici.KullaniciGrupId);
            return View(tbl_Kullanici);
        }

        // GET: Yonetim/Kullanici/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Kullanici tbl_Kullanici = db.tbl_Kullanici.Find(id);
            if (tbl_Kullanici == null)
            {
                return HttpNotFound();
            }
            ViewBag.KullaniciGrupId = new SelectList(db.tbl_KullaniciGrubu, "Id", "Adi", tbl_Kullanici.KullaniciGrupId);
            return View(tbl_Kullanici);
        }

        // POST: Yonetim/Kullanici/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KullaniciAdi,Sifre,AdiSoyadi,EPosta,OlusturmaTarihi,GuncellemeTarihi,SilinmeTarihi,AktifDurumu,Resim,KullaniciGrupId")] tbl_Kullanici tbl_Kullanici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Kullanici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KullaniciGrupId = new SelectList(db.tbl_KullaniciGrubu, "Id", "Adi", tbl_Kullanici.KullaniciGrupId);
            return View(tbl_Kullanici);
        }

        // GET: Yonetim/Kullanici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Kullanici tbl_Kullanici = db.tbl_Kullanici.Find(id);
            if (tbl_Kullanici == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Kullanici);
        }

        // POST: Yonetim/Kullanici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Kullanici tbl_Kullanici = db.tbl_Kullanici.Find(id);
            db.tbl_Kullanici.Remove(tbl_Kullanici);
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
