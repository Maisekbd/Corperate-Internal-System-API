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
    public partial class Round : BaseEntity
    {
        public Round()
        {
            //Decisions = new List<Decision>();
        }

        #region Properties
        public string RoundNumber { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool IsCurrent { get; set; }

        [ForeignKey("CouncilType")]
        public int CouncilTypeId { get; set; }

        public virtual CouncilType CouncilType { get; set; }
        #endregion
    }
}
