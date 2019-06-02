using MVCApp.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApp.ViewModels
{
    public class HRDashboardViewModel
    {


        [Required(ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New Last Name")]
        public string NewLastName { get; set; }

        [Required(ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New Email")]
        public string NewEmail { get; set; }
        
        [Required(ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New Address")]
        public string NewAddress { get; set; }

        [Required(ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New City")]
        public string NewCity { get; set; }

        [Required(ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New State")]
        public string NewState { get; set; }

        [Required(ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New Zipcode")]
        public int NewZipcode { get; set; }

        [Required(ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New Country")]
        public string NewCountry { get; set; }

        [Required(ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New HomePhone")]
        public string NewHomePhone { get; set; }


        public List<ContractChanges> ContractChanges { get; set; }
    }


}