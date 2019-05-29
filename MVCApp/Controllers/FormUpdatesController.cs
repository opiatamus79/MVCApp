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
                                          .OrderByDescending(contracts => contracts.c.DateCreated).First();



                bool showSurvey = true;
                bool inOptOutPeriod = true;

                //case where Employee has past contract changes.
                if (employee != null && latestContractForm != null)
                {
                    DateTime today = DateTime.Today;
                    inOptOutPeriod = (employee.LastUpdate).AddDays(90.00) <= today ? true : false;
                    showSurvey = ((employee.LastUpdate).AddMonths(3) >= today) && !inOptOutPeriod ? true : false;


                    return RedirectToAction("ShowDashboard", "Account", new {showSurvey = showSurvey, showOptOut = inOptOutPeriod });
                }

                //case where Employee is new.
                

                //Will be creating 2 new change log if will increment the ChangeLogID.


                return View();
            }
        }
    }
} 