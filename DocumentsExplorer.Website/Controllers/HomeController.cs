using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.Model.Models;
using DocumentsExplorer.Website.Models;
using Repository.Pattern.Ef6;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentsExplorer.Website.Controllers
{
    [Authorize]
    public class HomeController : SharedController
    {

        readonly IDecisionBLL _decisionBLL;


        public HomeController(IDecisionBLL decisionBLL,
            ICouncilTypeBLL councilTypeBLL) : base(councilTypeBLL)
        {
            this._decisionBLL = decisionBLL;
        }

        public ActionResult Index()
        {
            DashboardModel vmModel = new DashboardModel();
            var councilLst = _councilTypeBLL.GetCouncilTypes().ToList();
            foreach (CouncilType council in councilLst)
            {
                var decisionList = _decisionBLL.GetDecisions(council.Id, 0,0, "", "", 0, 0);
                vmModel.CouncilReportList.Add(new CouncilReportsVM()
                {
                    CouncilDescription = council.Description,
                    CouncilReportsCount = decisionList.Count(),
                    ReportsCountFirstYear = decisionList.Where(c => c.ConferenceYear == 2009).Count(),
                    ReportsCountSecondYear = decisionList.Where(c => c.ConferenceYear == (2009 - 1)).Count(),
                    ReportsCountThirdYear = decisionList.Where(c => c.ConferenceYear == (2009 - 2)).Count()
                    //ReportsCountFirstYear = decisionList.Where(c => c.ConferenceYear == DateTime.Now.Year).Count(),
                    //ReportsCountSecondYear = decisionList.Where(c => c.ConferenceYear == (DateTime.Now.Year - 1)).Count(),
                    //ReportsCountThirdYear = decisionList.Where(c => c.ConferenceYear == (DateTime.Now.Year - 2)).Count()
                });
            }
            return View(vmModel);
        }

        public JsonResult GetEffectedCountries(string year)
        {

            List<EffectedCountryReports> effectedCountryLst = new List<EffectedCountryReports>();
            List<KeyValuePair<string, int>> decisionList = _decisionBLL.GetDecisionsGroupedByCountry(Convert.ToInt32(year));

            foreach (var val in decisionList)

                effectedCountryLst.Add(new EffectedCountryReports() { CountryName = val.Key, DecisionsCount = val.Value });

            return Json(effectedCountryLst, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetLoanDecisionsByCompany(string year)
        {

            List<CompanyLoanCountModel> companyLoanCountLst = new List<CompanyLoanCountModel>();
            List<KeyValuePair<string, int>> decisionList = _decisionBLL.GetLoanDecisionsByCompany(Convert.ToInt32(year));

            foreach (var val in decisionList)

                companyLoanCountLst.Add(new CompanyLoanCountModel() { CompanyName = val.Key, DecisionsCount = val.Value });

            return Json(companyLoanCountLst, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetSectorsDecisions(string year)
        {

            List<SectorDecisionCountModel> sectorDecisionCountLst = new List<SectorDecisionCountModel>();
            List<KeyValuePair<string, int>> decisionList = _decisionBLL.GetDecisionsGroupedBySector(Convert.ToInt32(year));

            foreach (var val in decisionList)

                sectorDecisionCountLst.Add(new SectorDecisionCountModel() { SectorName = val.Key, DecisionsCount = val.Value });

            return Json(sectorDecisionCountLst, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetCouncilReportforMonth(string year)
        //        {

        //            List<EffectedCountryReports> effectedCountryLst = new List<EffectedCountryReports>();
        //            var councilLst = _councilTypeBLL.GetCouncilTypes().ToList();
        //            foreach (CouncilType council in councilLst)
        //            {
        //                List<KeyValuePair<string,int>> decisionList = _decisionBLL.GetCouncilReportsforYear(Convert.ToInt32(year), council.Id);

        //            foreach (var val in decisionList)

        //                effectedCountryLst.Add(new EffectedCountryReports() { CountryName = val.Key, DecisionsCount = val.Value });

        //            return Json(effectedCountryLst, JsonRequestBehavior.AllowGet);
        //        }
    }
}