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
    public partial class MainCategory : BaseEntity
    {
        public MainCategory()
        {
        }

        #region Properties
        public string Description { get; set; }

        [ForeignKey("CouncilType")]
        public int CouncilTypeId { get; set; }

        public virtual CouncilType CouncilType { get; set; }
        #endregion
    }
}
