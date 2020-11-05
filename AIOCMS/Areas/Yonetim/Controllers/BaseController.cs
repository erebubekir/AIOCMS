using AIOCMS.Areas.Yonetim.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIOCMS.Areas.Yonetim.Controllers
{
    public class BaseController : Controller
    {
        internal Dictionary<string, object> result;
        public BaseController()
        {
            result = new Dictionary<string, object>();
        }       

    }
}