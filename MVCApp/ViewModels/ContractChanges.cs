using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVCApp.DataAccess;

namespace MVCApp.ViewModels
{
    public class ContractChanges
    {
        [Key]
        public int ID { get; set; }

        public EmployeeContractChanges EmployeeContractChange { get; set; }
        public FormStatus Status { get; set; }
        public LegalForm LegalForms { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}