//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace THS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class yonetici
    {
        public int Yonetici_ID { get; set; }
        public Nullable<int> Restoran_ID { get; set; }
        public string Yonetici_mail { get; set; }
        public string Yonetici_adi { get; set; }
        public string Yonetici_soyadi { get; set; }
        public string Yonetici_kullaniciadi { get; set; }
        public string Yonetici_telefonno { get; set; }
        public string Yonetici_sifre { get; set; }
    
        public virtual restoran restoran { get; set; }
    }
}
