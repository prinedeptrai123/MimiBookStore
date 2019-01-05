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
    
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            this.Bill_Detail = new HashSet<Bill_Detail>();
        }
    
        public int Bill_ID { get; set; }
        public int Bill_Type { get; set; }
        public string Discount_Code { get; set; }
        public double Sum_Money { get; set; }
        public double Total_Money { get; set; }
        public double Excess_Cash { get; set; }
        public double Customer_Cash { get; set; }
        public int Customer_ID { get; set; }
        public int Employee_ID { get; set; }
        public System.DateTime Bill_Date { get; set; }
    
        public virtual Bill_Type Bill_Type1 { get; set; }
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_Detail> Bill_Detail { get; set; }
        public virtual Discount_Code Discount_Code1 { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
