﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Hotel_JustFriendEntities : DbContext
    {
        public Hotel_JustFriendEntities()
            : base("name=Hotel_JustFriendEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<RentInvoice> RentInvoices { get; set; }
        public virtual DbSet<Revenue> Revenues { get; set; }
        public virtual DbSet<RevenuePercentage> RevenuePercentages { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<TypeCustomer> TypeCustomers { get; set; }
        public virtual DbSet<TypeRoom> TypeRooms { get; set; }
    }
}
