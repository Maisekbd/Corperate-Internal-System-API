using AAAID.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class ReferenceType : BaseEntity
    {
        public ReferenceType()
        {
        }

        #region Properties
        public string Name { get; set; }

        public bool IsReferenceDecision { get; set; }

        #endregion
    }
}