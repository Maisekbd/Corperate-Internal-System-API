using AAAID.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class AgendaItem : BaseEntity
    {
        public AgendaItem()
        {
            AgendaDetails = new List<AgendaDetail>();
        }

        public string AgendaText { get; set; }

        public string AgendaNumber { get; set; }

        public string AgendaTitle { get; set; }
        
        public string PresentedBy { get; set; }

        public string Conclusion { get; set; }

        public string AttachementName { get; set; }

        [ForeignKey("Meeting")]
        public int MeetingId { get; set; }

        public virtual Meeting Meeting { get; set; }

        public virtual ICollection<AgendaDetail> AgendaDetails { get; set; }
    }
}
