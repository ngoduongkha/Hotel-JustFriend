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
    
    public partial class Attendance
    {
        public int idEmployee { get; set; }
        public System.DateTime dateAbsence { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}