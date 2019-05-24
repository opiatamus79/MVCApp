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
        public ActionResult GetEmployees()
        {
            //var employee = new Employee() { ID = 1, StaffID = 1, HRID = 1 };
            return View();
        }

        // GET: Employees
        public ActionResult Index()
        {
            var emp = new List<Employee>(); //Initializing emp for returning employees.
            using (HRP_DBEntities3 databaseConn = new HRP_DBEntities3()) //NOTE CONNECTION STRING
            {
                var employees = databaseConn.Employees.OrderBy(a => a.FirstName).ToList();
                emp = employees; //Json(new {employees= employees }, JsonRequestBehavior.AllowGet); // used to send json.
            }





            return View(emp);
        }
    }
}