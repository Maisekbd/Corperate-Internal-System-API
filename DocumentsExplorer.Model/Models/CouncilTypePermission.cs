using AAAID.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class CouncilTypePermission : BaseEntity
    {
        public CouncilTypePermission()
        {

        }

        public string UserID { get; set; }

        [ForeignKey("CouncilType")]
        public int CouncilTypeId { get; set; }

        public CouncilType CouncilType { get; set; }
    }
}
