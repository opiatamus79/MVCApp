using MVCApp.CustomAuthentication;
using MVCApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.ViewModels;




namespace MVCApp.Controllers
{
    [CustomAuthorize(Roles = "NonAdmin")]
    public class UserDashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index() //Will determine if user account needs to have survey created and sent and opt out button enabled.
        {
            ViewBag.Title = "Dashboard";
            ViewBag.ModalHeader = "Survey";
            ViewBag.Name = Session["Firstname"] + " " + Session["Lastname"];
            ViewBag.showOptout = (string)TempData["showOptout"] == "hide" ? false : true;
            ViewBag.showSurvey = (string)TempData["showSurvey"] == "hide" ? false : true;
            ViewBag.submitSurvey = ViewBag.showSurvey ? false : true;


           return View();
        }


        // GET: Dashboard
        [HttpGet]
        public ActionResult ShowContractChangeFormHR([Bind(Include = "NewLastName, NewEmail, NewAddress, NewCity," +
            "NewState,NewZipcode,NewCountry,NewHomePhone,FormType")] HRDashboardViewModel contract) //Will determine if user account needs to have survey created and sent and opt out button enabled.
        {//returns back data that is used to populate the Survey or Contract Change Form.
            contract.ContractChanges = new List<ContractChanges>();

            return PartialView("SetupContractChangeForm" , contract); 
        }
        // GET: Dashboard
        public ActionResult ShowContractChangeFormEmployee(EmployeeContractChanges contract) //Will determine if user account needs to have survey created and sent and opt out button enabled.
        {//returns back data that is used to populate the Survey or Contract Change Form.


            return PartialView("SetupContractChangeForm", contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetupContractChangeForm([Bind(Include = "NewLastName, NewEmail, NewAddress, NewCity," +
            "NewState,NewZipcode,NewCountry,NewHomePhone, FormType")] EmployeeContractChanges contract, string FormType)
        {//called to either initiate a contract change request (during surveys) or HR editing a contract change form.

            string form = FormType;
            //ContractChanges c = new ContractChanges();
            EmployeeContractChangesRepository eCCR = new EmployeeContractChangesRepository();
            if (form.Contains("survey"))
            {//Tested use case of HR 
                int UserID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID;
                contract.StatusID = 1;
                eCCR.InsertEmployeeContractChanges(contract, UserID);
            }
            else if (form.Contains("editing"))//hr worker is updating a users contract (one already created)
            {//Hr worker will always be working on a created contract.
                
                
            }


            //Need to send to Form updater method that goes through to determine if user needs to get Surveyed.
            return RedirectToAction("EnableSurvey", "FormUpdates");
        }





    }

}
