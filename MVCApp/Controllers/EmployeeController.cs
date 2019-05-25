using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class EmployeeController : Controller
    {
        /// <summary>
        /// //Testing here
        /// </summary>
        public  IEnumerable<EmployeeCurrentContractInfo> RetrieveContractInfo()
        {
            
            using (AuthenticateContext db = new AuthenticateContext())
            {
                return (from e in db.Employees.AsEnumerable()
                        join c in db.EmployeeContractChanges.AsEnumerable() on e.ID equals c.EmployeeID
                        orderby e.LastName descending
                        select new EmployeeCurrentContractInfo() //Still need to join by Formstatus
                        {
                            ID = e.ID,
                            Email = e.Email.ToString(),
                            NewEmail = c.NewEmail.ToString(),
                            LastName = e.LastName.ToString(),
                            NewLastName = c.NewLastName.ToString(),
                            Address = e.Address.ToString(),
                            NewAddress = c.NewAddress.ToString(),
                            City = e.City.ToString(),
                            NewCity = c.NewCity.ToString(),
                            State = e.State.ToString(),
                            NewState = c.NewState.ToString(),
                            Zipcode = e.Zipcode,
                            NewZipcode = c.NewZipcode,
                            Country = e.Country.ToString(),
                            NewCountry = c.NewCountry.ToString(),
                            Homephone = e.HomePhone.ToString(),
                            NewHomephone = c.NewHomePhone.ToString(),

                            LastUpdateOnSurvery = e.LastUpdate


                        }).ToList();
            }
        }

        // GET: Employee
        public ActionResult Index()
        {
            //var contractInfo = RetrieveContractInfo();
            using (AuthenticateContext db = new AuthenticateContext()) {
                return View(db.Employees.OrderBy(e => e.FirstName).ToList());
            }
           
        }

        // GET: Employee
        public ActionResult BulkView()
        {
            var contractInfo = RetrieveContractInfo();
            using (AuthenticateContext db = new AuthenticateContext())
            {
                return View(contractInfo);
            }

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login info)
        {
            using (AuthenticateContext db = new AuthenticateContext())
            {
                var usr = db.Employees.Single(e => e.UserName == info.UserName && e.Password == info.Password);
                if (usr != null)
                {
                    Session["StaffID"] = usr.StaffID;
                    Session["UserName"] = usr.UserName.ToString();
                    Session["UserType"] = usr.UserTypeID;


                    //Can perform check here to send admin user or regular user to respective pages.

                    return RedirectToAction("AdminLoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong.");
                }
               

            }

                return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["StaffID"] != null)
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