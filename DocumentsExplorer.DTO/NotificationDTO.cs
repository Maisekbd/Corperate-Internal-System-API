using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class NotificationDTO
    {
        public int Id { get; set; }

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

        public int? DecisionId { get; set; }

        public virtual DecisionDTO Decision { get; set; }

        public int? MeetingId { get; set; }

        public virtual MeetingDTO Meeting { get; set; }
    }
}
