//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AIOCMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Ayarlar
    {
        public int Id { get; set; }
        public string LogoUrl { get; set; }
        public string Baslik { get; set; }
        public string AltBaslik { get; set; }
        public string AnahtarKelime { get; set; }
        public string Aciklama { get; set; }
        public string Adres { get; set; }
        public string TelNo { get; set; }
        public string GSMNo { get; set; }
        public string FaxNo { get; set; }
        public string ScriptKodlari { get; set; }
        public System.DateTime OlusturmaTarihi { get; set; }
        public Nullable<System.DateTime> GuncellenmeTarihi { get; set; }
    }
}
