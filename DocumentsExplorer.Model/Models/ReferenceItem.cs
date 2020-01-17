using AAAID.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class ReferenceItem : BaseEntity
    {
        public ReferenceItem()
        {
        }

        #region Properties
        public int RefereceItemNo { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Path { get; set; }

        public string FileExtension { get; set; }

        /// <summary>
        /// Enum Type 
        /// 0- مذكرة
        /// 1- قانون
        /// 2- محضر إجتماع
        /// 3-تقرير
        /// 4- جدول أعمال 
        /// 5- توصيه
        /// 6- إقتراح
        /// </summary>
        [ForeignKey("ReferenceType")]
        public int ReferenceTypeId { get; set; }

        public virtual ReferenceType ReferenceType { get; set; }

        [ForeignKey("Decision")]
        public int DecisionId { get; set; }

        public virtual Decision Decision { get; set; }

        [ForeignKey("ReferenceDecision")]
        public int? ReferenceDecisionId { get; set; }

        public virtual Decision ReferenceDecision { get; set; }
        #endregion
    }
}
