using AAAID.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class Attachment : BaseEntity
    {
        public Attachment()
        {

        }

        public string Name { get; set; }

        public string Path { get; set; }

        public string FileExtension { get; set; }

        [ForeignKey("DecisionExecution")]
        public int DecisionExecutionId { get; set; }

        public DecisionExecution DecisionExecution { get; set; }
    }
}
