using MVCApp.CustomAuthentication;
using MVCApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.ViewModels;
using System.Collections;

namespace MVCApp.Controllers
{
    [CustomAuthorize(Roles = "NonAdmin")]
    public class UserDashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index() //Will determine if user account needs to have survey created and sent and opt out button enabled.
        {
            if (TempData["showOptout"] == null)
            {
                return RedirectToAction("EnableSurvey", "FormUpdates");
            }
            ViewBag.Title = "Dashboard";
            ViewBag.ModalHeader = "Survey";
            ViewBag.Name = Session["Firstname"] + " " + Session["Lastname"];
            ViewBag.showOptout = (string)TempData["showOptout"] == "hide" ? false : true;
            ViewBag.showSurvey = (string)TempData["showSurvey"] == "hide" ? false : true;
            ViewBag.submitSurvey = ViewBag.showSurvey ? false : true;


            EmployeeContractChangesRepository eCCR = new EmployeeContractChangesRepository();
            int UserID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID;
            Employee employee = eCCR.GetEmployeeBy(UserID);

            var UniqueList = eCCR.GetUniqueEmployeeContractLogs();
            var GroupedChangeLogs = UniqueList.ToList().Where(x => x.EmployeeID == UserID);





            DateTime today = DateTime.Today;
            DateTime OptDate = (employee.LastUpdate).AddDays(90.00);

            if (employee != null)
                ViewBag.OptOutDaysLeft = ViewBag.showOptOut ? (OptDate.Subtract(today)).Days : 0;

            if (GroupedChangeLogs != null)
            {
                return View(GroupedChangeLogs);
            }

           return View();
        }

        // GET: Dashboard
        [HttpGet]
        public ActionResult ShowContractChangeFormHR([Bind(Include = "ID,NewLastName, NewEmail, NewAddress, NewCity," +
            "NewState,NewZipcode,NewCountry,NewHomePhone,FormType,EmployeeID")] HRDashboardViewModel contract) //Will determine if user account needs to have survey created and sent and opt out button enabled.
        {//returns back data that is used to populate the Survey or Contract Change Form.
            contract.ContractChanges = new List<ContractChanges>();
            IEnumerable<FormStatus> Statuses = new List<FormStatus>();

            using (AuthenticateContext db = new AuthenticateContext())
            {
                 Statuses = db.FormStatuses.AsEnumerable().ToList();  
            }
            ViewBag.FormStatuses = new SelectList(Statuses, "ID", "StatusName");

            return PartialView("~/Views/HR/SetupContractChangeForm.cshtml", contract);
        }

        // GET: Dashboard
        [HttpGet]
        public ActionResult ShowContractChangeFormEmployee(EmployeeSurveyViewModel contract) //Will determine if user account needs to have survey created and sent and opt out button enabled.
        {//returns back data that is used to populate the Survey.

            contract.LastName = contract.NewLastName;
              return PartialView("SetupContractChangeForm", contract);
        }

        [HttpGet]
        public ActionResult ShowOptOutForm(EmployeeContractChanges contract)
        {
            return PartialView("SetupOptOutForm", contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetupContractChangeForm([Bind(Include = "ID,NewLastName, NewEmail, NewAddress, NewCity," +
            "NewState,NewZipcode,NewCountry,NewHomePhone, FormType, StatusID, EmployeeID")] EmployeeContractChanges contract, string FormType)
        {//called to either initiate a contract change request (during surveys) or HR editing a contract change form.


            EmployeeContractChangesRepository eCCR = new EmployeeContractChangesRepository();

            if (ModelState.IsValid)
            {

            }


            string form = FormType;
            bool editing = form.Contains("editing");
            bool survey = form.Contains("survey");
            bool optout = form.Contains("optout");
            bool notEditingCurrentCF = false;
            int UserID = -1;

            if (form == "editing")
                UserID = contract.EmployeeID;
            else
                UserID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID;



            EmployeeContractChanges LastCF = eCCR.GetLCF(UserID);
            if (editing)
            {
                if (LastCF != null)
                    notEditingCurrentCF = (contract.ID != eCCR.GetLCF(UserID).ID) ? true : false;
  
                if (notEditingCurrentCF)
                {
                    ModelState.AddModelError("", "Recent changes have been made to this Contract, please review these changes.");
                    HRDashboardViewModel passBackContract = eCCR.HRDashboardViewModel(contract, form);
                    passBackContract.ContractChanges = new List<ContractChanges>();
                    IEnumerable<FormStatus> Statuses = new List<FormStatus>();

                    using (AuthenticateContext db = new AuthenticateContext())
                    {
                        Statuses = db.FormStatuses.AsEnumerable().ToList();
                    }
                    ViewBag.FormStatuses = new SelectList(Statuses, "ID", "StatusName");
                    ViewBag.Error = "Recent changes have been made to this Contract, please review these changes";
                    //return PartialView("~/Views/HR/SetupContractChangeForm.cshtml", passBackContract);
                    var errors = new Hashtable();
                    foreach (var pair in ModelState)
                    {
                        if (pair.Value.Errors.Count > 0)
                        {
                            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
                        }
                    }
                    return Json(new { success = true, errors });
                }
            }




            if (survey || editing)
            {
                eCCR.InsertEmployeeContractChanges(contract, UserID, editing, survey);
                if(editing)
                    eCCR.CheckApproved(contract, UserID);

                return Json(new { redirectTo = Url.Action("EnableSurvey", "FormUpdates") });
            }
            else if (optout)
            {
                eCCR.ResetContractChange(UserID, contract);
                return Json(new { redirectTo = Url.Action("EnableSurvey", "FormUpdates") });
            }
            //Need to send to Form updater method that goes through to determine if user needs to get Surveyed.
            return RedirectToAction("EnableSurvey", "FormUpdates");
        }



    }

}
