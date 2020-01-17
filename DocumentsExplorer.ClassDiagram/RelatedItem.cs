using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentsExplorer.ClassDiagram
{
    public class ReferenceItem
    {
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
        /// 6- 
        /// </summary>
        public int ReferenceType { get; set; }
    }
}