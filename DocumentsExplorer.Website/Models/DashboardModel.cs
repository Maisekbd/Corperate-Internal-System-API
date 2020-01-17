using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentsExplorer.Website.Models
{
    public class DashboardModel
    {
        public DashboardModel()
        {
            CouncilReportList = new List<CouncilReportsVM>();
        }

        public List<CouncilReportsVM> CouncilReportList { get; set; }
    }


    public class CouncilReportsVM
    {
        public string CouncilDescription { get; set; }

        public int CouncilReportsCount { get; set; }

        public int ReportsCountFirstYear { get; set; }

        public int ReportsCountSecondYear { get; set; }

        public int ReportsCountThirdYear { get; set; }
    }

    public class EffectedCountryReports
    {
        public string CountryName { get; set; }

        public int DecisionsCount { get; set; }
    }


    public class CompanyLoanCountModel
    {
        public string CompanyName { get; set; }

        public int DecisionsCount { get; set; }
    }

    public class SectorDecisionCountModel
    {
        public string SectorName { get; set; }

        public int DecisionsCount { get; set; }
    }

}