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

        public EmployeeContractChanges EmployeeContractChange { get; set; } //EID, ChangeLogID, ContractChangeID 
        public FormStatus Status { get; set; }
        public LegalForm LegalForms { get; set; }
        public DateTime UpdatedOn { get; set; }
        //public String StatusName { get; set; }

    }
}