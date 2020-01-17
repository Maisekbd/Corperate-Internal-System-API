using AAAID.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class Notification : BaseEntity
    {
        public Notification()
        {

        }

        public string UserId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeNo { get; set; }

        public string EmployeeMail { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// 0- not Send
        /// 1- send
        /// 2- unread
        /// 3- read
        /// 
        /// </summary>
        public int status { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsOpen { get; set; }

        /// <summary>
        /// Enum 
        /// 0- related To Decision Execution
        /// 1- Personal Notification
        /// 3- 
        /// </summary>
        public int NotificationType { get; set; }

        [ForeignKey("Decision")]
        public int? DecisionId { get; set; }

        public virtual Decision Decision { get; set; }

        [ForeignKey("Meeting")]
        public int? MeetingId { get; set; }

        public virtual Meeting Meeting { get; set; }
    }
}
