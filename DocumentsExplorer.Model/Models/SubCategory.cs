using AAAID.Common.Model;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class SubCategory : BaseEntity
    {

        public SubCategory()
        {
        }

        #region Properties
        public string Description { get; set; }

        [ForeignKey("MainCategory")]
        public int MainCategoryId { get; set; }

        
        public virtual MainCategory MainCategory { get; set; }


        /// <summary>
        /// this fielded added by first version
        /// not related to system perpuse
        /// </summary>
        #region not needed fields
        //public bool NeedCompanyField { get; set; }

        ////public bool NeedBudgetFields { get; set; }

        ////public bool NeedInvestmentProgramFields { get; set; }

        ////public bool IsLoan { get; set; } 
        #endregion
        #endregion
    }
}
