using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;


namespace MVCApp.Models
{
    public class AuthenticateContext : DbContext
    {
        /// <summary>
        /// TODO: Need to rename this context its main purpose now is to show HR Bulk View (Employees Contract Changes)
        /// </summary>
        /// 

        

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EmployeeContractChanges> EmployeeContractChanges { get; set; }
        public DbSet<FormStatus> FormStatuses { get; set; }
        public DbSet<LegalForm> LegalForms { get; set; }

        //Changed namespace here to use the name EmployeeCurrentContractInfo
        public System.Data.Entity.DbSet<EmployeeCurrentContractInfo> EmployeeCurrentContractInfo { get; set; }
    }
}