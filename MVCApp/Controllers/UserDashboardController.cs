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
        public ActionResult ShowContractChangeForm(EmployeeContractChanges contract) //Will determine if user account needs to have survey created and sent and opt out button enabled.
        {

            return PartialView("CreateContractChangeForm" , contract); //return to partial view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createContractChangeForm([Bind(Include = "NewLastName, NewEmail, NewAddress, NewCity," +
            "NewState,NewZipcode,NewCountry,NewHomePhone")] EmployeeContractChanges contract)
        {


            EmployeeContractChangesRepository eCCR = new EmployeeContractChangesRepository();

            eCCR.InsertEmployeeContractChanges(contract);

                //Need to send to Form updater method that goes through to determine if user needs to get Surveyed.
                return RedirectToAction("EnableSurvey", "FormUpdates");
        }





    }

}
