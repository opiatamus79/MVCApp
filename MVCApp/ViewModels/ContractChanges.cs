using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVCApp.DataAccess;

namespace MVCApp.ViewModels
{
    public class ContractChanges
    {//Plan for this model is to set to a partial view and retrieve all matching sets of Contract changes with matching
     //ChangeLogID, EmployeeID, based on the ChangeLogId/EmployeeID from the very last contractChange entry in the set.
        [Key]
        public int ID { get; set; } //ContractChangeID
        [Display(Name = "New Last Name")]
        public string NewLastName { get; set; }
        [Display(Name = "New Email")]
        public string NewEmail { get; set; }
        [Display(Name = "New Address")]
        public string NewAddress { get; set; }
        [Display(Name = "New City")]
        public string NewCity { get; set; }
        [Display(Name = "New State")]
        public string NewState { get; set; }
        [Display(Name = "New Zipcode")]
        public int NewZipcode { get; set; }
        [Display(Name = "New Country")]
        public string NewCountry { get; set; }
        [Display(Name = "New HomePhone")]
        public string NewHomePhone { get; set; }

        public DateTime DateCreated { get; set; }
        public int StatusID { get; set; }
        public int LegalFormsID { get; set; }
        public int EmployeeID { get; set; }
        public int ChangeLogID { get; set; }
        [Display(Name = "Status")]
        public string StatusName { get; set; }
        public string Description { get; set; }
        [Display(Name = "Download Attachments")]
        public string FilePath { get; set; }
        public string Reason { get; set; }
        public DateTime UpdatedOn { get; set; }


        public string getStatusName(int id)
        {
            string status = null;
            using (AuthenticateContext db = new AuthenticateContext())
            {
                var x = db.FormStatuses.Find(id);

                status = x != null ? x.StatusName : status;
            }
            return status;
        }

    }
}