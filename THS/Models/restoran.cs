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
    
    public partial class restoran
    {
        public restoran()
        {
            this.kategoris = new HashSet<kategori>();
            this.masas = new HashSet<masa>();
            this.musteris = new HashSet<musteri>();
            this.rezervasyons = new HashSet<rezervasyon>();
            this.uruns = new HashSet<urun>();
            this.yoneticis = new HashSet<yonetici>();
        }
    
        public int Restoran_ID { get; set; }
        public string Restoran_tipi { get; set; }
        public string Restoran_mail { get; set; }
        public string Restoran_adresi { get; set; }
        public string Restoran_adi { get; set; }
        public string Restoran_telefonno { get; set; }
        public string Restoran_resim { get; set; }
        public Nullable<int> Semt_ID { get; set; }
        public Nullable<int> Random { get; set; }
        public Nullable<int> Security { get; set; }
    
        public virtual ICollection<kategori> kategoris { get; set; }
        public virtual ICollection<masa> masas { get; set; }
        public virtual ICollection<musteri> musteris { get; set; }
        public virtual semt semt { get; set; }
        public virtual ICollection<rezervasyon> rezervasyons { get; set; }
        public virtual ICollection<urun> uruns { get; set; }
        public virtual ICollection<yonetici> yoneticis { get; set; }
    }
}