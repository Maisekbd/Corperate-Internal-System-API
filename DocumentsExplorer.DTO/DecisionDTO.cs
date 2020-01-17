using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DocumentsExplorer.DTO
{
    public class DecisionDTO
    {
        public DecisionDTO()
        {
            ActivitySectors = new List<ActivitySectorDTO>();
            ReferenceItems = new List<ReferenceItemDTO>();
            DecisionExecutions = new List<DecisionExecutionDTO>();
        }

        public int Id { get; set; }

        public string Subject { get; set; }

        public string DecisionPath { get; set; }

        public string DecisionReference { get; set; }

        public string DecisionNumber { get; set; }

        public string DecisionText { get; set; }

        public int? ConferenceYear { get; set; }

        public string ConferenceNumber { get; set; }

        //[DataType(DataType.Date)]
        public DateTime? ConferenceDate { get; set; }

        public string ConferenceLocation { get; set; }

        public int? ConferenceIndex { get; set; }

        public string KeyWords { get; set; }

        public List<string> KeyWordList { get; set; }

        public string ParagraphIndex { get; set; }

        public int? CountryId { get; set; }

        public virtual CountryDTO Country { get; set; }

        public int? SubCategoryId { get; set; }

        public virtual SubCategoryDTO SubCategory { get; set; }

        public int? CompanyId { get; set; }

        public virtual CompanyDTO Company { get; set; }

        public string CountryName { get; set; }

        public string CouncilTypeDescription { get; set; }

        public int? CouncilTypeId { get; set; }

        public CouncilTypeDTO CouncilType { get; set; }

        public string DecisionTypeName { get; set; }

        public int? DecisionTypeId { get; set; }

        public MainCategoryDTO MainCategory { get; set; }

        public string MainCategoryDescription { get; set; }

        public int? MainCategoryId { get; set; }

        public string SubCategoryDescription { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string LastUpdateBy { get; set; }

        public float ContributorsProfitPercentage { get; set; }

        public float LegalReserve { get; set; }

        public float PalestineSupport { get; set; }

        public float MainReserve { get; set; }

        public float ProgramValue { get; set; }

        public float ProgramValueInDollar { get; set; }

        public int? ReferenceDecisionId { get; set; }

        public virtual DecisionDTO ReferenceDecision { get; set; }

        public DateTime? SuggestedExecutionDate { get; set; }

        //public int? ActivitySectorId { get; set; }

        //public virtual ActivitySectorDTO ActivitySector { get; set; }

        public List<int?> SelectedSectors { get; set; }

        public virtual ICollection<ActivitySectorDTO> ActivitySectors { get; set; }

        public string DecisionDescription { get { return String.Format("{0}-{1}: {2}", DecisionNumber??"", ConferenceYear.ToString(), Subject??""); } }

        public List<CompanyDTO> Companies { get; set; }

        public List<int> SelectedCompaniesIds { get; set; }

        public List<DepartmentDTO> Departments { get; set; }

        public List<int> SelectedDepartmentIds { get; set; }

        public EnumDecisionStatus DecisionStatus { get; set; }

        public string DecisionAnnexPath { get; set; }

        public bool IsExecutable { get; set; }

        public string ExecutionNotes { get; set; }

        public DateTime? ExecutionDate { get; set; }


        public int? AgendaItemId { get; set; }

        public AgendaItemDTO AgendaItem { get; set; }

        public int? AgendaDetailId { get; set; }

        public AgendaDetailDTO AgendaDetail { get; set; }

        public HttpPostedFileBase fileAtta { get; set; }

        public virtual IList<ReferenceItemDTO> ReferenceItems { get; set; }

        public virtual IList<DecisionExecutionDTO> DecisionExecutions { get; set; }

    }


   


    
}
