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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.SingleOrDefault(d=>d.Id==id);
            if (tbl_Yorum == null)
            {
                return HttpNotFound();
            }
            tbl_Yorum.SilinmeTarihi = DateTime.Now;
            db.Entry(tbl_Yorum).State = EntityState.Modified;
            db.SaveChanges();
            return View(tbl_Yorum);
        }
 
        [Yetki(enmYetkiler.KaliciSilme)]
        public ActionResult KaliciSil(int id)
        {
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.Find(id);
            db.tbl_Yorum.Remove(tbl_Yorum);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // POST: Yonetim/Yorum/Aktiflik/5
       
        // [ValidateAntiForgeryToken]
        [Yetki(enmYetkiler.Duzenleme | enmYetkiler.Ekleme)]
        public ActionResult Aktiflik(int id)
        {
            tbl_Yorum tbl_Yorum = db.tbl_Yorum.Find(id);
            tbl_Yorum.AktifDurumu = !tbl_Yorum.AktifDurumu;
            db.Entry(tbl_Yorum).State = EntityState.Modified;
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
