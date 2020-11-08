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
    /// Yorum işlmeleri
    /// </summary>   
    public class YorumController : BaseController
    {
        private CMSDBEntities db = new CMSDBEntities();

        // GET: Yonetim/Yorum
        [Yetki(enmYetkiler.Listeleme)]
        public ActionResult Index()
        {
            var tbl_Yorum = db.tbl_Yorum.AsQueryable();
            if (!KullaniciBilgi.YetkiliMi(enmYetkiler.KaliciSilme, RouteData))
                tbl_Yorum = tbl_Yorum.Where(d => d.SilinmeTarihi == null);
            return View(tbl_Yorum.Include(t => t.tbl_Icerik).ToList());
        }

        // GET: Yonetim/Yorum/Details/5
        [Yetki(enmYetkiler.Detay)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.SingleOrDefault(d => d.Id == id);
            if (tbl_Yorum == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Yorum);
        }

        [HttpPost]
        [Yetki(enmYetkiler.Duzenleme)]
        public JsonResult Status(int id)
        {

            tbl_Yorum tbl_Yorum = db.tbl_Yorum.SingleOrDefault(d => d.Id == id);
            if (tbl_Yorum == null)
            {
                result
                    .Status(enmStatus.error)
                    .Message("Bişeyler Yanlış Gidiyor");


            }
            else
            {
                tbl_Yorum.AktifDurumu = !tbl_Yorum.AktifDurumu;
                db.Entry(tbl_Yorum).State = EntityState.Modified;
                db.SaveChanges();
                result
                  .Status(enmStatus.success)
                  .Reload();

            }
            return Json(result);
        }

        // GET: Yonetim/Yorum/Delete/5
        [Yetki(enmYetkiler.Silme)]
        public JsonResult Delete(int? id)
        {
            if (id == null)
            {
                result
                    .Status(enmStatus.error)
                    .Message("Bişeyler Yanlış Gidiyor");

            }
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.SingleOrDefault(d => d.Id == id);
            if (tbl_Yorum == null)
            {
                result
                   .Status(enmStatus.error)
                   .Message("Bişeyler Yanlış Gidiyor");

            }
            else
            {                
                //tbl_Yorum.AktifDurumu = false;
                tbl_Yorum.SilinmeTarihi = DateTime.Now;
                db.Entry(tbl_Yorum).State = EntityState.Modified;
                db.SaveChanges();
                result
                   .Status(enmStatus.success)
                   .Message("Başarıyla Geri Dönüşüme Gönderildi")
                   .Reload();
            }
            return Json(result);

        }


        // GET: Yonetim/Tags/Delete/5
        [HttpPost]
        [Yetki(enmYetkiler.Silme)]
        public JsonResult GeriAl(int? id)
        {
            if (id == null)
            {
                result
                    .Status(enmStatus.error)
                    .Message("Bişeyler Yanlış Gidiyor");

            }
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.SingleOrDefault(d => d.Id == id);
            if (tbl_Yorum == null)
            {
                result
                  .Status(enmStatus.error)
                  .Message("Bişeyler Yanlış Gidiyor");

            }
            else
            {
                tbl_Yorum.SilinmeTarihi = null;
                db.Entry(tbl_Yorum).State = EntityState.Modified;
                db.SaveChanges();
                result
                  .Status(enmStatus.success)
                  .Message("Başarıyla Geri Yüklendi")
                  .Reload();

            }

            return Json(result);
        }

        public void AltYorumSil(tbl_Yorum tbl_Yorum)
        {
            if (tbl_Yorum != null)
            {
                foreach (var item in tbl_Yorum.tbl_Yorum1)
                {
                    AltYorumSil(item);
                }
                db.tbl_Yorum.RemoveRange(tbl_Yorum.tbl_Yorum1);                
            }            
        }

        [Yetki(enmYetkiler.KaliciSilme)]
        public JsonResult KaliciSil(int id)
        {
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.SingleOrDefault(d => d.Id == id);

            if (tbl_Yorum == null)
            {
                result
                   .Status(enmStatus.error)
                   .Message("Bişeyler Yanlış Gidiyor");

            }
            else
            {      
                
                
                AltYorumSil(tbl_Yorum);
                db.tbl_Yorum.Remove(tbl_Yorum);
                db.SaveChanges();
                result
                .Status(enmStatus.success)
                .Message("Başarılı Bir Şekilde Silindi")
                .Reload();
            }
            return Json(result);
        }

        /*
        // POST: Yonetim/Yorum/Aktiflik/5       
        // [ValidateAntiForgeryToken]
        [Yetki(enmYetkiler.Duzenleme)]
        public JsonResult Aktiflik(int id)
        {                
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.SingleOrDefault(d => d.Id == id);
            if (tbl_Yorum == null)
            {
                result
                  .Status(enmStatus.error)
                  .Message("Bişeyler Yanlış Gidiyor");
            }
            else
            {                
                tbl_Yorum.AktifDurumu = !tbl_Yorum.AktifDurumu;
                db.Entry(tbl_Yorum).State = EntityState.Modified;
                db.SaveChanges();
                result
                  .Status(enmStatus.success)
                  .Message("Başarılı Bir Şekilde Silindi")
                  .Reload();
            }
            return Json(result);        
        }*/

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
