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
    public class RoundController : SharedController
    {
        readonly IRoundBLL _roundBLL;

        public RoundController(
            IRoundBLL roundBLL,
            ICouncilTypeBLL councilTypeBLL) : base(councilTypeBLL)
        {
            _roundBLL = roundBLL;
        }
        // GET: Round
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create(int? id)
        {
            RoundDTO model;
            if (!id.HasValue)
                model = new RoundDTO();
            else
                model = _roundBLL.GetById(id.Value);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(RoundDTO obj)
        {
            if (ModelState.IsValid)
            {
                var s = _roundBLL.Save(obj, CurrentUser.Id);
            }
            return RedirectToAction("Index");

        }

        public JsonResult ReadRounds([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_roundBLL.GetRounds().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, RoundDTO obj)
        {
            if (obj != null)
            {
                _roundBLL.Delete(obj.Id);
            }
            return Json(ModelState.ToDataSourceResult());
        }
    }
}