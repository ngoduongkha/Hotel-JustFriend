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
    
    public partial class SalaryRecord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalaryRecord()
        {
            this.SalaryRecordInfoes = new HashSet<SalaryRecordInfo>();
        }
    
        public string idSalaryRecord { get; set; }
        public Nullable<int> idAccount { get; set; }
        public Nullable<System.DateTime> salaryRecordDate { get; set; }
        public decimal total { get; set; }
    
        public virtual Account Account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryRecordInfo> SalaryRecordInfoes { get; set; }
    }
}