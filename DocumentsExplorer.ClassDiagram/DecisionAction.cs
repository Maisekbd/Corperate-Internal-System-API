using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentsExplorer.ClassDiagram
{
    public class DecisionAction
    {


        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public int ActionType { get; set; }

        public int Status { get; set; }

        public string ExecutionNotes { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public List<DocumentsExplorer.ClassDiagram.Attachment> Attachments
        {
            get;
            set;
        }
    }
}