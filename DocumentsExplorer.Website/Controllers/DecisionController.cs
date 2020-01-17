using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentsExplorer.Website.Controllers
{
    [Authorize(Roles = "Admin,DepartmentCoordinator,DecisionReader")]
    public class DecisionController : SharedController
    {

        readonly IDecisionBLL _decisionBLL;
        readonly IMainCategoryBLL _mainCategoryBLL;
        readonly IActivitySectorBLL _activitySectorBLL;
        readonly ISubCategoryBLL _subCategoryBLL;
        readonly ICompanyBLL _companyBLL;
        readonly IDepartmentBLL _departmentBLL;
        readonly ICountryBLL _countryBLL;
        readonly IDecisionTypeBLL _decisionTypeBLL;
        readonly IDecisionExecutionBLL _decisionExecutionBLL;
        readonly IDepartmentResponsibleBLL _departmentResponsibleBLL;


        public DecisionController(IDecisionBLL decisionBLL,
            ICouncilTypeBLL councilTypeBLL,
            ICountryBLL countryBLL,
            IDecisionTypeBLL decisionTypeBLL,
            IActivitySectorBLL activitySectorBLL,
            IMainCategoryBLL mainCategoryBLL,
            ICompanyBLL companyBLL,
            IDepartmentBLL departmentBLL,
            IDecisionExecutionBLL decisionExecutionBLL,
            IDepartmentResponsibleBLL departmentResponsibleBLL,
            ISubCategoryBLL subCategoryBLL) : base(councilTypeBLL)
        {
            this._decisionBLL = decisionBLL;
            this._mainCategoryBLL = mainCategoryBLL;
            this._subCategoryBLL = subCategoryBLL;
            _countryBLL = countryBLL;
            _decisionTypeBLL = decisionTypeBLL;
            _activitySectorBLL = activitySectorBLL;
            _companyBLL = companyBLL;
            _departmentBLL = departmentBLL;
            _decisionExecutionBLL = decisionExecutionBLL;
            _departmentResponsibleBLL = departmentResponsibleBLL;
        }

        // GET: Decision
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int? id)
        {
            DecisionDTO model;
            if (!id.HasValue)
                model = new DecisionDTO();
            else
            {
                model = _decisionBLL.GetById(id.Value);
                model.CouncilTypeId = model.SubCategory.MainCategory.CouncilTypeId;
                model.ConferenceDate = new DateTime(model.ConferenceDate.Value.Year, model.ConferenceDate.Value.Month, model.ConferenceDate.Value.Day);
                model.MainCategoryId = model.SubCategory.MainCategoryId;

            }
            var listCompanies = id.HasValue ? _decisionBLL.GetById(id.Value).Companies.Select(c => c.Id).ToList() : new List<int>();
            var companies = _companyBLL.GetCompanies().Where(c => !listCompanies.Contains(c.Id));
            ViewBag.AllCompanies = companies;

            var listDepartments = id.HasValue ? _decisionBLL.GetById(id.Value).Departments.Select(c => c.Id).ToList() : new List<int>();
            var departments = _departmentBLL.GetDepartments().Where(c => !listDepartments.Contains(c.Id));
            ViewBag.AllDepartments = departments;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DecisionDTO obj, IEnumerable<HttpPostedFileBase> files, IEnumerable<HttpPostedFileBase> decisionAnnexFiles, List<int> selected, List<int> departmentSelected)
        {

            string decisionFileName = "";
            string annexFileName = "";
            if (obj.SuggestedExecutionDate == null)
                if (ModelState.Keys.Contains("SuggestedExecutionDate"))
                    ModelState["SuggestedExecutionDate"].Errors.Clear();
            if (ModelState.IsValid)
            {
                obj.DecisionText = HttpUtility.HtmlDecode(obj.DecisionText);
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        if (!Directory.Exists(ConfigurationManager.AppSettings["DecisionPath"]))
                            Directory.CreateDirectory(ConfigurationManager.AppSettings["DecisionPath"]);
                        //physicalPath = ;
                        //physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                        file.SaveAs(Path.Combine(ConfigurationManager.AppSettings["DecisionPath"] + "//", fileName));
                        decisionFileName = fileName;
                    }
                    obj.DecisionPath = decisionFileName;
                }


                if (decisionAnnexFiles != null)
                {
                    foreach (var file in decisionAnnexFiles)
                    {
                        var fileName = "Annex_" + Path.GetFileName(file.FileName);
                        if (!Directory.Exists(ConfigurationManager.AppSettings["DecisionPath"]))
                            Directory.CreateDirectory(ConfigurationManager.AppSettings["DecisionPath"]);
                        //decisionAnnexPath = ;
                        //physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                        file.SaveAs(Path.Combine(ConfigurationManager.AppSettings["DecisionPath"] + "//", fileName));
                        annexFileName = fileName;
                    }
                    obj.DecisionAnnexPath = annexFileName;
                }
                //var file = Basic_Usage_Get_File_Info(files);


                obj.SelectedCompaniesIds = selected;
                obj.SelectedDepartmentIds = departmentSelected;
                var s = _decisionBLL.Save(obj, CurrentUser.Id);
            }
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, DecisionDTO obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                _decisionBLL.Delete(obj.Id);
            }
            return Json(ModelState.ToDataSourceResult());
        }



        private IEnumerable<string> Basic_Usage_Get_File_Info(IEnumerable<HttpPostedFileBase> files)
        {
            return
                from a in files
                where a != null
                select string.Format("{0} ({1} bytes)", Path.GetFileName(a.FileName), a.ContentLength);
        }


        public ActionResult ViewCouncilDecisions(int councilId)
        {
            var council = _councilTypeBLL.GetById(councilId);
            ViewBag.councilId = council.Id;
            ViewBag.councilName = council.Description;
            return View();
        }
        public JsonResult ReadDecisions([DataSourceRequest]DataSourceRequest request, string councilTypeId, string mainCategoryId, string subCategoryId, string txtSerach, string decisionNO, string conferenceYear, string countryId)
        {

            var result = _decisionBLL.GetDecisions(Convert.ToInt32(councilTypeId), Convert.ToInt32(mainCategoryId), Convert.ToInt32(subCategoryId), txtSerach, decisionNO, Convert.ToInt32(!String.IsNullOrEmpty(conferenceYear) ? conferenceYear : "0"), Convert.ToInt32(countryId));
            if (!User.IsInRole("Admin"))
                if (User.IsInRole("DepartmentCoordinator"))
                {
                    var departmets = _departmentResponsibleBLL.GetDepartmentByUserId(CurrentUser.Id);
                    result = result.Where(c => c.Departments.Count() > 0 && c.Departments.Select(d => d.Id).Intersect(departmets).Count() > 0);
                }
            return new JsonResult() { Data = result.ToDataSourceResult(request), JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
        }

        public JsonResult ReadCouncilTypes(string text)
        {

            var councilTypes = _councilTypeBLL.GetCouncilTypes();

            if (!string.IsNullOrEmpty(text))
            {
                councilTypes = councilTypes.Where(p => p.Description.Contains(text));
            }

            return Json(councilTypes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReadDecisionTypes(string text)
        {

            var decisionTypes = _decisionTypeBLL.GetDecisionTypes();

            if (!string.IsNullOrEmpty(text))
            {
                decisionTypes = decisionTypes.Where(p => p.Name.Contains(text));
            }

            return Json(decisionTypes, JsonRequestBehavior.AllowGet);
        }



        public JsonResult ReadMainCategories(int? councilTypeId)
        {

            var mainCategories = _mainCategoryBLL.GetMainCategories(councilTypeId);
            return Json(mainCategories, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadSubCategories(int? mainCategoryId)
        {

            var subCategories = _subCategoryBLL.GetSubCategories(mainCategoryId);
            return Json(subCategories, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReadCompanies(int? modeId)
        {

            var listCompanies = modeId.HasValue ? _decisionBLL.GetById(modeId.Value).Companies.Select(c => c.Id).ToList() : new List<int>();
            var companies = _companyBLL.GetCompanies().Where(c => !listCompanies.Contains(c.Id));
            return Json(companies, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetSubCategory(int? subCategoryId)
        {
            if (subCategoryId.HasValue)
            {
                var subCategory = _subCategoryBLL.GetById(subCategoryId.Value);
                return Json(subCategory, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new List<SubCategoryDTO>(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult ReadReferenceDecisions(string text)
        {

            var decisions = _decisionBLL.GetDecisions();

            if (!string.IsNullOrEmpty(text))
            {
                decisions = decisions.Where(p => p.Subject.Contains(text) || p.ConferenceYear.ToString().Contains(text));
            }
            return new JsonResult()
            {
                Data = decisions,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
            //return Json(decisions, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompany(int? companyId)
        {

            var company = _companyBLL.GetById(companyId.Value);
            return Json(company, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetReferenceDecision(int? referenceDecisionId)
        {

            var decision = _decisionBLL.GetById(referenceDecisionId.Value);
            return Json(decision, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Download(string fileName)
        {
            try
            {
                if (fileName != null)
                {
                    //string path = @"F:\\Decisions"; //eg "C:\\Attachment"          
                    string path = ConfigurationManager.AppSettings["DecisionPath"];
                    string pathWithoutHash = fileName.Split('#').First();
                    string fullPath = Path.Combine(path, pathWithoutHash);
                    var contentType = MimeMapping.GetMimeMapping(fullPath);
                    var fileBytes = System.IO.File.ReadAllBytes(fullPath);

                    // Convert to ContentDisposition
                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = pathWithoutHash,
                        Inline = false,
                    };
                    Response.AppendHeader("Content-Disposition", cd.ToString());

                    // View document
                    return File(fileBytes, contentType);
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public ActionResult Preview(string fileName)
        {
            //string path = @"F:\\Decisions";
            string path = ConfigurationManager.AppSettings["DecisionPath"];
            string pathWithoutHash = fileName.Split('#').First();
            string fullPath = Path.Combine(path, pathWithoutHash);
            return File(fullPath, "application/pdf");
        }


        public ActionResult Async_Save(IEnumerable<HttpPostedFileBase> files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(file.FileName);
                    //var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    var physicalPath = Path.Combine(ConfigurationManager.AppSettings["DecisionPath"] + "//", fileName);
                    // The files are not actually saved in this demo
                    file.SaveAs(physicalPath);
                }
            }

            return Content("");
        }

        public ActionResult Async_Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    //var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                    var physicalPath = Path.Combine(ConfigurationManager.AppSettings["DecisionPath"] + "//", fileName);
                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        // System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
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

        public JsonResult ReadActivitySectors(string text)
        {
            var activitySectors = _activitySectorBLL.GetActivitySectors();
            if (!string.IsNullOrEmpty(text))
            {
                activitySectors = activitySectors.Where(p => p.Name.Contains(text));
            }
            return Json(activitySectors, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ExecuteDecision(int decisionId)
        {
            var model = _decisionExecutionBLL.GetById(decisionId, CurrentUser.Id);
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ExecuteDecision(DecisionExecutionDTO obj)
        {

            //string decisionFileName = "";
            //string annexFileName = "";
            //if (obj.SuggestedExecutionDate == null)
            //    if (ModelState.Keys.Contains("SuggestedExecutionDate"))
            //        ModelState["SuggestedExecutionDate"].Errors.Clear();
            if (ModelState.IsValid)
            {
                var s = _decisionExecutionBLL.Save(obj, CurrentUser.Id);
            }
            return RedirectToAction("Index");
        }



    }
}