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
    public partial class DepartmentResponsible : BaseEntity
    {
        public DepartmentResponsible()
        {
        }

        //[Key]
        //public int Id { get; set; }

        public string UserId { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public DateTime? CreateDate { get; set; }

        //public string CreatedBy { get; set; }

        //public DateTime? LastUpdateDate { get; set; }

        //public string LastUpdateBy { get; set; }
    }
}
