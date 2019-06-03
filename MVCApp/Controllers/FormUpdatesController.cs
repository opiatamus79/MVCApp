using MVCApp.DataAccess;
using MVCApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;





namespace MVCApp.Controllers
{
    public class FormUpdatesController : Controller
    {
        // GET: FormUpdates
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OptOutForm()
        {//Setup the functionality for the OptOut Form




            return View();
        }

        public ActionResult LoadContractChangeForm( string form, int employeeID = 0,  string ReturnUrl="" ) //User can only see this if they are in opt out period.
        {//displays most recent contract change form to the user (will be most recent contract change form created or 

            using (AuthenticateContext db = new AuthenticateContext())
            {
                int userID = 0;
                if (!form.Contains("editing"))
                {//User is getting surveyed.
                    userID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID;
                }
                else
                {//HR worker is coming in to make changes to ContractChangeOrder
                    userID = employeeID;
                }
                //need case for opt out.


           var lastContractChangeForm = db.EmployeeContractChanges.Where(x => x.EmployeeID == userID).OrderByDescending(x => x.DateCreated).FirstOrDefault();
           var employee = db.Employees.Where(x => x.ID == userID).FirstOrDefault();

            if (lastContractChangeForm != null|| employee != null)
             {
               var lastCF = lastContractChangeForm;


                    EmployeeContractChangesRepository eCCR = new EmployeeContractChangesRepository();
                    HRDashboardViewModel HRModel = eCCR.HRDashboardViewModel(employee, form, lastCF.StatusID);

                    var UniqueList = eCCR.GetUniqueEmployeeContractLogs();//these changes recently added to get status
                    var GroupedChangeLogs = UniqueList.ToList();
                    HRModel.ContractChanges = GroupedChangeLogs.ToList();


                    return RedirectToAction("ShowContractChangeFormHR", "UserDashboard", HRModel);
             }
            }

            if (Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }

            return View();

        }

        // GET: FormUpdates
        public ActionResult EnableSurvey() 
        {
            //Determine if user is passed due on survey period.

            using (AuthenticateContext db = new AuthenticateContext())
            {
                
                int userID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID;
                var employee = (from e in db.Employees
                               where e.ID == userID
                               select e).First();



                var latestContractForm = (from c in db.EmployeeContractChanges
                                          join s in db.FormStatuses on c.StatusID equals s.ID
                                          join e in db.Employees.Where(e => e.ID == userID) on c.EmployeeID equals e.ID
                                          select new{ c, s, e})
                                          .OrderByDescending(contracts => contracts.c.DateCreated);



                bool showSurvey = true;
                bool showOptout = true;

                //case where Employee has past contract changes.
                if (employee != null && latestContractForm != null)
                {
                    var latestCF = latestContractForm.FirstOrDefault();
                    DateTime today = DateTime.Today;
                    DateTime SurveyPeriod = (employee.LastUpdate).AddMonths(3);
                    DateTime OptOutPeriod = (employee.LastUpdate).AddDays(90.00);

                    bool optOutLastDay = SurveyPeriod == OptOutPeriod;

                    showOptout = (OptOutPeriod <= today) ? false : true;
                    showSurvey = (  SurveyPeriod >= today) && !optOutLastDay  ? false : true;

                    TempData["showOptout"] = null;
                    TempData["showSurvey"] = null;


                    TempData["showOptout"] = showOptout ? "show" : "hide";
                    TempData["showSurvey"] = showSurvey ? "show" : "hide";
                    return RedirectToAction("ShowDashboard", "Account");
                }

                //case where Employee is new.
                //Check if userID exists, if it does update display the create form, if it does not then just redirect to login.
                if (userID >= 0)
                {//create contract forms.
                    return RedirectToAction("ShowDashboard", "Account");
                }

                //Will be creating 2 new change log if will increment the ChangeLogID.


                return Redirect("/Account/Login");
            }
        }


    }
} 