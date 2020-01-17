using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class AgendaItemDTO
    {
        public AgendaItemDTO()
        {
            AgendaDetails = new List<AgendaDetailDTO>();
        }

        public int Id { get; set; }

        public string AgendaText { get; set; }

        public string AgendaNumber { get; set; }

        public string AgendaTitle { get; set; }

        public string PresentedBy { get; set; }

        public string Conclusion { get; set; }

        public int MeetingId { get; set; }

        public string AttachementName { get; set; }

        public virtual IList<AgendaDetailDTO> AgendaDetails { get; set; }
    }
}
