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
    
    public partial class ilce
    {
        public ilce()
        {
            this.semts = new HashSet<semt>();
        }
    
        public int Ilce_ID { get; set; }
        public int Sehir_ID { get; set; }
        public string Ilce_adi { get; set; }
    
        public virtual sehir sehir { get; set; }
        public virtual ICollection<semt> semts { get; set; }
    }
}
