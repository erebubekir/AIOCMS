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
    public class TagsController : Controller
    {
        private CMSDBEntities db = new CMSDBEntities();

        // GET: Yonetim/Tags
        public ActionResult Index()
        {
            return View(db.tbl_Tags.ToList());
        }

        // GET: Yonetim/Tags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Tags tbl_Tags = db.tbl_Tags.Find(id);
            if (tbl_Tags == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Tags);
        }

        // GET: Yonetim/Tags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yonetim/Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,OlusturmaTarihi,GuncellemeTarihi,SilinmeTarihi,AktifDurumu,Url")] tbl_Tags tbl_Tags)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Tags.Add(tbl_Tags);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Tags);
        }

        // GET: Yonetim/Tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Tags tbl_Tags = db.tbl_Tags.Find(id);
            if (tbl_Tags == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Tags);
        }

        // POST: Yonetim/Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,OlusturmaTarihi,GuncellemeTarihi,SilinmeTarihi,AktifDurumu,Url")] tbl_Tags tbl_Tags)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Tags).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Tags);
        }

        // GET: Yonetim/Tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Tags tbl_Tags = db.tbl_Tags.Find(id);
            if (tbl_Tags == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Tags);
        }

        // POST: Yonetim/Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Tags tbl_Tags = db.tbl_Tags.Find(id);
            db.tbl_Tags.Remove(tbl_Tags);
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
