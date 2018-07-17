using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogicUniversity.Controllers
{
    public class StoreClerkController : Controller
    {
        // GET: StoreClerk
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<collection_detail> collection_detail = BusinessLogic.GetCollectionDetails();
            ViewData["collection_detail"] = collection_detail;

            return View();
        }
    }
}