using AIOCMS.Areas.Yonetim.Data;
using AIOCMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml;

namespace AIOCMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var typeArray = Assembly.GetExecutingAssembly().GetTypes().Where(TheType => TheType.IsClass && !TheType.IsAbstract && TheType.Namespace == "AIOCMS.Areas.Yonetim.Controllers" && TheType.Name.Contains("Controller")).ToList();
            var KontrollerAdi = typeArray.Select(d => d.Name).ToList();
            var dllPath = Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "");
            string docuPath = dllPath.Substring(0, dllPath.LastIndexOf(".")) + ".XML";
            XmlDocument _docuDoc = new XmlDocument();
            if (File.Exists(docuPath))
            {
                _docuDoc = new XmlDocument();
                _docuDoc.Load(docuPath);
            }
            List<KontrollerTipi> kontTipleri = new List<KontrollerTipi>();
            foreach (var item in KontrollerAdi)
            {
                string path = "T:AIOCMS.Areas.Yonetim.Controllers." + item;
                XmlNode xmlDocuOfMethod = _docuDoc.SelectSingleNode(
              "//member[starts-with(@name, '" + path + "')]");
                KontrollerTipi kont = new KontrollerTipi
                    ();
                kont.KontrollerAdi = item.ToUpper();
                if (xmlDocuOfMethod != null)
                    kont.Summary= xmlDocuOfMethod.ChildNodes[0].InnerText;
                kontTipleri.Add(kont);
            }
            IzinVM.KontrollerAdlari = kontTipleri;

            CMSDBEntities db = new CMSDBEntities();
            var grups = db.tbl_KullaniciGrubu.ToList();
           
            foreach (var item in grups)
            {
                foreach (var izin in item.tbl_Izinler.ToList())
                {
                    if (!kontTipleri.Any(d => d.KontrollerAdi == izin.KontrollerAdi))
                    {
                        db.tbl_Izinler.Remove(izin);
                    }
                }
                foreach (var sistemIzin in kontTipleri)
                {
                    if (!item.tbl_Izinler.Any(d => d.KontrollerAdi == sistemIzin.KontrollerAdi))
                    {
                        db.tbl_Izinler.Add(new tbl_Izinler
                        {
                            AktifDurumu = true,
                            OlusturmaTarihi = DateTime.Now,
                            KontrollerAdi = sistemIzin.KontrollerAdi,
                            Yetkiler = 0,
                            KullaniciGrubuId=item.Id
                        });
                    }
                }
            }
            db.SaveChanges();
            
        }
    }
}
