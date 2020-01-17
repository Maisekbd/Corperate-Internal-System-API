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
    public partial class Meeting : BaseEntity
    {
        public Meeting()
        {
            MeetingAttendances = new List<MeetingAttendance>();
            AgendaItems = new List<AgendaItem>();
            Attachments = new List<MeetingAttachment>();
        }

        #region Properties
        /// <summary>
        /// Serial Number in Current Round
        /// </summary>
        public int MeetingNumber { get; set; }

        public DateTime MeetingDate { get; set; }

        public TimeSpan MeetingTime { get; set; }

        public string Location { get; set; }

        public string PreparedById { get; set; }

        public string PreparedByName { get; set; }

        /// <summary>
        /// Serial Number for all Meetings 
        /// </summary>
        public int MeetingIndexNumber { get; set; }

        public string MeetingAgendaAttachmnet { get; set; }

        [ForeignKey("Round")]
        public int RoundId { get; set; }

        public virtual Round Round { get; set; }

        [ForeignKey("CouncilType")]
        public int CouncilTypeId { get; set; }

        public virtual CouncilType CouncilType { get; set; }

        public virtual ICollection<AgendaItem> AgendaItems { get; set; }

        public virtual ICollection<MeetingAttendance> MeetingAttendances { get; set; }

        

        public virtual IList<MeetingAttachment> Attachments { get; set; }
        #endregion
    }
}
