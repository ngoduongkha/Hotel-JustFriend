//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel_JustFriend.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductImport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductImport()
        {
            this.ProductImportInfoes = new HashSet<ProductImportInfo>();
        }
    
        public string idImport { get; set; }
        public Nullable<int> idEmployee { get; set; }
        public System.DateTime dateImport { get; set; }
        public decimal importPrice { get; set; }
    
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImportInfo> ProductImportInfoes { get; set; }
    }
}
