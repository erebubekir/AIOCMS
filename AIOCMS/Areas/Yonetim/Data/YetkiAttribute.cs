﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIOCMS.Areas.Yonetim.Data
{
    /// <summary>
    /// Kontroller ve Actionların Yetkileri varsa kisilerin erişimine açmaya yarar
    /// </summary>
    public class YetkiAttribute : ActionFilterAttribute
    {
        //1101101
        enmYetkiler yetkiler;
        public YetkiAttribute(enmYetkiler yetki)
        {
            yetkiler = yetki;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["Kullanici"] == null)
            {
                if (filterContext.HttpContext.Request.Cookies["KullaniciAdiCk"] != null)
                {
                  
                    filterContext.Result = new RedirectResult("/Yonetim/KullaniciIslemleri/OturumGiris");
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Yonetim/KullaniciIslemleri/Giris");

                }
            }
            else
            {

                if (KullaniciBilgi.YetkiliMi(yetkiler, filterContext.RouteData))
                {
                    return;
                }
                filterContext.Result = new RedirectResult("/Yonetim/KullaniciIslemleri/YetkisizGiris");
            }
            return;
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

    }
}