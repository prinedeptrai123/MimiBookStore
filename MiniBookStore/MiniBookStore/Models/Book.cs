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
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.Bill_Detail = new HashSet<Bill_Detail>();
            this.Book_Inventory = new HashSet<Book_Inventory>();
            this.Warehouse_Detail = new HashSet<Warehouse_Detail>();
        }
    
        public int Book_ID { get; set; }
        public string Book_Name { get; set; }
        public string Book_Author { get; set; }
        public int Book_Type { get; set; }
        public int Book_Theme { get; set; }
        public int Book_Company { get; set; }
        public double Book_Price { get; set; }
        public double Book_Promotion { get; set; }
        public byte[] Book_Image { get; set; }
        public int Book_Count { get; set; }
        public bool Exist { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_Detail> Bill_Detail { get; set; }
        public virtual Book_Theme Book_Theme1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book_Inventory> Book_Inventory { get; set; }
        public virtual Publishing_Company Publishing_Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warehouse_Detail> Warehouse_Detail { get; set; }
    }
}
