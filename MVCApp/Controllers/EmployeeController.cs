using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCApp.Models;
using MVCApp.DataAccess;
using MVCApp.CustomAuthentication;

namespace MVCApp.Controllers
{
    
    public class EmployeeController : Controller
    {


        public IEnumerable<EmployeeCurrentContractInfo> RetrieveContractInfo()
        {
            
            using (AuthenticateContext db = new AuthenticateContext())
            {
                return (from e in db.Employees.AsEnumerable()
                        join c in db.EmployeeContractChanges.AsEnumerable() on e.ID equals c.EmployeeID
                        orderby e.DateCreated descending
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
        
        public ActionResult Index() //Returning list of all employees (TODO REWRITE)
        {
            //var contractInfo = RetrieveContractInfo();
            using (AuthenticateContext db = new AuthenticateContext()) {
                return View(db.Employees.OrderBy(e => e.FirstName).ToList());
            }
           
        }

        // GET: Employee
        [CustomAuthorize(Roles = "NonAdmin")]
        public ActionResult BulkView()
        {
            

            var EmployeesContractInfo = RetrieveContractInfo();
            using (AuthenticateContext db = new AuthenticateContext())
            {
                
                ViewBag.ID = ((CustomAuthentication.CustomPrincipal)this.HttpContext.User).ID;
                return View(EmployeesContractInfo);
            }

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView info)
        {
            using (AuthenticateContext db = new AuthenticateContext())
            {
                Employee usr = db.Employees.FirstOrDefault(e => e.UserName == info.UserName && e.Password == info.Password);
                if (usr != null)
                {

                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong.");
                }
               

            }
                //could not connect to db
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