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
    
    public partial class sehir
    {
        public sehir()
        {
            this.ilces = new HashSet<ilce>();
        }
    
        public int Sehir_ID { get; set; }
        public string Sehir_adi { get; set; }
    
        public virtual ICollection<ilce> ilces { get; set; }
    }
}