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
    public class CompanyController : SharedController
    {
        readonly ICompanyBLL _companyBLL;
        readonly IActivitySectorBLL _activitySectorBLL;
        readonly ICountryBLL _countryBLL;

        public CompanyController(
           ICompanyBLL companyBLL,
           ICountryBLL countryBLL,
           IActivitySectorBLL activitySectorBLL,
            ICouncilTypeBLL councilTypeBLL) : base(councilTypeBLL)
        {
            _companyBLL = companyBLL;
            _countryBLL = countryBLL;
            _activitySectorBLL = activitySectorBLL;
        }


        // GET: Company
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create(int? id)
        {
            CompanyDTO model;
            if (!id.HasValue)
                model = new CompanyDTO();
            else
                model = _companyBLL.GetById(id.Value);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CompanyDTO obj)
        {
            int objectId = 0;
            if (ModelState.Keys.Contains("DateOfIncorporation"))
                ModelState["DateOfIncorporation"].Errors.Clear();
            if (ModelState.IsValid)
            {
                objectId = _companyBLL.Save(obj, CurrentUser.Id);
            }
            if (obj.Id == 0)
                return RedirectToAction("Create", new { id = objectId });
            else
                return RedirectToAction("Index");

        }

        public JsonResult ReadCompanies([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_companyBLL.GetCompanies().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateContributor([DataSourceRequest] DataSourceRequest request, ContributorDTO obj, string companyId)
        {
            //if (obj != null && ModelState.IsValid)
            //{
            //    var id = _companyBLL.SaveContributor(obj, Convert.ToInt32(companyId), CurrentUser.Id);
            //    obj = _companyBLL.GetContributorById(id);
            //}

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateContributor([DataSourceRequest] DataSourceRequest request, ContributorDTO obj, string companyId)
        {
            //if (obj != null && ModelState.IsValid)
            //{
            //    _companyBLL.SaveContributor(obj, Convert.ToInt32(companyId), CurrentUser.Id);
            //}

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult DestroyContributor([DataSourceRequest] DataSourceRequest request, ContributorDTO obj)
        //{
        //    if (obj != null)
        //    {
        //        _companyBLL.DeleteContributor(obj.Id);
        //    }

        //    return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, CompanyDTO obj)
        {
            if (obj != null)
            {
                _companyBLL.Delete(obj.Id);
            }
            return Json(ModelState.ToDataSourceResult());
        }


        public JsonResult ReadActivitySectors(string text)
        {
            var activitySectors = _activitySectorBLL.GetActivitySectors();
            if (!string.IsNullOrEmpty(text))
            {
                activitySectors = activitySectors.Where(p => p.Name.Contains(text));
            }
            return Json(activitySectors, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadCountries(string text)
        {
            var countries = _countryBLL.GetCountries();
            if (!string.IsNullOrEmpty(text))
            {
                countries = countries.Where(p => p.Name.Contains(text));
            }
            return Json(countries, JsonRequestBehavior.AllowGet);
        }


    }
}