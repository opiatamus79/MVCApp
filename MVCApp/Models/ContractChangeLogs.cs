using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCApp.DataAccess
{
    public class ContractChangeLogs 
    {
        public Employee Employee { get; set; }   
        public IEnumerable<EmployeeContractChanges> EmployeeContractChangeList { get; set; } 
        public FormStatus FormStatus { get; set; }
        public LegalForm LegalForms { get; set; }
        public DateTime UpdatedOn { get; set; }


    }
}