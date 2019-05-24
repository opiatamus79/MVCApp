using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public int StaffID { get; set; }
        public int HRID { get; set; }
        public DateTime LastUpdate { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String Lastname { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zipcode { get; set; }
        public String Country { get; set; }
        public String HomePhone { get; set; }
        public DateTime DateCreated { get; set; }


    }


}