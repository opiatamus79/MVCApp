using MVCApp.CustomAuthentication;
using MVCApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    [CustomAuthorize(Roles = "NonAdmin")]
    public class UserDashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.Title = "Dashboard";
            ViewBag.ModalHeader = "Survey";


            using (AuthenticateContext db = new AuthenticateContext())
            {
                //determine if user has passed their 3 month period, or their 90 day opt out period
                int userID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID;
                var employee = db.Employees.OrderBy(e => e.ID == userID).First(); //Going to need to join with latest entry in 

                var latestContractForm = from e in db.EmployeeContractChanges
                                         join s in db.FormStatuses on e.StatusID equals s.ID
                                         select new { e, s };//db.EmployeeContractChanges.OrderByDescending(l => l.DateCreated).First();



                if (employee != null && latestContractForm != null)
                {
                    //Determine if in opt out period
                    var test = latestContractForm
                                     .Where(S => S.s.StatusName == "Edit" || S.s.StatusName == "Approved")
                                     .OrderByDescending(l => l.e.DateCreated);


                    if (test != null)
                    {//Check if passed the opt out period, this is determined by days since previous update.
                        Console.Write(test.ToList());

                        var x = test.ToList();

                        Console.Write(x);

                    }


                    //Determin if in Survey period
                    if (employee.LastUpdate <  (DateTime.Today).AddMonths(3))
                    {//if last update date < 3 months from today-> survey

                        //if(latestContractForm.StatusID)

                       

                       bool needUpdate= true; //is this true?
                    }
                }

                



                return View();
            }



                
        }
    }
}

