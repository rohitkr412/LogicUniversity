using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicUniversity
{
    // The business logic class 
    public class BusinessLogic
    {

        // Returns a list of employees
        public static List<employee> GetEmployees()
        {
            var context = new LogicUniversityEntities();
            var query = from x in context.employees select x;

            return query.ToList();

        }
    }
}