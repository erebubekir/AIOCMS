using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AIOCMS.Areas.Yonetim.Data
{
    /// <summary>
    /// Sessionda tutulacak kullanıcı bilgileri
    /// </summary>
    public class KullaniciSessionModel
    {
        /// <summary>
        /// kurucu method
        /// </summary>
        public KullaniciSessionModel()
        {
            Yetkiler = new Dictionary<string, int>();
        }
        /// <summary>
        /// Veri tabanı tbl_Kullanici Id alanı
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Veri tabanı tbl_Kullanici KullaniciAdi alanı
        /// </summary>
        public string KullaniciAdi { get; set; }
        /// <summary>
        /// Veri tabanı tbl_Kullanici AdiSoyadi alanı
        /// </summary>
        public string AdiSoyadi { get; set; }
        /// <summary>
        /// Veri tabanı tbl_Kullanici nin Kullanıcı grubuna ait izinlerin bulunduğu liste (KontollerAdi,Yetkiler)
        /// </summary>
        public Dictionary<string, int> Yetkiler { get; set; }
    }
}