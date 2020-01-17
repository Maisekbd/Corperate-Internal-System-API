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
    public partial class Decision : BaseEntity
    {
        public Decision()
        {
            ActivitySectors = new List<ActivitySector>();
            Companies = new List<Company>();
            Departments = new List<Department>();
            DecisionExecutions = new List<DecisionExecution>();
            ReferenceItems = new List<ReferenceItem>();
        }

        #region Properties
        public string Subject { get; set; }

        public string DecisionNumber { get; set; }

        public string DecisionText { get; set; }

        /// <summary>
        /// decision Pdf Path File
        /// </summary>
        public string DecisionPath { get; set; }


        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        /// <summary>
        /// TODO - Check with BO if Required
        /// </summary>
        [ForeignKey("DecisionType")]
        public int? DecisionTypeId { get; set; }

        public virtual DecisionType DecisionType { get; set; }

        public string KeyWords { get; set; }

        [ForeignKey("Country")]
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        [ForeignKey("AgendaItem")]
        public int? AgendaItemId { get; set; }

        public virtual AgendaItem AgendaItem { get; set; }

        [ForeignKey("AgendaDetail")]
        public int? AgendaDetailId { get; set; }

        public virtual AgendaDetail AgendaDetail { get; set; }

        public string DecisionAnnexPath { get; set; }

        /// <summary>
        /// القرار المرجعي
        /// TODO Should Remove After Migartion
        /// </summary>
        public string DecisionReference { get; set; }

        public string ParagraphIndex { get; set; }

        public DateTime? SuggestedExecutionDate { get; set; }

        public int DecisionStatus { get; set; }

        public virtual IList<ReferenceItem> ReferenceItems { get; set; }

        public virtual IList<Company> Companies { get; set; }

        public virtual IList<DecisionExecution> DecisionExecutions { get; set; }

        #region to_Delete

        public DateTime? CreateDate { get; set; }


        /// <summary>
        /// TODO i think should Removed
        /// </summary>
        public string UpdatedByEmpNO { get; set; }

        //[ForeignKey("Company")]
        //public int? CompanyId { get; set; }

        //public virtual Company Company { get; set; }

        #region Added in Old Version
        //public float ContributorsProfitPercentage { get; set; }

        //public float LegalReserve { get; set; }

        //public float PalestineSupport { get; set; }

        //public float MainReserve { get; set; }

        //public float ProgramValue { get; set; }

        //public float ProgramValueInDollar { get; set; } 
        #endregion

        //[ForeignKey("ReferenceDecision")]
        //public int? ReferenceDecisionId { get; set; }

        //public virtual Decision ReferenceDecision { get; set; }


        public bool IsExecutable { get; set; }


        public string ExecutionNotes { get; set; }

        public DateTime? ExecutionDate { get; set; }

        //[ForeignKey("ActivitySector")]
        //public int? ActivitySectorId { get; set; }

        //public virtual ActivitySector ActivitySector { get; set; }

        public virtual IList<Department> Departments { get; set; }

        /// <summary>
        /// TODO Should Moved To Meeting Table 
        /// </summary>
        #region Meeting Info
        public int ConferenceYear { get; set; }

        public string ConferenceType { get; set; }

        public string ConferenceNumber { get; set; }

        public DateTime? ConferenceDate { get; set; }

        public string ConferenceLocation { get; set; }

        public int ConferenceIndex { get; set; }
        #endregion

        public virtual IList<ActivitySector> ActivitySectors { get; set; }

        #endregion
        #endregion
    }
}
