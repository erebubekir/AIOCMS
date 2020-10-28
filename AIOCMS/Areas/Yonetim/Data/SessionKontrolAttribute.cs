using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIOCMS.Areas.Yonetim.Data
{
    /// <summary>
    /// Sessionda bulunan BitisTarihi indexini kontrol eder.
    /// </summary>
    public class SessionKontrolAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Her istek yapıldığında BitisTarihine atanan değer şuanki zamandan küçükse Session temizlenir.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (((DateTime)(filterContext.HttpContext.Session["BitisTarihi"]??DateTime.Now.AddMinutes(1))) < DateTime.Now)
            {
                filterContext.HttpContext.Session.RemoveAll();
                filterContext.HttpContext.Session.Clear();
            }
            base.OnActionExecuting(filterContext);
        }
        
    }
}