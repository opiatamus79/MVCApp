using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCApp.Models;
using MVCApp.DataAccess;
using MVCApp.CustomAuthentication;
using MVCApp.ViewModels;

namespace MVCApp.Controllers
{
    
    public class HRController : Controller
    {


        /*public IEnumerable<EmployeeCurrentContractInfo> RetrieveContractInfo()
        {
            // Will be changing this. Delete contractinfoes model and just 
            
            using (AuthenticateContext db = new AuthenticateContext())
            {
                return (from e in db.Employees.AsEnumerable()
                        join c in db.EmployeeContractChanges.AsEnumerable() on e.ID equals c.EmployeeID //Can 
                        orderby e.FirstName descending
                        select new EmployeeCurrentContractInfo()).ToList();
            }
       }*/

        // GET: Employee
        public ActionResult Index() //Returning list of all employees (TODO REWRITE)
        {
            //var contractInfo = RetrieveContractInfo();
            using (AuthenticateContext db = new AuthenticateContext()) {
                return View(db.Employees.OrderBy(e => e.FirstName).ToList());
            }
           
        }
        // GET: Employee
        public ActionResult ChangeHistory(int changeLogID, int employeeID)  //Pass the cid and eid as parameters
        {
            using (AuthenticateContext db = new AuthenticateContext())
            {
                /*
                 *NOTICE: This query will be used to populate modals for the Admin HR Bulk view. 
                 * Put this inside a partial view, still currently testing,
                 * Also be aware that there is a constant value being used just for testing, will just need to 
                 * make a call to the partial view and pass in the ChangeLogId to ViewBag and should populate modal
                 * with all the logs of a specific Employee Contract that was created when the user updated their info.
                 * 
                 * Remember will be creating two entries into the EmployeeContractChanges table when starting, one for the 
                 * initial to show the earliest change and another one to represent the update to user info.
                 * 
                 * ?
                */


                var ContractChangeHistory = (from c in db.EmployeeContractChanges.AsEnumerable()
                                         join status in db.FormStatuses.AsEnumerable() on c.StatusID equals status.ID
                                         join legal in db.LegalForms.AsEnumerable() on c.LegalFormsID equals legal.ID
                                         where (c.ChangeLogID == changeLogID &&  c.EmployeeID == employeeID)////BE AWARE THIS IS FOR TESTING
                                         select new ViewModels.ContractChanges
                                         {
                                             ID = c.ID,
                                             EmployeeContractChange = c,
                                             LegalForms = c.LegalForm,
                                             UpdatedOn = c.DateCreated,
                                             Status = c.FormStatus

                                        });

                
                
                if(ContractChangeHistory != null)
                {
                    return PartialView("ChangeHistoryTable", ContractChangeHistory.ToList());//View(ContractChangeHistory.ToList());
                }

                return View();
            }

        }


        // GET: HR/BulkView
        [CustomAuthorize(Roles = "NonAdmin")]
        public ActionResult ChangeHistoryOverview()
        {

            using (AuthenticateContext db = new AuthenticateContext())
            {
                var GroupedChangeLogs = (from c in db.EmployeeContractChanges.AsEnumerable()
                                         join status in db.FormStatuses.AsEnumerable() on c.StatusID equals status.ID
                                         join legal in db.LegalForms.AsEnumerable() on c.LegalFormsID equals legal.ID
                                         select new ViewModels.ContractChanges
                                         {
                                             ID = c.ID,
                                             EmployeeContractChange = c,
                                             LegalForms = c.LegalForm,
                                             UpdatedOn = c.DateCreated,
                                             Status = c.FormStatus

                                         });
  
                ViewBag.ID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID; //EXAMPLE TO RETRIEVE USER ID
                if (GroupedChangeLogs != null)
                {
                    return View(GroupedChangeLogs.ToList()); 
                }
                
                return View();
            }

        }

        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult LoggedIn()
        {
            if (Session["StaffID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }





    }
}