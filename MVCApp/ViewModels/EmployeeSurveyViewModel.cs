//using ExpressiveAnnotations.Attributes;
using ExpressiveAnnotations.Attributes;
using MVCApp.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApp.ViewModels
{
    public class EmployeeSurveyViewModel
    {


        
        [RequiredIf("true", ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New Last Name")]
        public string NewLastName { get; set; }

        [RequiredIf("true", ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New Email")]
        public string NewEmail { get; set; }

        [RequiredIf("true", ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New Address")]
        public string NewAddress { get; set; }

        [RequiredIf("true", ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New City")]
        public string NewCity { get; set; }

        [RequiredIf("true", ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New State")]
        public string NewState { get; set; }

        [AssertThat("true", ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New Zipcode")]
        public int NewZipcode { get; set; }

        [RequiredIf("true", ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New Country")]
        public string NewCountry { get; set; }

        [RequiredIf("false",ErrorMessage = "Contact Information is invalid.")]
        [Display(Name = "New HomePhone")]
        public string NewHomePhone { get; set; }



        [RequiredIf("NewLastName != LastName", ErrorMessage = "Required to provide Legal Documentation if changing Last Name.")]
        [Display (Name = "Legal Documents")]
        public HttpPostedFileBase File { get; set; }

        [RequiredIf("NewLastName != LastName", ErrorMessage = "Required to provide Reason if changing Last Name.")]
        [Display(Name = "Reason")]
        public string Reason { get; set; }


        public string LastName {get; set;}
        public int StatusID { get; set; } 
        public int EmployeeID { get; set; }
        public string FormType { get; set; }
        public List<ContractChanges> ContractChanges { get; set; }

    }



}

