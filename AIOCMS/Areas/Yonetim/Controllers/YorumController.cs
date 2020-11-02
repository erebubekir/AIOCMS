using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AIOCMS.Models;

namespace AIOCMS.Areas.Yonetim.Controllers
{
    public class YorumController : Controller
    {
        private CMSDBEntities db = new CMSDBEntities();

        // GET: Yonetim/Yorum
        public ActionResult Index()
        {
            var tbl_Yorum = db.tbl_Yorum.Include(t => t.tbl_Icerik).Include(t => t.tbl_Yorum2);
            return View(tbl_Yorum.ToList());
        }

        // GET: Yonetim/Yorum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.Find(id);
            if (tbl_Yorum == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Yorum);
        }

        // GET: Yonetim/Yorum/Create
        public ActionResult Create()
        {
            ViewBag.IcerikId = new SelectList(db.tbl_Icerik, "Id", "Baslik");
            ViewBag.UstId = new SelectList(db.tbl_Yorum, "Id", "AdiSoyadi");
            return View();
        }

        // POST: Yonetim/Yorum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AdiSoyadi,Yorum,Puan,UstId,IcerikId,OlusturmaTarihi,GuncellemeTarihi,SilinmeTarihi,AktifDurumu,BegeniSayisi,BegenmemeSayisi")] tbl_Yorum tbl_Yorum)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Yorum.Add(tbl_Yorum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IcerikId = new SelectList(db.tbl_Icerik, "Id", "Baslik", tbl_Yorum.IcerikId);
            ViewBag.UstId = new SelectList(db.tbl_Yorum, "Id", "AdiSoyadi", tbl_Yorum.UstId);
            return View(tbl_Yorum);
        }

        // GET: Yonetim/Yorum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.Find(id);
            if (tbl_Yorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.IcerikId = new SelectList(db.tbl_Icerik, "Id", "Baslik", tbl_Yorum.IcerikId);
            ViewBag.UstId = new SelectList(db.tbl_Yorum, "Id", "AdiSoyadi", tbl_Yorum.UstId);
            return View(tbl_Yorum);
        }

        // POST: Yonetim/Yorum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AdiSoyadi,Yorum,Puan,UstId,IcerikId,OlusturmaTarihi,GuncellemeTarihi,SilinmeTarihi,AktifDurumu,BegeniSayisi,BegenmemeSayisi")] tbl_Yorum tbl_Yorum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Yorum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IcerikId = new SelectList(db.tbl_Icerik, "Id", "Baslik", tbl_Yorum.IcerikId);
            ViewBag.UstId = new SelectList(db.tbl_Yorum, "Id", "AdiSoyadi", tbl_Yorum.UstId);
            return View(tbl_Yorum);
        }

        // GET: Yonetim/Yorum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.Find(id);
            if (tbl_Yorum == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Yorum);
        }

        // POST: Yonetim/Yorum/Delete/5
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.Find(id);
            db.tbl_Yorum.Remove(tbl_Yorum);
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
