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
    
    public partial class semt
    {
        public semt()
        {
            this.restorans = new HashSet<restoran>();
        }
    
        public int Ilce_ID { get; set; }
        public int Semt_ID { get; set; }
        public string Semt_adi { get; set; }
    
        public virtual ilce ilce { get; set; }
        public virtual ICollection<restoran> restorans { get; set; }
    }
}
