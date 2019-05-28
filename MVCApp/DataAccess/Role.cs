using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.DataAccess
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

    }
}