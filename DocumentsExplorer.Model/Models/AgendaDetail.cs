using AAAID.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class AgendaDetail : BaseEntity
    {
        public AgendaDetail()
        {

        }
        public string TreeNumber { get; set; }
        public string Description { get; set; }
        //public string ResponsibleId { get; set; }
        //public string ResponsibleName { get; set; }

        /// <summary>
        /// 0- task
        /// 1- توصيه
        /// 
        /// </summary>
        //public int ActionType { get; set; }
        public string AttachementName { get; set; }

        [ForeignKey("AgendaItem")]
        public int AgendaItemId { get; set; }

        public virtual AgendaItem AgendaItem { get; set; }
    }
}
