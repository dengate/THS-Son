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
    
    public partial class musteri
    {
        public musteri()
        {
            this.odemes = new HashSet<odeme>();
            this.yorums = new HashSet<yorum>();
        }
    
        public int Musteri_ID { get; set; }
        public Nullable<int> Masa_ID { get; set; }
        public Nullable<int> Restoran_ID { get; set; }
        public Nullable<bool> durum { get; set; }
        public Nullable<bool> cikis { get; set; }
    
        public virtual masa masa { get; set; }
        public virtual restoran restoran { get; set; }
        public virtual ICollection<odeme> odemes { get; set; }
        public virtual ICollection<yorum> yorums { get; set; }
    }
}