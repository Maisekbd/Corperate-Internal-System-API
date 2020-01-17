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
    public partial class Department : BaseEntity
    {
        public Department()
        {
            Decisions = new List<Decision>();
        }


        #region Properties
        //[Key]
        //public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? CreateDate { get; set; }

        //public string CreatedBy { get; set; }

        //public DateTime? LastUpdateDate { get; set; }

        //public string LastUpdateBy { get; set; }

        public virtual ICollection<Decision> Decisions { get; set; }
        #endregion
    }
}
