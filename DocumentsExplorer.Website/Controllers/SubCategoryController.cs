using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentsExplorer.Website.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {
        // GET: SubCategory
        public ActionResult Index()
        {
            return View();
        }
    }
}