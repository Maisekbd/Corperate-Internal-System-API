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
    public class DecisionExecution : BaseEntity
    {
        public DecisionExecution()
        {
            Attachments = new List<Attachment>();
        }

        #region Properties
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public string DepartmentName { get; set; }

        /// <summary>
        /// 0- يحتاج المتابعه
        /// 1- للاطلاع
        /// </summary>
        public int ActionType { get; set; }

        /// <summary>
        /// 0- تحت التنفيذ
        /// 1- حذر 
        /// 2- متاخر 
        /// 3- تم التنفيذ
        /// </summary>
        public int DecisionStatus { get; set; }

        public string ExecutionNotes { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public string UpdatedByEmpNO { get; set; }

        [ForeignKey("Decision")]
        public int DecisionId { get; set; }

        public Decision Decision { get; set; }

        public string AttachementName { get; set; }

        public virtual IList<Attachment> Attachments { get; set; }
        #endregion
    }
}
