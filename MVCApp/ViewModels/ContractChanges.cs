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
        public string NewLastName { get; set; }
        public string NewEmail { get; set; }
        public string NewAddress { get; set; }
        public string NewCity { get; set; }
        public string NewState { get; set; }
        public int NewZipcode { get; set; }
        public string NewCountry { get; set; }
        public string NewHomePhone { get; set; }

        public DateTime DateCreated { get; set; }
        public int StatusID { get; set; }
        public int LegalFormsID { get; set; }
        public int EmployeeID { get; set; }
        public int ChangeLogID { get; set; }
        public string StatusName { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string Reason { get; set; }
        public DateTime UpdatedOn { get; set; }
        



}
}