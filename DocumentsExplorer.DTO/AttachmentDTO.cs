using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class AttachmentDTO
    {
        public AttachmentDTO()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string FileExtension { get; set; }

        public int DecisionExecutionId { get; set; }
    }
}
