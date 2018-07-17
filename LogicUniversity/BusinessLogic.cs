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

        public static List<department> GetDepartments()
        {
            var context = new LogicUniversityEntities();
            var query = from x in context.departments select x;

            return query.ToList();
        }

        public static List<collection_detail> GetCollectionDetails()
        {
            var context = new LogicUniversityEntities();
            var query = from x in context.collection_detail
                        where x.collection_status=="Collected"
                        select x;

            return query.ToList();
        }
    }
}