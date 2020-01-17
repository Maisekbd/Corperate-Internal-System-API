using IdentityManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.DTO
{
    public class DepartmentResponsibleDTO
    {
        public DepartmentResponsibleDTO()
        {

        }

        public int Id { get; set; }

        [UIHint("UserEditor")]
        public string UserId { get; set; }

        public string UserName { get; set; }

        //public ApplicationUser User { get; set; }

        [UIHint("DepartmentEditor")]
        public int DepartmentId { get; set; }

       
        public virtual DepartmentDTO Department { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string LastUpdateBy { get; set; }
    }
}
