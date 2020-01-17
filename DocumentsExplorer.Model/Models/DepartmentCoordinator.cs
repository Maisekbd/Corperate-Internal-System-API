using AAAID.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class DepartmentCoordinator : BaseEntity
    {
        public DepartmentCoordinator()
        {

        }

        public string UserId { get; set; }

        public int DepartmentId { get; set; }

        public virtual string DepartmentName { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }
    }
}
