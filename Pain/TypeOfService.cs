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
    
    public partial class TypeOfService
    {
        public TypeOfService()
        {
            this.Employee = new HashSet<Employee>();
            this.Service = new HashSet<Service>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Service> Service { get; set; }
        public virtual TypeOfPrice TypeOfPrice { get; set; }
    }
}
