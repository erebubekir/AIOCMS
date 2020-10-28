using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIOCMS.Areas.Yonetim.Data
{
    [Flags]
    public enum enmYetkiler
    {
        Listeleme = 1,
        Ekleme =2,
        Duzenleme=4,
        Detay=8,
        Silme=16,
        KaliciSilme=32,
        OzelYetki=64 ,
        


        /// <summary>
        /// En Altta olması gereken Enum değeri, Toplam Enum Değerini almak için kullanılır.
        /// </summary>
        ButunYetkiler=127

    }
}