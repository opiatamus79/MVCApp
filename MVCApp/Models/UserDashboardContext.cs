using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCApp.Models.DataAccess
{
    public class UserDashboardContext : DbContext
    {
        public System.Data.Entity.DbSet<Employee> Employee { get; set; }   //need incase opt out is used.
        public System.Data.Entity.DbSet<EmployeeContractChanges> EmployeeContractChanges { get; set; } //used to for update form.
        public System.Data.Entity.DbSet<FormStatus> FormStatuses { get; set; }
        public System.Data.Entity.DbSet<LegalForm> LegalForms { get; set; }


    }
}