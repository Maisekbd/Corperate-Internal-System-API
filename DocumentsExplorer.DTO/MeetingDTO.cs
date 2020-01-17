using AAAID.HR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class MeetingDTO
    {
        public MeetingDTO()
        {
            MeetingAttendances = new List<MeetingAttendanceDTO>();
            AgendaItems = new List<AgendaItemDTO>();
            //Attachments = new List<MeetingAttachment>();
            SelectedCouncilMembers = new List<int>();
            SelectedEmployees = new List<EmployeeDTO>();
        }

        #region Properties

        public int Id { get; set; }
        /// <summary>
        /// Serial Number in Current Round
        /// </summary>
        public int MeetingNumber { get; set; }

        public DateTime MeetingDate { get; set; }

        public TimeSpan MeetingTime { get; set; }

        public string Location { get; set; }

        public string PreparedById { get; set; }

        public string PreparedByName { get; set; }

        public string MeetingAgendaAttachmnet { get; set; }

        /// <summary>
        /// Serial Number for all Meetings 
        /// </summary>
        public int MeetingIndexNumber { get; set; }

        public RoundDTO Round { get; set; }

        public int RoundId { get; set; }

        public CouncilTypeDTO CouncilType { get; set; }

        public int CouncilTypeId { get; set; }

        public IList<int> SelectedCouncilMembers { get; set; }

        public IList<EmployeeDTO> SelectedEmployees { get; set; }

        public virtual IList<AgendaItemDTO> AgendaItems { get; set; }

        public virtual IList<MeetingAttendanceDTO> MeetingAttendances { get; set; }

        public string GeneratedMeetingNumber { get; set; }

        //public virtual IList<MeetingAttachment> Attachments { get; set; }
        #endregion
    }


    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
