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
    public class DecisionTypeController : SharedController
    {
        readonly IDecisionTypeBLL _decisionTypeBLL;

        public DecisionTypeController(
           IDecisionTypeBLL decisionTypeBLL,
            ICouncilTypeBLL councilTypeBLL) : base(councilTypeBLL)
        {
            _decisionTypeBLL = decisionTypeBLL;
        }
        // GET: Round
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_decisionTypeBLL.GetDecisionTypes().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, DecisionTypeDTO obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                var id = _decisionTypeBLL.Save(obj);
                obj = _decisionTypeBLL.GetById(id);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, DecisionTypeDTO obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                _decisionTypeBLL.Save(obj);

            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, DecisionTypeDTO obj)
        {
            if (obj != null)
            {
                _decisionTypeBLL.Delete(obj.Id);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }
    }
}