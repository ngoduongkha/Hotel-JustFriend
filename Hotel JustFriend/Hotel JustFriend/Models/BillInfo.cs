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
    
    public partial class BillInfo
    {
        public Nullable<int> numberDay { get; set; }
        public Nullable<decimal> price { get; set; }
        public int idBill { get; set; }
        public int idRoom { get; set; }
        public int idCustomer { get; set; }
    
        public virtual Bill Bill { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
    }
}
