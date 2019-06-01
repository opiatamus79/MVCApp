using MVCApp.DataAccess;
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

        public ActionResult LoadContractChangeForm( string action, int employeeID = 0,  string ReturnUrl="" ) //User can only see this if they are in opt out period.
        {

            using (AuthenticateContext db = new AuthenticateContext())
            {
                int userID = 0;
                if (action != "Edit")
                {
                    userID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID;
                }
                else
                {
                    userID = employeeID;
                }



            var lastContractChangeForm = db.EmployeeContractChanges.Where(x => x.EmployeeID == userID).OrderByDescending(x => x.DateCreated).FirstOrDefault();
            var employee = db.Employees.Where(x => x.ID == userID).FirstOrDefault();

            if (lastContractChangeForm != null || employee != null)
             {
               var lastCF = lastContractChangeForm;

                    return RedirectToAction("showContractChangeForm", "UserDashboard", new {
                        ID = lastCF != null ? lastCF.ID : 0,
                        NewAddress = lastCF != null ? lastCF.NewAddress : employee.Address,
                        NewCity = lastCF != null ? lastCF.NewCity : employee.City,
                        NewCountry = lastCF != null ? lastCF.NewCountry : employee.Country,
                        NewEmail = lastCF != null ? lastCF.NewEmail : employee.Email,
                        NewHomePhone = lastCF != null ? lastCF.NewHomePhone : employee.HomePhone,
                        NewLastName = lastCF != null ? lastCF.NewLastName : employee.LastName,
                        NewState = lastCF != null ? lastCF.NewState : employee.State,
                        NewZipcode = lastCF != null ?  lastCF.NewZipcode : employee.Zipcode,
                        DateCreated = lastCF != null ? lastCF.DateCreated : DateTime.Today,
                        ChangeLogID = lastCF != null ? lastCF.ChangeLogID : 1,
                        StatusID = lastCF != null ? lastCF.StatusID : 1,
                        LegalFormsID = lastCF != null ? lastCF.LegalFormsID : 0,
                        EmployeeID = lastCF != null ? lastCF.EmployeeID : userID,
                        FormStatus = lastCF != null ? lastCF.FormStatus : new FormStatus(),
                        LegalForm = lastCF != null ? lastCF.LegalForm : new LegalForm(),
                        Employee = lastCF != null ? lastCF.Employee : employee
                    });
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