using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentsExplorer.Website.Controllers
{
    public class DepartmentResponsibleController : SharedController
    {
        private ApplicationUserManager _userManager;
        readonly IDepartmentBLL _departmentBLL;
        readonly IDepartmentResponsibleBLL _departmentResponsibleBLL;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public DepartmentResponsibleController(
       IDepartmentBLL departmentBLL,
             ICouncilTypeBLL councilTypeBLL,
             IDepartmentResponsibleBLL departmentResponsibleBLL) : base(councilTypeBLL)
        {
            _departmentBLL = departmentBLL;
            _departmentResponsibleBLL = departmentResponsibleBLL;
        }

        // GET: DepartmentResponsible
        public ActionResult Index()
        {
            ViewData["defaultDepartment"] = _departmentBLL.GetDepartments().FirstOrDefault();
            ViewData["defaultUser"] = UserManager.Users.FirstOrDefault();
            return View();
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, DepartmentResponsibleDTO obj)
        {
            if (ModelState.Keys.Contains("Department.CreatedBy"))
                ModelState["Department.CreatedBy"].Errors.Clear();
            if (ModelState.Keys.Contains("Department.CreateDate"))
                ModelState["Department.CreateDate"].Errors.Clear();
            if (ModelState.Keys.Contains("Department.LastUpdateDate"))
                ModelState["Department.LastUpdateDate"].Errors.Clear();
            if (obj != null && ModelState.IsValid)
            {
                if (_departmentResponsibleBLL.GetDepartmentResponsibles().Where(c => c.UserId == obj.UserId && c.DepartmentId == obj.DepartmentId).Any())
                    ModelState.AddModelError("dublicate", "تم تعيين المستخدم كمسؤول عن نفس الإدارة مسبقا");
                else
                {
                    var id = _departmentResponsibleBLL.Save(obj, CurrentUser.Id);
                    obj = _departmentResponsibleBLL.GetById(id);
                    obj.UserName = UserManager.Users.Where(c => c.Id == obj.UserId).FirstOrDefault().FullName;
                }
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, DepartmentResponsibleDTO obj)
        {
            if (ModelState.Keys.Contains("Department.CreatedBy"))
                ModelState["Department.CreatedBy"].Errors.Clear();
            if (ModelState.Keys.Contains("Department.CreateDate"))
                ModelState["Department.CreateDate"].Errors.Clear();
            if (ModelState.Keys.Contains("Department.LastUpdateDate"))
                ModelState["Department.LastUpdateDate"].Errors.Clear();
            if (obj != null && ModelState.IsValid)
            {
                if (_departmentResponsibleBLL.GetDepartmentResponsibles().Where(c => c.UserId == obj.UserId && c.DepartmentId == obj.DepartmentId).Any())
                    ModelState.AddModelError("dublicate", "تم تعيين المستخدم كمسؤول عن نفس الإدارة مسبقا");
                else
                {
                    _departmentResponsibleBLL.Save(obj, CurrentUser.Id);
                    obj = _departmentResponsibleBLL.GetById(obj.Id);
                    obj.UserName = UserManager.Users.Where(c => c.Id == obj.UserId).FirstOrDefault().FullName;
                }
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, DepartmentResponsibleDTO obj)
        {
            if (ModelState.Keys.Contains("Department.CreatedBy"))
                ModelState["Department.CreatedBy"].Errors.Clear();
            if (ModelState.Keys.Contains("Department.CreateDate"))
                ModelState["Department.CreateDate"].Errors.Clear();
            if (ModelState.Keys.Contains("Department.LastUpdateDate"))
                ModelState["Department.LastUpdateDate"].Errors.Clear();
            if (obj != null)
            {
                _departmentResponsibleBLL.Delete(obj.Id);
            }

            return Json(new[] { new DepartmentResponsibleDTO() }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var listOfResponsibles = _departmentResponsibleBLL.GetDepartmentResponsibles().ToList();
            var users = UserManager.Users.ToList();
            foreach (var depRes in listOfResponsibles)
                depRes.UserName = users.Where(c => c.Id == depRes.UserId).FirstOrDefault().FullName;
            return Json(listOfResponsibles.ToDataSourceResult(request));
        }


        public JsonResult ReadDepartments(string text)
        {
            var decisionTypes = _departmentBLL.GetDepartments();
            if (!string.IsNullOrEmpty(text))
                decisionTypes = decisionTypes.Where(p => p.Name.Contains(text));
            return Json(decisionTypes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadUsers(string text)
        {
            var users = UserManager.Users.ToList();
            if (!string.IsNullOrEmpty(text))
                users = users.Where(p => p.FullName.Contains(text)).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}