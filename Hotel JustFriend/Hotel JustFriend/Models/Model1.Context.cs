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
        public virtual DbSet<AccountRole> AccountRoles { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Billinfo> Billinfoes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImport> ProductImports { get; set; }
        public virtual DbSet<ProductImportInfo> ProductImportInfoes { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<SalaryRecord> SalaryRecords { get; set; }
        public virtual DbSet<SalaryRecordInfo> SalaryRecordInfoes { get; set; }
        public virtual DbSet<SalaryTable> SalaryTables { get; set; }
    }
}
