//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MiniBookStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee_Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee_Role()
        {
            this.Employees = new HashSet<Employee>();
        }
    
        public int Role_ID { get; set; }
        public string Role_Name { get; set; }
        public int Role_Decentralization { get; set; }
        public double Role_Salary { get; set; }
        public bool Exist { get; set; }
    
        public virtual Decentralization Decentralization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}