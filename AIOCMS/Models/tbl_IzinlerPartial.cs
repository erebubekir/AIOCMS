using AIOCMS.Areas.Yonetim.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AIOCMS.Models
{
    public partial class tbl_Izinler
    {
        [NotMapped]
        private string sYetki;
        [NotMapped]
        public string SYetki
        {
            get
            {
                if (string.IsNullOrEmpty(sYetki))
                    sYetki = Yetkiler.ToIntStringBit();
                return sYetki;
            }
            set
            {
                sYetki = value;
            }
        }
    }
}