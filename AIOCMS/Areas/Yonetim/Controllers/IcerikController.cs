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
    public class IcerikController : BaseController
    {
        private CMSDBEntities db = new CMSDBEntities();

        [Yetki(enmYetkiler.Listeleme)]
        public ActionResult Index()
        {
            var tbl_Icerik = db.tbl_Icerik.AsQueryable();
            if (!KullaniciBilgi.YetkiliMi(enmYetkiler.KaliciSilme, RouteData))
                tbl_Icerik = tbl_Icerik.Where(d => d.SilinmeTarihi == null).AsQueryable();
            if (!KullaniciBilgi.YetkiliMi(enmYetkiler.ButunYetkiler, RouteData))
                tbl_Icerik = tbl_Icerik.Where(d => d.KullaniciId == KullaniciBilgi.Kullanici.Id).AsQueryable();
            return View(tbl_Icerik.Include(t => t.tbl_Icerik2).Include(t => t.tbl_Kullanici).ToList());
        }
        [Yetki(enmYetkiler.Detay)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Icerik tbl_Icerik = db.tbl_Icerik.SingleOrDefault(d => d.Id == id);
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
        public JsonResult Create(tbl_Icerik tbl_Icerik)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(tbl_Icerik.Baslik))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Başlık Alanı Boş Geçilemez");
                }
                else if (string.IsNullOrEmpty(tbl_Icerik.Icerik))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Içerik Alanı Boş Geçilemez");
                }
                else if (string.IsNullOrEmpty(tbl_Icerik.Url))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Url Alanı Boş Geçilemez");
                }
                else if (db.tbl_Icerik.Any(d => d.Url == tbl_Icerik.Url))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Bu Url Başka Bir Yerde Kullanılmış");
                }
                else if (string.IsNullOrEmpty(tbl_Icerik.Resim))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Resim Alanı Boş Geçilemez");
                }
                else
                {
                    tbl_Icerik.KullaniciId = KullaniciBilgi.Kullanici.Id;
                    tbl_Icerik.OlusturmaTarihi = DateTime.Now;
                    db.tbl_Icerik.Add(tbl_Icerik);
                    db.SaveChanges();
                    result
                       .Status(enmStatus.success)
                       .Href("Icerik")
                       .Message("Kayıt Başarıyla Eklendi");

                }               
                
            }
            else
            {
                result
                    .Status(enmStatus.success)
                    .Message("Bişeyler Eksik Lütfen Tüm Alanları Doldurunuz");

            }

           ViewBag.UstId = new SelectList(db.tbl_Icerik, "Id", "Baslik", tbl_Icerik.UstId);
            return Json(result);
        }

        [HttpPost]
        [Yetki(enmYetkiler.Duzenleme)]
        public JsonResult Status(int id)
        {

            tbl_Icerik tbl_Icerik = db.tbl_Icerik.SingleOrDefault(d => d.Id == id);
            if (tbl_Icerik == null)
            {
                result
                    .Status(enmStatus.error)
                    .Message("Bişeyler Yanlış Gidiyor");


            }
            else
            {
                tbl_Icerik.AktifDurumu = !tbl_Icerik.AktifDurumu;
                db.Entry(tbl_Icerik).State = EntityState.Modified;
                db.SaveChanges();
                result
                  .Status(enmStatus.success)
                  .Reload();

            }
            return Json(result);
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
        public JsonResult Edit(tbl_Icerik tbl_Icerik)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(tbl_Icerik.Baslik))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Başlık Alanı Boş Geçilemez");
                }
                else if (string.IsNullOrEmpty(tbl_Icerik.Icerik))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Içerik Alanı Boş Geçilemez");
                }
                else if (string.IsNullOrEmpty(tbl_Icerik.Url))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Url Alanı Boş Geçilemez");
                }
                else if (db.tbl_Icerik.Any(d => d.Url == tbl_Icerik.Url && d.Id != tbl_Icerik.Id))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Bu Url Başka Bir Yerde Kullanılmış");
                }
                else if (string.IsNullOrEmpty(tbl_Icerik.Resim))
                {
                    result
                        .Status(enmStatus.warning)
                        .Message("Resim Alanı Boş Geçilemez");
                }
                else
                {
                    var data = db.tbl_Icerik.SingleOrDefault(d => d.Id == tbl_Icerik.Id);
                    data.Baslik = tbl_Icerik.Baslik;
                    data.Icerik = tbl_Icerik.Icerik;
                    data.Url = tbl_Icerik.Url;
                    data.Resim = tbl_Icerik.Resim;
                    data.UstId = tbl_Icerik.UstId;
                    data.OnayDurumu = tbl_Icerik.OnayDurumu;
                    data.IcerikTipi = tbl_Icerik.IcerikTipi;
                    data.AktifDurumu = tbl_Icerik.AktifDurumu;
                    data.OkunmaSuresi = tbl_Icerik.OkunmaSuresi;
                    data.OkunmaSayisi = tbl_Icerik.OkunmaSayisi;
                    data.GuncellemeTarihi = DateTime.Now;
                    db.Entry(data).State = EntityState.Modified;
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

            ViewBag.UstId = new SelectList(db.tbl_Icerik, "Id", "Baslik", tbl_Icerik.UstId);
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
            var icerikSorgum = db.tbl_Icerik.AsQueryable();
            if (!KullaniciBilgi.YetkiliMi(enmYetkiler.ButunYetkiler, RouteData))
                icerikSorgum = icerikSorgum.Where(d => d.KullaniciId == KullaniciBilgi.Kullanici.Id);

            tbl_Icerik tbl_Icerik = icerikSorgum.SingleOrDefault(d => d.Id == id);
            if (tbl_Icerik == null)
            {
                result
                 .Status(enmStatus.error)
                 .Message("Bişeyler Yanlış Gidiyor");
            }
            else
            {
                tbl_Icerik.SilinmeTarihi = DateTime.Now;
                //tbl_Icerik.AktifDurumu = false;
                db.Entry(tbl_Icerik).State = EntityState.Modified;
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
            tbl_Icerik tbl_Icerik = db.tbl_Icerik.SingleOrDefault(d => d.Id == id);
            if (tbl_Icerik == null)
            {
                result
                  .Status(enmStatus.error)
                  .Message("Bişeyler Yanlış Gidiyor");

            }
            else
            {
                tbl_Icerik.SilinmeTarihi = null;
                db.Entry(tbl_Icerik).State = EntityState.Modified;
                db.SaveChanges();
                result
                  .Status(enmStatus.success)
                  .Message("Başarıyla Geri Yüklendi")
                  .Reload();

            }

            return Json(result);
        }
          

        /// <summary>
        /// Icerik Kalıcı olarak silinir Bütün yetkiler varsa kullanıcının kim olduğuna bakılmaz
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Yetki(enmYetkiler.KaliciSilme)]
        public JsonResult KaliciSil(int id)
        {
            var icerikSorgum = db.tbl_Icerik.AsQueryable();
            if (!KullaniciBilgi.YetkiliMi(enmYetkiler.ButunYetkiler, RouteData))
                icerikSorgum = icerikSorgum.Where(d => d.KullaniciId == KullaniciBilgi.Kullanici.Id);
            tbl_Icerik tbl_Icerik = icerikSorgum.SingleOrDefault(d => d.Id == id);
            if (tbl_Icerik == null)
            {
                result
                   .Status(enmStatus.error)
                   .Message("Bişeyler Yanlış Gidiyor");
            }
            else
            {              
                foreach (var item in tbl_Icerik.tbl_Icerik1.ToList())
                {
                    if (tbl_Icerik.tbl_Icerik2 != null)
                        item.UstId = tbl_Icerik.tbl_Icerik2.Id;
                    else
                        item.UstId = null;
                    db.Entry(item).State = EntityState.Modified;                   
                }
                db.tbl_Yorum.RemoveRange(tbl_Icerik.tbl_Yorum);
                db.tbl_Icerik.Remove(tbl_Icerik);
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
