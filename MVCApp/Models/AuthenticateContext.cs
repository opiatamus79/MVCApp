using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCApp.Models
{
    public class AuthenticateContext : DbContext
    {
        /// <summary>
        /// TODO: Need to rename this context its main purpose now is to show HR Bulk View (Employees Contract Changes)
        /// </summary>

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeContractChanges> EmployeeContractChanges { get; set; }
        public DbSet<FormStatus> FormStatuses { get; set; }
        public DbSet<LegalForm> LegalForms { get; set; }

        public System.Data.Entity.DbSet<MVCApp.Models.EmployeeCurrentContractInfo> EmployeeCurrentContractInfo { get; set; }
    }
}