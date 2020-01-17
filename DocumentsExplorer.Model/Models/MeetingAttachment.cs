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
    public partial class MeetingAttachment : BaseEntity
    {
        public MeetingAttachment()
        {

        }

        public string Name { get; set; }

        public string Path { get; set; }

        public string FileExtension { get; set; }

        [ForeignKey("Meeting")]
        public int MeetingId { get; set; }

        public Meeting Meeting { get; set; }
    }
}
