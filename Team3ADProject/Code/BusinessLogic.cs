using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team3ADProject.Model;


namespace Team3ADProject.Code
{
    public class BusinessLogic
    {
        static LogicUniversityEntities context = new LogicUniversityEntities();
        
        public static List<spViewCollectionList_Result> ViewCollectionList()
        {
            List<spViewCollectionList_Result> list = new List<spViewCollectionList_Result>();
            return list = context.spViewCollectionList().ToList();
        }
    }
}