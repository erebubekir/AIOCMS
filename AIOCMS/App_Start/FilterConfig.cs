using AIOCMS.Areas.Yonetim.Data;
using System.Web;
using System.Web.Mvc;

namespace AIOCMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SessionKontrolAttribute());
        }
    }
}
