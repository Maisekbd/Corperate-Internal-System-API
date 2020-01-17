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
    [Authorize(Roles = "Admin")]
    public class CouncilMemberController : SharedController
    {
        readonly ICouncilMemberBLL _councilMemberBLL;
        readonly ICountryBLL _countryBLL;

        public CouncilMemberController(
            ICouncilMemberBLL councilMemberBLL,
            ICountryBLL countryBLL,
            ICouncilTypeBLL councilTypeBLL) : base(councilTypeBLL)
        {
            _councilMemberBLL = councilMemberBLL;
            _countryBLL = countryBLL;
        }

        // GET: CouncilMember
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create(int? id)
        {
            CouncilMemberDTO model;
            if (!id.HasValue)
                model = new CouncilMemberDTO();
            else
            {
                model = _councilMemberBLL.GetById(id.Value);
                model.CouncilTypeId = model.CouncilTypeId;
                //model.ConferenceDate = new DateTime(model.ConferenceDate.Value.Year, model.ConferenceDate.Value.Month, model.ConferenceDate.Value.Day);
                //model.MainCategoryId = model.SubCategory.MainCategoryId;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CouncilMemberDTO obj, IEnumerable<HttpPostedFileBase> Photo)
        {
            string physicalPath = "";
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (!Directory.Exists(Server.MapPath("~/AvatarPhotos")))
                        Directory.CreateDirectory(Server.MapPath("~/AvatarPhotos"));
                    foreach (var file in Photo)
                    {
                        obj.PhotoPath = Path.GetFileName(file.FileName);
                        physicalPath = Path.Combine(Server.MapPath("~/AvatarPhotos"), obj.Name+".jpg");
                        file.SaveAs(physicalPath);
                    }
                }
                var s = _councilMemberBLL.Save(obj, CurrentUser.Id);
            }
            return RedirectToAction("Index");

        }

        public JsonResult ReadCouncilMembers([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_councilMemberBLL.GetCouncilMembers().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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

        public JsonResult ReadCountries(string text)
        {

            var countries = _countryBLL.GetCountries();

            if (!string.IsNullOrEmpty(text))
            {
                countries = countries.Where(p => p.Name.Contains(text));
            }

            return Json(countries, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, CouncilMemberDTO obj)
        {
            if (obj != null)
            {
                _councilMemberBLL.Delete(obj.Id);
            }
            return Json(ModelState.ToDataSourceResult());
        }
    }
}