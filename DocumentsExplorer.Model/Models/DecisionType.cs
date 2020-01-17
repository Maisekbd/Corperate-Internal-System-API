using AAAID.Common.Model;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class DecisionType : BaseEntity
    {
        public DecisionType()
        {
        }
        public string Name { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
