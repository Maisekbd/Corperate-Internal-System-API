using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentsExplorer.ClassDiagram
{
    public class Notification
    {
        public int EmployeeId { get; set; }

        public string EmployeeNo { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int status { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsOpen { get; set; }

        public int NotificationType { get; set; }

        public Decision Decision
        {
            get => default(Decision);
            set
            {
            }
        }
    }
}