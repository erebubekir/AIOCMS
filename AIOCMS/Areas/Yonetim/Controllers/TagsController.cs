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
using Newtonsoft.Json;

namespace AIOCMS.Areas.Yonetim.Controllers
{
    public class TagsController : Controller
    {
        private CMSDBEntities db = new CMSDBEntities();

        // GET: Yonetim/Tags
        [Yetki(enmYetkiler.Listeleme)]
        public ActionResult Index()
        {
            var tbl_Tags = db.tbl_Tags.AsQueryable();
            if (!KullaniciBilgi.YetkiliMi(enmYetkiler.KaliciSilme, RouteData))
                tbl_Tags = db.tbl_Tags.Where(d => d.SilinmeTarihi == null).AsQueryable();

            return View(tbl_Tags.ToList());
        }

        // GET: Yonetim/Tags/Details/5
        [Yetki(enmYetkiler.Detay)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Tags tbl_Tags = db.tbl_Tags.SingleOrDefault(d => d.Id == id);
            if (tbl_Tags == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Tags);
        }

        // GET: Yonetim/Tags/Create 
        [Yetki(enmYetkiler.Ekleme)]
        public ActionResult Create()
        {
            ViewBag.UstId = new SelectList(db.tbl_Icerik, "Id", "Baslik");
            return View();
        }

        // POST: Yonetim/Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Yetki(enmYetkiler.Ekleme)]
        public string Create(tbl_Tags istek)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (ModelState.IsValid)
            {

                if (!db.tbl_Tags.Any(d => d.Url == istek.Url))
                {
                    istek.OlusturmaTarihi = DateTime.Now;
                    db.tbl_Tags.Add(istek);
                    db.SaveChanges();
                    result["status"] = "success";
                    result["message"] = "Kayıt Başarıyla Eklendi";
                    result["reload"] = "true";
                }
                else
                {
                    result["status"] = "error";
                    result["message"] = "Bu Url Başka Bir Yerde Kullanılmış";
                }

            }
            else
            {
                result["status"] = "error";
                result["message"] = "Bişeyler Eksik Lütfen Tüm Alanları Doldurunuz";
            }

            return JsonConvert.SerializeObject(result);
        }

        // GET: Yonetim/Tags/Edit/5
        [Yetki(enmYetkiler.Duzenleme | enmYetkiler.Ekleme)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Tags tbl_Tags = db.tbl_Tags.SingleOrDefault(d => d.Id == id);
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
        [Yetki(enmYetkiler.Duzenleme | enmYetkiler.Ekleme)]
        public string Edit(tbl_Tags tbl_Tags)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (ModelState.IsValid)
            {

                if (!db.tbl_Tags.Any(d => d.Url == tbl_Tags.Url && d.Id != tbl_Tags.Id))
                {
                    //  tbl_Tags.OlusturmaTarihi = DateTime.Now;
                    tbl_Tags.GuncellemeTarihi = DateTime.Now;
                    db.Entry(tbl_Tags).State = EntityState.Modified;
                    db.SaveChanges();
                    result["status"] = "success";
                    result["message"] = "Kayıt Başarıyla Güncellendi";
                }
                else
                {
                    result["status"] = "error";
                    result["message"] = "Bu Url Başka Bir Yerde Kullanılmış";
                }

            }
            else
            {
                result["status"] = "error";
                result["message"] = "Bişeyler Eksik Lütfen Tüm Alanları Doldurunuz";
            }

            return JsonConvert.SerializeObject(result);
        }

        // GET: Yonetim/Tags/Delete/5
        [Yetki(enmYetkiler.Silme)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Tags tbl_Tags = db.tbl_Tags.SingleOrDefault(d => d.Id == id);
            if (tbl_Tags == null)
            {
                return HttpNotFound();
            }
            tbl_Tags.SilinmeTarihi = DateTime.Now;
            db.Entry(tbl_Tags).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Yonetim/Tags/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Yetki(enmYetkiler.KaliciSilme)]
        public string KaliciSil(int id)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            tbl_Tags tbl_Tags = db.tbl_Tags.SingleOrDefault(d => d.Id == id);
            if (tbl_Tags == null)
            {
                result["status"] = "error";
                result["message"] = "Bişeyler Yanlış Gidiyor";

            }
            else
            {
                db.tbl_Tags.Remove(tbl_Tags);
                db.SaveChanges();
                result["status"] = "success";
                result["message"] = "Başarılı Bir Şekilde Silindi";
                result["reload"] = "true";
            }

            return JsonConvert.SerializeObject(result);
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
