using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MVCApp.Models
{
    public class EmployeeCurrentContractInfo
    {
        [Key]
        public int ID { get; set; }

        public string NewEmail { get; set; }
        public string NewLastName { get; set; }
        public string NewAddress { get; set; }
        public string NewCity { get; set; }
        public string NewState { get; set; }
        public int NewZipcode { get; set; }
        public string NewCountry { get; set; }
        public string NewHomephone { get; set; }

        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        public string Country { get; set; }
        public string Homephone { get; set; }

        public DateTime LastUpdateOnSurvery { get; set;}

    }
}