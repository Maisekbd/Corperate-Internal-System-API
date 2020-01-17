using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class MeetingAttendanceDTO
    {
        public MeetingAttendanceDTO()
        {

        }
        public int Id { get; set; }

        //public int? MinutesOfMeetingId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string JobDescription { get; set; }

        public string CompanyName { get; set; }

        public string EmployeId { get; set; }

        public string EmployeName { get; set; }

        public int? DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public bool IsAttendant { get; set; }

        public EnumMemberType MemberType { get; set; }

        public int? CouncilMemberId { get; set; }

        public int Role { get; set; }

        public string CauseOfAbsence { get; set; }

        public int MeetingId { get; set; }
    }
}
