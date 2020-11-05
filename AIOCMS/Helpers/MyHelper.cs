using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIOCMS.Helpers
{
    public static class MyHelper
    {
        public static string BaseUrl(this HtmlHelper help,string url = "")
        {
           
            var requestU = HttpContext.Current.Request.Url;
            //@(Request.Url.Scheme)://@(Request.Url.Authority)
            return requestU.Scheme + "://" + requestU.Authority+"/"+ url;
        }        
        public static string YonetimUrl(this HtmlHelper help, string url = "")
        {
            return YonetimUrl(url);
        }
        public static string YonetimUrl( string url="")
        {

            var requestU = HttpContext.Current.Request.Url;
            //@(Request.Url.Scheme)://@(Request.Url.Authority)
            return requestU.Scheme + "://" + requestU.Authority + "/Yonetim" + "/" + url;
        }
    }
}