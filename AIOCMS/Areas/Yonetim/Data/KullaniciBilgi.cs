using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIOCMS.Areas.Yonetim.Data
{
    public static class KullaniciBilgi
    {
        private static KullaniciSessionModel _kullanici;
        public static KullaniciSessionModel Kullanici
        {
            get
            {
                if (_kullanici == null)
                    _kullanici = (KullaniciSessionModel)HttpContext.Current.Session["Kullanici"];
                return _kullanici;
            }
        }
        public static bool YetkiliMi(enmYetkiler yetki,System.Web.Routing.RouteData rData)
        {
            var istekYapilanKontroller = rData.Values["Controller"].ToString() + "Controller";
            var kulYetki = Kullanici.Yetkiler[istekYapilanKontroller.ToUpper()];
           return ((enmYetkiler)kulYetki & yetki) == yetki;
        }
    }
}