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
    
    public partial class Promotion_Type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Promotion_Type()
        {
            this.Discount_Code = new HashSet<Discount_Code>();
        }
    
        public int Type_IDs { get; set; }
        public string Type_Names { get; set; }
        public Nullable<int> Book_Count { get; set; }
        public double Type_Promotion { get; set; }
        public bool Exist { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discount_Code> Discount_Code { get; set; }
    }
}