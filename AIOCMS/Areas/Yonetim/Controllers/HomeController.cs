using AIOCMS.Areas.Yonetim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIOCMS.Areas.Yonetim.Controllers
{
    /// <summary>
    /// Dashboard controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Dashboard Sayfası olarak kulanılacak
        /// </summary>
        /// <returns>Dashboard safasını bize döndürür</returns>
        [Yetki(enmYetkiler.Silme|enmYetkiler.Listeleme)]
        public ActionResult Index()
        {

            return View();

        }

    }
}