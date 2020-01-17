using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class SubCategoryDTO
    {
        public SubCategoryDTO()
        {
        }

        #region Properties
        public int Id { get; set; }

        public string Description { get; set; }

        public int MainCategoryId { get; set; }

        public virtual MainCategoryDTO MainCategory { get; set; }

        public bool NeedCompanyField { get; set; }

        public bool NeedBudgetFields { get; set; }

        public bool NeedInvestmentProgramFields { get; set; }
        #endregion
    }
}
