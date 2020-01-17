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
    public partial class Currency : BaseEntity
    {
        public Currency()
        {
            //Decisions = new List<Decision>();
        }

        #region Properties
        public string Name { get; set; }

        //public virtual ICollection<Decision> Decisions { get; set; }
        #endregion
    }
}
