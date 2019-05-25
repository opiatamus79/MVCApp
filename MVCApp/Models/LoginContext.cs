using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCApp.Models
{
    public class LoginContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<Login> LoginInfo { get; set; }
    }


}