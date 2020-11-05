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
    /// <summary>
    /// Kullanıcı Grup işlemleri
    /// </summary>
    [Yetki(enmYetkiler.ButunYetkiler)]
    public class KullaniciGrubuController : Controller
    {
        private CMSDBEntities db = new CMSDBEntities();

        // GET: Yonetim/KullaniciGrubu
        public ActionResult Index()
        {
            return View(db.tbl_KullaniciGrubu.ToList());
        }

        // GET: Yonetim/KullaniciGrubu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_KullaniciGrubu tbl_KullaniciGrubu = db.tbl_KullaniciGrubu.Find(id);
            if (tbl_KullaniciGrubu == null)
            {
                return HttpNotFound();
            }
            return View(tbl_KullaniciGrubu);
        }

        // GET: Yonetim/KullaniciGrubu/Create
        public ActionResult Create()
        {
            
            return View(new tbl_KullaniciGrubu());
        }

        // POST: Yonetim/KullaniciGrubu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( tbl_KullaniciGrubu model)
        {
        
            if (ModelState.IsValid)
            {
                model.OlusturmaTarihi = DateTime.Now;
                
                foreach (var item in model.tbl_Izinler)
                {
                   
                    item.Yetkiler = item.SYetki.ToStringBitInt();
                    item.OlusturmaTarihi = DateTime.Now;
                    item.AktifDurumu = true;
                }
                db.tbl_KullaniciGrubu.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Yonetim/KullaniciGrubu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_KullaniciGrubu tbl_KullaniciGrubu = db.tbl_KullaniciGrubu.Find(id);
            if (tbl_KullaniciGrubu == null)
            {
                return HttpNotFound();
            }
            return View(tbl_KullaniciGrubu);
        }

        // POST: Yonetim/KullaniciGrubu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_KullaniciGrubu model)
        {
            var degistirilecekModel = db.tbl_KullaniciGrubu.FirstOrDefault(d => d.Id == model.Id);
            if (degistirilecekModel == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                degistirilecekModel.GuncellemeTarihi = DateTime.Now;
                degistirilecekModel.Adi = model.Adi;
                degistirilecekModel.AktifDurumu = model.AktifDurumu;
                foreach (var item in degistirilecekModel.tbl_Izinler)
                {
                    item.Yetkiler = model.tbl_Izinler.FirstOrDefault(d => d.Id == item.Id).SYetki.ToStringBitInt();
                    item.GuncellenmeTarihi = DateTime.Now;
                }
                db.Entry(degistirilecekModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(degistirilecekModel);
        }

        // GET: Yonetim/KullaniciGrubu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_KullaniciGrubu tbl_KullaniciGrubu = db.tbl_KullaniciGrubu.Find(id);
            if (tbl_KullaniciGrubu == null)
            {
                return HttpNotFound();
            }
            return View(tbl_KullaniciGrubu);
        }

        // POST: Yonetim/KullaniciGrubu/Delete/5
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_KullaniciGrubu tbl_KullaniciGrubu = db.tbl_KullaniciGrubu.Find(id);
            db.tbl_KullaniciGrubu.Remove(tbl_KullaniciGrubu);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
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
