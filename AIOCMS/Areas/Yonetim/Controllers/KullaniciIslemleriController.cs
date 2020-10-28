using AIOCMS.Areas.Yonetim.Data;
using AIOCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AIOCMS.Areas.Yonetim.Controllers
{
    /// <summary>
    /// BU Kontroller Kullanıcı giriş, Şifre değiştirme ve kayıt olma işlemlerini gerçekleştirir.
    /// </summary>
    public class KullaniciIslemleriController : Controller
    {
        /// <summary>
        /// Kullanıcı giriş ekranı get isteğine cevap verir
        /// </summary>
        /// <returns></returns>
        public ActionResult Giris()
        {
            return View();
        }
        /// <summary>
        /// Kullanıcı giriş ekranı post isteğine cevap verir, başarılı ise Home controller yonlendirir.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Giris(string KullaniciAdi, string Sifre)
        {
            CMSDBEntities db = new CMSDBEntities();
            if (Request.Cookies["KullaniciAdiCk"] != null)
            {
                KullaniciAdi = Crypto.SifreyiCozAES(Request.Cookies["KullaniciAdiCk"].Value, Crypto.paylasilanAnahtar);
            }
            var kullanici = db.tbl_Kullanici.SingleOrDefault(d => d.KullaniciAdi == KullaniciAdi && d.Sifre == Sifre);
            if (kullanici != null)
            {
                KullaniciSessionModel kulSesMod = new KullaniciSessionModel();
                kulSesMod.Id = kullanici.Id;
                kulSesMod.AdiSoyadi = kullanici.AdiSoyadi;
                kulSesMod.KullaniciAdi = kullanici.KullaniciAdi;
                foreach (var item in kullanici.tbl_KullaniciGrubu.tbl_Izinler)
                {
                    kulSesMod.Yetkiler.Add(item.KontrollerAdi, item.Yetkiler);
                }
                Session["Kullanici"] = kulSesMod;
                Session["BitisTarihi"] = DateTime.Now.AddHours(1);
                Session.Timeout = 10;

                HttpCookie ck = new HttpCookie("KullaniciAdiCk", Crypto.SifreleAES(kullanici.KullaniciAdi, Crypto.paylasilanAnahtar));
                ck.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(ck);
                return Redirect("/Yonetim");
            }
            if (Request.Cookies["KullaniciAdiCk"] == null)
                return View();
            return View("OturumGiris");
        }
        /// <summary>
        /// Session Timeout olduğunda bu ekrana yonlenecek
        /// </summary>
        /// <returns></returns>
        public ActionResult OturumGiris()
        {
            return View();
        }
        /// <summary>
        /// Kullanıcının çıkış yapmasınısağlayan Action
        /// </summary>
        /// <returns></returns>
        public ActionResult Cikis()
        {
            Session.RemoveAll();
            Session.Clear();
            HttpContext.Response.Cookies["KullaniciAdiCk"].Expires = DateTime.Now.AddDays(-1);
            return Redirect("/");
        }
    }
}