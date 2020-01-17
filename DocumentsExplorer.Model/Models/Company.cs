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
    public partial class Company : BaseEntity
    {
        public Company()
        {
            ActivitySectors = new List<ActivitySector>();
            Decisions = new List<Decision>();
            //Contributors = new List<Contributor>();

        }

        #region Properties
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateOfIncorporation { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public DateTime? CreateDate { get; set; }

        public virtual IList<ActivitySector> ActivitySectors { get; set; }
        public virtual IList<Decision> Decisions { get; set; }
        //public virtual IList<Contributor> Contributors { get; set; }


        #region Proerties From Inhouse mergration Data

        public string City { get; set; }

        public double? Captial { get; set; }

        public double? SubscribedCapital { get; set; }

        public double? PaidUpCapital { get; set; }

        public double? AAAIDSharesNum { get; set; }

        public double? AAAIDShareValue { get; set; }

        public double? AAAIDPaidCapital { get; set; }

        public double? AAAIDRemainCapital { get; set; }

        public string Address { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public string WebSite { get; set; }

        public string Fax { get; set; }

        public string CompanyManager { get; set; }

        [ForeignKey("Currency")]
        public int? CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

        public DateTime? FinYearStart { get; set; }

        public DateTime? FinYearEnd { get; set; }

        public DateTime? CompanyEstablishDate { get; set; }

        public DateTime? AAAIDShareDate { get; set; }

        public string InvestOpportunity { get; set; } 
        #endregion

        #endregion
    }
}
