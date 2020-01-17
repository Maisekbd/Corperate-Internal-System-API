using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentsExplorer.ClassDiagram
{
    public class DepartmentCoordinator
    {
        public string UserId { get; set; }

        public int DepartmentId { get; set; }

        public virtual string DepartmentName { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

    }
}