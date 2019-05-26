using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVCApp.Models.DataAccess;

namespace MVCApp.Models
{
    public class AuthenticateContext : DbContext
    {
        /// <summary>
        /// TODO: Need to rename this context its main purpose now is to show HR Bulk View (Employees Contract Changes)
        /// </summary>
        /// 
        public AuthenticateContext()
            : base("AuthenticationDB")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Employees)
                .Map(m =>
                {
                    m.ToTable("UserRoles");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EmployeeContractChanges> EmployeeContractChanges { get; set; }
        public DbSet<FormStatus> FormStatuses { get; set; }
        public DbSet<LegalForm> LegalForms { get; set; }

        //Changed namespace here to use the name EmployeeCurrentContractInfo
        public System.Data.Entity.DbSet<EmployeeCurrentContractInfo> EmployeeCurrentContractInfo { get; set; }
    }
}