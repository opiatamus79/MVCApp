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
        // GET: Employee
        public ActionResult Index()
        {
            using (AuthenticateContext db = new AuthenticateContext())
            {
                // db.Employees.OrderBy(e => e.FirstName).ToList();
                //return View(db.Employees.OrderBy(e => e.FirstName).ToList());
                //var employees = db.Employees.OrderBy(e => e.FirstName);
                
                return View(db.Employees.ToList());
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