using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class DecisionExecutionDTO
    {
        public DecisionExecutionDTO()
        {
        }

        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }


        /// <summary>
        /// 0- يحتاج المتابعه
        /// 1- للاطلاع
        /// </summary>
        public int ActionType { get; set; }

        public int DecisionId { get; set; }

        public string DecisionSubject { get; set; }

        public string DecisionDecisionNumber { get; set; }

        public int DecisionStatus { get; set; }

        public string ExecutionNotes { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public string AttachementName { get; set; }

        public bool NeedAction { get; set; }

        public List<AttachmentDTO> Attachments { get; set; }
    }
}
