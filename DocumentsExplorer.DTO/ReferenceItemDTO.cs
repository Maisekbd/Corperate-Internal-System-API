using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class ReferenceItemDTO
    {
        public int Id { get; set; }

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
        public ReferenceTypeDTO ReferenceType { get; set; }
        public int ReferenceTypeId { get; set; }

        public int DecisionId { get; set; }

        public int? ReferenceDecisionId { get; set; }

        public string RefDecisionPath { get; set; }
    }
}
