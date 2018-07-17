using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogicUniversity.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<employee> employees = BusinessLogic.GetEmployees();
            ViewData["employees"] = employees;

            return View();
        }
    }
}