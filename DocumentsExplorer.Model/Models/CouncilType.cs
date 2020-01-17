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
    [Table("CouncilType")]
    public partial class CouncilType : BaseEntity
    {
        public CouncilType()
        {
            CouncilMembers = new List<CouncilMember>();
            //Rounds = new List<Round>();
        }

        #region Properties
        public string Description { get; set; }

        public virtual ICollection<CouncilMember> CouncilMembers { get; set; }

        //public virtual IList<Round> Rounds { get; set; }
        #endregion
    }
}
