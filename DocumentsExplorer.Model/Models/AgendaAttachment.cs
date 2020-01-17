using AAAID.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public class AgendaAttachment : BaseEntity
    {
        public int ObjectId { get; set; }

        public string ObjectType { get; set; }

        public string Description { get; set; }

        public string AttachmentPath { get; set; }

        public string CreatedByName { get; set; }
    }
}
