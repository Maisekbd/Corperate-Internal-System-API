using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class MinutesOfMeetingDTO
    {
        public MinutesOfMeetingDTO()
        {
            Attendances = new List<CouncilMemberDTO>();
            Absents = new List<CouncilMemberDTO>();
        }

        public int Id { get; set; }

        public int RoundId { get; set; }

        public string RoundRoundNumber { get; set; }

        public int CouncilTypeId { get; set; }

        public string CouncilTypeDescription { get; set; }

        public DateTime MeetingDate { get; set; }

        public string Location { get; set; }

        public string MeetingSummary { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string LastUpdateBy { get; set; }

        public virtual IList<CouncilMemberDTO> Attendances { get; set; }

        public virtual IList<CouncilMemberDTO> Absents { get; set; }

        public List<int> SelectedAttendances { get; set; }

        public List<int> SelectedAbsents { get; set; }

        public virtual ICollection<MeetingAttendanceDTO> MeetingAttendances { get; set; }
    }
}
