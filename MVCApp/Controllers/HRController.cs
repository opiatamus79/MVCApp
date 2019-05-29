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
    [CustomAuthorize(Roles = "Admin")]
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


        // GET: HR
        public ActionResult Index() 
        {


            return View();
        }
        // GET: HR
        public ActionResult ChangeHistory(int changeLogID, int employeeID)  
        {
            /*
            using (AuthenticateContext db = new AuthenticateContext())
            {
                var ContractChangeHistory = (from c in db.EmployeeContractChanges.AsEnumerable()
                                         join status in db.FormStatuses.AsEnumerable() on c.StatusID equals status.ID
                                         join legal in db.LegalForms.AsEnumerable() on c.LegalFormsID equals legal.ID
                                         where (c.ChangeLogID == changeLogID &&  c.EmployeeID == employeeID)
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
                    return PartialView("ChangeHistoryTable", ContractChangeHistory.ToList());
                }
                
                return View();
            }
            */
            return View();
        }


        // GET: HR/ChangeHistoryView
        public ActionResult ChangeHistoryOverview(bool showSurvey = false, bool showOptOut = false ) //NOTE: need to come to only list table with unique change log ids and latest contract changes.
        {

            using (AuthenticateContext db = new AuthenticateContext())
            {
                //pass in stored procedure result here then modify to be ContractChanges format.
                EmployeeContractChangesRepository eCCR = new EmployeeContractChangesRepository();
                var UniqueList = eCCR.GetUniqueEmployeeContractLogs();




                /*var GroupedChangeLogs = (from c in UniqueList
                                         join status in db.FormStatuses.AsEnumerable() on c.StatusID equals status.ID
                                         join legal in db.LegalForms.AsEnumerable() on c.LegalFormsID equals legal.ID
                                         select new ViewModels.ContractChanges
                                         {
                                             ID = c.ID,
                                             EmployeeContractChange = c,
                                             LegalForms = c.LegalForm,
                                             UpdatedOn = c.DateCreated,
                                             Status = c.FormStatus

                                         });*/

                var GroupedChangeLogs = UniqueList.ToList();


  
                ViewBag.ID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID; //EXAMPLE TO RETRIEVE USER ID
                ViewBag.showSurvey = showSurvey ? true : false;
                ViewBag.showOptOut = showOptOut ? true : false;
                if (GroupedChangeLogs != null)
                {
                    return View(GroupedChangeLogs.ToList()); 
                }
                

                return View();
            }

        }
        public ActionResult VisualOverview()
        {
            //Need to show Pending, Edited, and Approved forms for each user.


            ViewBag.pending = 5; //will be making call to stored procedure to determine amount of pending, edited, and approved forms.

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        
        public ActionResult LoggedIn()
        {
            if (Session["Role"] != null)
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