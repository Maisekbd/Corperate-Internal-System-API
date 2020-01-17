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
    public partial class MeetingAttendance : BaseEntity
    {
        public MeetingAttendance()
        {
        }

        #region Properties
        public string Name { get; set; }

        public string Email { get; set; }

        public string JobDescription { get; set; }

        public string CompanyName { get; set; }

        public string EmployeId { get; set; }

        public string EmployeName { get; set; }

        public int? DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public bool IsAttendant { get; set; }

        public int MemberType { get; set; }

        [ForeignKey("CouncilMember")]
        public int? CouncilMemberId { get; set; }

        public virtual CouncilMember CouncilMember { get; set; }

        public int Role { get; set; }

        public string CauseOfAbsence { get; set; }

        [ForeignKey("Meeting")]
        public int MeetingId { get; set; }

        public virtual Meeting Meeting { get; set; }
        #endregion
    }
}
