using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account Info
        public ActionResult Index()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        public ActionResult Login(LoginContext loginInfo)
        {
            if (ModelState.IsValid)
            {
                using (LoginContext db = new LoginContext()) {
                    db.Employees.First();
                }
            }
            return View();
        }
    }
}