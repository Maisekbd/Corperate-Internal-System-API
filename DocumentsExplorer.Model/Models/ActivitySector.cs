using AAAID.Common.Model;
using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class ActivitySector : BaseEntity
    {
        public ActivitySector()
        {
            Companies = new List<Company>();
            Decisions = new List<Decision>();
        }

        #region Properties
        public string Name { get; set; }

        public DateTime? CreateDate { get; set; }

        public virtual IList<Company> Companies { get; set; }

        public virtual IList<Decision> Decisions { get; set; }
        #endregion
    }
}
