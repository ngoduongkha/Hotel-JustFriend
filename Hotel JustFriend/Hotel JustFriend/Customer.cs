//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hotel_JustFriend
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.RentInvoiceInfoes = new HashSet<RentInvoiceInfo>();
        }
    
        public int idCustomer { get; set; }
        public string fullname { get; set; }
        public string idCard { get; set; }
        public int idType { get; set; }
        public string address { get; set; }
    
        public virtual TypeCustomer TypeCustomer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RentInvoiceInfo> RentInvoiceInfoes { get; set; }
    }
}