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
        public int NumberDay { get; set; }
        public decimal Price { get; set; }
        public int IdBill { get; set; }
        public int IdRoom { get; set; }
    
        public virtual Bill Bill { get; set; }
        public virtual Room Room { get; set; }
    }
}
