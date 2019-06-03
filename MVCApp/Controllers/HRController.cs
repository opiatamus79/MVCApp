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
            EmployeeContractChangesRepository eCCR = new EmployeeContractChangesRepository();
            var ContractChangeHistory = eCCR.getChangeHistory(changeLogID, employeeID);

              
            if(ContractChangeHistory != null)
             {
                return PartialView("ChangeHistoryTable", ContractChangeHistory.ToList());
             }
                
            
            
            return View();
        }


        // GET: HR/ChangeHistoryView
        public ActionResult ChangeHistoryOverview()
        {//displays change history for HR when selecting a table row.

            if (TempData["showOptout"] == null)
            {//Determine if user has refreshed the page and needs to collect values to enable survey.
                return RedirectToAction("EnableSurvey", "FormUpdates");
            }

                using (AuthenticateContext db = new AuthenticateContext())
            {
                //pass in stored procedure result here then modify to be ContractChanges format.
                EmployeeContractChangesRepository eCCR = new EmployeeContractChangesRepository();
                var UniqueList = eCCR.GetUniqueEmployeeContractLogs();
                var GroupedChangeLogs = UniqueList.ToList();
                ViewBag.Title = "Dashboard";
                ViewBag.ModalHeader = "Survey";
                ViewBag.Name = Session["Firstname"] + " " + Session["Lastname"];

                var x = ViewBag.showSurvey;


                ViewBag.showOptout = (string)TempData["showOptout"] == "hide" ? false : true;
                ViewBag.showSurvey = (string)TempData["showSurvey"] == "hide" ? false : true;
                ViewBag.submitSurvey = ViewBag.showSurvey ? false : true;


              if (GroupedChangeLogs != null)
                {

                    HRDashboardViewModel model = new HRDashboardViewModel();

                    model.ContractChanges = GroupedChangeLogs.ToList();

                    return View(model);

                    //return View(GroupedChangeLogs.ToList()); 
                }
                

                return View();
            }

        }
        public ActionResult VisualOverview()
        {
            //Need to show Pending, Edited, and Approved forms for each user.
            EmployeeContractChangesRepository eCCR = new EmployeeContractChangesRepository();
            var UniqueContractChangeList = eCCR.GetUniqueEmployeeContractLogs();
            ViewBag.pending= 0;
            ViewBag.approved= 0;
            ViewBag.editing= 0;
            ViewBag.optout= 0;

            if (UniqueContractChangeList != null)
            {
                foreach (ContractChanges c in UniqueContractChangeList)
                {
                    ViewBag.pending = c.StatusName == "Pending" ? (ViewBag.pending + 1) : ViewBag.pending;
                    ViewBag.editing = c.StatusName == "Editing" ? (ViewBag.editing + 1) : ViewBag.approved;
                    ViewBag.approved = c.StatusName == "Approved" ? (ViewBag.approved + 1) : ViewBag.approved;
                    ViewBag.optout = c.StatusName == "Opt-out" ? (ViewBag.optout + 1) : ViewBag.optout;
                }
                return View(UniqueContractChangeList);

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