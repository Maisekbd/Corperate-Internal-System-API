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
    public partial class CouncilMember : BaseEntity
    {
        public CouncilMember()
        {
            //CouncilTypes = new List<CouncilType>();
        }

        #region Properties
        public string Name { get; set; }

        //[ForeignKey("CouncilType")]
        //public int CouncilTypeId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public string Position { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public int memberRole { get; set; }

        public string PhotoPath { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }

        public DateTime? CreateDate { get; set; }

        [ForeignKey("CouncilType")]
        public int CouncilTypeId { get; set; }

        public virtual CouncilType CouncilType { get; set; }
        //public virtual ICollection<CouncilType> CouncilTypes { get; set; }


        //public string CreatedBy { get; set; }

        //public DateTime? LastUpdateDate { get; set; }

        //public string LastUpdateBy { get; set; }


        #endregion
    }
}
