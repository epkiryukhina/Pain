//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client : Person
    {
        public Client()
        {
            this.Visit = new HashSet<Visit>();
            this.Service = new HashSet<Service>();
        }
    
        public int Debt { get; set; }
    
        public virtual ICollection<Visit> Visit { get; set; }
        public virtual ICollection<Service> Service { get; set; }
    }
}