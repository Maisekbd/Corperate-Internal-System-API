using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentsExplorer.Website.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActivitySectorController : SharedController
    {
        readonly IActivitySectorBLL _activitySectorBLL;

        public ActivitySectorController(
           IActivitySectorBLL activitySectorBLL,
            ICouncilTypeBLL councilTypeBLL) : base(councilTypeBLL)
        {
            _activitySectorBLL = activitySectorBLL;
        }
        // GET: Round
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_activitySectorBLL.GetActivitySectors().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ActivitySectorDTO obj)
        {
            if (obj != null && ModelState.IsValid)
            {
               var id = _activitySectorBLL.Save(obj, CurrentUser.Id);
                obj = _activitySectorBLL.GetById(id);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, ActivitySectorDTO obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                _activitySectorBLL.Save(obj, CurrentUser.Id);
               
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, ActivitySectorDTO obj)
        {
            if (obj != null)
            {
                _activitySectorBLL.Delete(obj.Id);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }
    }
}