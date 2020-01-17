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
    public class DepartmentController : SharedController
    {
        readonly IDepartmentBLL _departmentBLL;

        public DepartmentController(
           IDepartmentBLL departmentBLL,
            ICouncilTypeBLL councilTypeBLL) : base(councilTypeBLL)
        {
            _departmentBLL = departmentBLL;
        }
        // GET: Round
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var s = _departmentBLL.GetDepartments().ToList();
            return Json(_departmentBLL.GetDepartments().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, DepartmentDTO obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                var id = _departmentBLL.Save(obj, CurrentUser.Id);
                obj = _departmentBLL.GetById(id);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, DepartmentDTO obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                _departmentBLL.Save(obj, CurrentUser.Id);

            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, DepartmentDTO obj)
        {
            if (obj != null)
            {
                _departmentBLL.Delete(obj.Id);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }
    }
}