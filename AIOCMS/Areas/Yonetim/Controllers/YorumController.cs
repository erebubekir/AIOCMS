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
    public class YorumController : Controller
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
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.Find(id);
            if (tbl_Yorum == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Yorum);
        }

        // GET: Yonetim/Yorum/Delete/5
        [Yetki(enmYetkiler.Silme)]
        public JsonResult Delete(int? id)
        {          
            
            Dictionary<string, string> result = new Dictionary<string, string>();
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.SingleOrDefault(d => d.Id == id);
            if (tbl_Yorum == null || id == null)
            {
                result["status"] = "error";
                result["message"] = "Bişeyler Yanlış Gidiyor";

            }
            else
            {
                if (tbl_Yorum.SilinmeTarihi == null)
                {
                    tbl_Yorum.AktifDurumu = false;
                    tbl_Yorum.SilinmeTarihi = DateTime.Now;
                }
                else
                {
                    tbl_Yorum.AktifDurumu = true;
                    tbl_Yorum.SilinmeTarihi = null;
                }                               
                db.Entry(tbl_Yorum).State = EntityState.Modified;
                db.SaveChanges();
                result["status"] = "success";
                result["message"] = "İşlem Başarılı";
                result["reload"] = "true";
            }
            return Json(result);

        }
 
        [Yetki(enmYetkiler.KaliciSilme)]
        public JsonResult KaliciSil(int id)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.SingleOrDefault(d => d.Id == id);
            if (tbl_Yorum == null)
            {
                result["status"] = "error";
                result["message"] = "Bişeyler Yanlış Gidiyor";

            }
            else
            {
                db.tbl_Yorum.Remove(tbl_Yorum);
                db.SaveChanges();
                result["status"] = "success";
                result["message"] = "Başarılı Bir Şekilde Silindi";
                result["reload"] = "true";
            }
            return Json(result);
        }

        // POST: Yonetim/Yorum/Aktiflik/5
       
        // [ValidateAntiForgeryToken]
        [Yetki(enmYetkiler.Duzenleme)]
        public JsonResult Aktiflik(int id)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();           
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.SingleOrDefault(d => d.Id == id);
            if (tbl_Yorum == null)
            {
                result["status"] = "error";
                result["message"] = "Bişeyler Yanlış Gidiyor";

            }
            else
            {                
                tbl_Yorum.AktifDurumu = !tbl_Yorum.AktifDurumu;
                db.Entry(tbl_Yorum).State = EntityState.Modified;
                db.SaveChanges();
                result["status"] = "success";
                result["message"] = "İşlem Başarılı";
                result["reload"] = "true";
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
