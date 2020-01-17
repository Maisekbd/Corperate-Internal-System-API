using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class AgendaDetailDTO
    {
        public AgendaDetailDTO()
        {

        }
        public int Id { get; set; }

        public string TreeNumber { get; set; }

        public string Description { get; set; }

        public int AgendaItemId { get; set; }

        public string AttachementName { get; set; }
    }
}
