﻿using MVCApp.DataAccess;
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

        public ActionResult LoadContractChangeForm( string ReturnUrl="" ) //User can only see this if they are in opt out period.
        {
            //Gather list of the current logged on users info, into model EmployeeContractChanges.
            //(and would need to allow for two models)D:\Source\repos\MVCApp\MVCApp\Controllers\FormUpdatesController.cs


            //then after form can be viewed and submitted can add partial view 

            using (AuthenticateContext db = new AuthenticateContext())
            {
             int userID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID;



            var lastContractChangeForm = db.EmployeeContractChanges.Where(x => x.EmployeeID == userID).OrderByDescending(x => x.DateCreated).FirstOrDefault();

            if (lastContractChangeForm != null)
             {
               var lastCF = lastContractChangeForm;

                    return RedirectToAction("showContractChangeForm", "UserDashboard", new {
                        ID = lastCF.ID,
                        NewAddress = lastCF.NewAddress,
                        NewCity = lastCF.NewCity,
                        NewCountry = lastCF.NewCountry,
                        NewEmail = lastCF.NewEmail,
                        NewHomePhone = lastCF.NewHomePhone,
                        NewLastName = lastCF.NewLastName,
                        NewState = lastCF.NewState,
                        NewZipcode = lastCF.NewZipcode,
                        DateCreated = lastCF.DateCreated,
                        ChangeLogID = lastCF.ChangeLogID,
                        StatusID = lastCF.StatusID,
                        LegalFormsID = lastCF.LegalFormsID,
                        EmployeeID = lastCF.EmployeeID,
                        FormStatus = lastCF.FormStatus,
                        LegalForm = lastCF.LegalForm,
                        Employee = lastCF.Employee
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