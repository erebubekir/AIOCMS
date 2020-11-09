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
    public class TagsController : BaseController
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
        public JsonResult Create(tbl_Tags istek)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(istek.Adi))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Adı Alanı Boş Geçilemez");
                 

                  
                }
                else if (string.IsNullOrEmpty(istek.Url))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Seo Url Alanı Boş Geçilemez");
               
                }
                else if (db.tbl_Tags.Any(d => d.Url == istek.Url))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Bu Url Başka Bir Yerde Kullanılmış");
                }
                else
                {
                    istek.OlusturmaTarihi = DateTime.Now;
                    db.tbl_Tags.Add(istek);
                    db.SaveChanges();
                    result
                        .Status(enmStatus.success)
                        .Href("Tags")
                        .Message("Kayıt Başarıyla Eklendi");
                    
                    
                }

            }
            else
            {
                result
                    .Status(enmStatus.error)
                    .Message("Bişeyler Eksik Lütfen Tüm Alanları Doldurunuz");
           
            }

            return Json(result);
        }


        [HttpPost]
        [Yetki(enmYetkiler.Duzenleme)]
        public JsonResult Status(int id)
        {

            tbl_Tags tbl_Tags = db.tbl_Tags.SingleOrDefault(d => d.Id == id);
            if (tbl_Tags == null)
            {
                result
                    .Status(enmStatus.error)
                    .Message("Bişeyler Yanlış Gidiyor");
           

            }
            else
            {
                tbl_Tags.AktifDurumu = !tbl_Tags.AktifDurumu;
                db.Entry(tbl_Tags).State = EntityState.Modified;
                db.SaveChanges();
                result
                  .Status(enmStatus.success)
                  .Reload();
    
            }
            return Json(result);
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
        public JsonResult Edit(tbl_Tags istek)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(istek.Adi))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Adı Alanı Boş Geçilemez");

                }
                else if (string.IsNullOrEmpty(istek.Url))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Seo Url Alanı Boş Geçilemez");

                }
                else if (db.tbl_Tags.Any(d => d.Url == istek.Url && d.Id != istek.Id))
                {
                   
                    result
                           .Status(enmStatus.warning)
                           .Message("Bu Url Başka Bir Yerde Kullanılmış");
                }
                else
                {
                    istek.GuncellemeTarihi = DateTime.Now;
                    db.Entry(istek).State = EntityState.Modified;
                    db.SaveChanges();
                    result
                        .Status(enmStatus.success)
                        .Message("Kayıt Başarıyla Güncellendi")
                        .Reload();

                }

            }
            else
            {
                result
                    .Status(enmStatus.error)
                    .Message("Bişeyler Eksik Lütfen Tüm Alanları Doldurunuz");
   
            }

            return Json(result);
        }

        
        [HttpPost]
        [Yetki(enmYetkiler.Silme)]
        public JsonResult Delete(int? id)
        {
            if (id == null)
            {
                result
                    .Status(enmStatus.error)
                    .Message("Bişeyler Yanlış Gidiyor");

            }
            tbl_Tags tbl_Tags = db.tbl_Tags.SingleOrDefault(d => d.Id == id);
            if (tbl_Tags == null)
            {
                result
                  .Status(enmStatus.error)
                  .Message("Bişeyler Yanlış Gidiyor");

            }
            else
            {
                tbl_Tags.SilinmeTarihi = DateTime.Now;
                db.Entry(tbl_Tags).State = EntityState.Modified;
                db.SaveChanges();
                result
                  .Status(enmStatus.success)
                  .Message("Başarıyla Geri Dönüşüme Gönderildi")
                  .Reload();

            }

            return Json(result);
        }

        
        [HttpPost]
        [Yetki(enmYetkiler.KaliciSilme)]
        public JsonResult GeriAl(int? id)
        {
            if (id == null)
            {
                result
                    .Status(enmStatus.error)
                    .Message("Bişeyler Yanlış Gidiyor");
            
            }
            tbl_Tags tbl_Tags = db.tbl_Tags.SingleOrDefault(d => d.Id == id);
            if (tbl_Tags == null)
            {
                result
                  .Status(enmStatus.error)
                  .Message("Bişeyler Yanlış Gidiyor");

            }
            else
            {
                tbl_Tags.SilinmeTarihi = null;
                db.Entry(tbl_Tags).State = EntityState.Modified;
                db.SaveChanges();
                result
                  .Status(enmStatus.success)
                  .Message("Başarıyla Geri Yüklendi")
                  .Reload();

            }

            return Json(result);
        }

        // POST: Yonetim/Tags/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Yetki(enmYetkiler.KaliciSilme)]
        public JsonResult KaliciSil(int id)
        {
           
            tbl_Tags tbl_Tags = db.tbl_Tags.SingleOrDefault(d => d.Id == id);
            if (tbl_Tags == null)
            {
                result
                   .Status(enmStatus.error)
                   .Message("Bişeyler Yanlış Gidiyor");
            }
            else
            {
                db.tbl_Tags.Remove(tbl_Tags);
                db.SaveChanges();
                result
                 .Status(enmStatus.success)
                 .Message("Başarılı Bir Şekilde Silindi")
                 .Reload();
     
            }

            return Json(result);
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
