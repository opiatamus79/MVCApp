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
        // GET: HR
        public ActionResult Index() 
        {


            return View();
        }
        // GET: HR
        public ActionResult ChangeHistory(int changeLogID, int employeeID)  
        {
            using (AuthenticateContext db = new AuthenticateContext())
            {
                var ContractChangeHistory = (from c in db.EmployeeContractChanges.AsEnumerable()
                                         join status in db.FormStatuses.AsEnumerable() on c.StatusID equals status.ID
                                         join legal in db.LegalForms.AsEnumerable() on c.LegalFormsID equals legal.ID
                                         where (c.ChangeLogID == changeLogID &&  c.EmployeeID == employeeID)
                                         select new ViewModels.ContractChanges //NEED TO REFACTOR THIS 
                                         {
                                             ID = c.ID,
                                             NewLastName = c.NewLastName,
                                             NewEmail = c.NewEmail,
                                             NewAddress = c.NewAddress,
                                             NewCity = c.NewCity,
                                             NewState = c.NewState,
                                             NewZipcode = c.NewZipcode,
                                             NewCountry = c.NewCountry,
                                             NewHomePhone = c.NewHomePhone,
                                             DateCreated = c.DateCreated,
                                             StatusID = c.StatusID,
                                             LegalFormsID = c.LegalFormsID,
                                             EmployeeID = c.EmployeeID,
                                             ChangeLogID = c.ChangeLogID,
                                             StatusName = c.FormStatus.StatusName,
                                             Description = c.FormStatus.Description,
                                             FilePath = c.LegalForm.FilePath,
                                             Reason = c.LegalForm.Reason,
                                             UpdatedOn = c.DateCreated

                                        });

              
                if(ContractChangeHistory != null)
                {
                    return PartialView("ChangeHistoryTable", ContractChangeHistory.ToList());
                }
                
            }
            
            return View();
        }


        // GET: HR/ChangeHistoryView
        public ActionResult ChangeHistoryOverview() //NOTE: need to come to only list table with unique change log ids and latest contract changes.
        {

            using (AuthenticateContext db = new AuthenticateContext())
            {
                //pass in stored procedure result here then modify to be ContractChanges format.
                EmployeeContractChangesRepository eCCR = new EmployeeContractChangesRepository();
                var UniqueList = eCCR.GetUniqueEmployeeContractLogs();
                var GroupedChangeLogs = UniqueList.ToList();
                ViewBag.Title = "Dashboard";
                ViewBag.ModalHeader = "Survey";
                ViewBag.Name = Session["Firstname"] + " " + Session["Lastname"];
                ViewBag.showOptout = (string)TempData["showOptout"] == "hide" ? false : true;
                ViewBag.showSurvey = (string)TempData["showSurvey"] == "hide" ? false : true;
                ViewBag.submitSurvey = ViewBag.showSurvey ? false : true;


                //ViewBag.ID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID; //EXAMPLE TO RETRIEVE USER ID



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
            EmployeeContractChangesRepository eCCR = new EmployeeContractChangesRepository();
            var UniqueContracChangeList = eCCR.GetUniqueEmployeeContractLogs();
            ViewBag.pending = 0;
            ViewBag.approved = 0;
            ViewBag.editing = 0;
            ViewBag.optout = 0;

            if (UniqueContracChangeList != null)
            {
                foreach (ContractChanges c in UniqueContracChangeList)
                {
                    ViewBag.pending = c.StatusName == "Pending" ? (ViewBag.pending + 1) : ViewBag.pending;
                    ViewBag.approved = c.StatusName == "Editing" ? (ViewBag.approved + 1) : ViewBag.approved;
                    ViewBag.editing = c.StatusName == "Approved" ? (ViewBag.editing + 1) : ViewBag.approved;
                    ViewBag.optout = c.StatusName == "Opt-out" ? (ViewBag.optout + 1) : ViewBag.optout;
                }
                return View(UniqueContracChangeList);

            }

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