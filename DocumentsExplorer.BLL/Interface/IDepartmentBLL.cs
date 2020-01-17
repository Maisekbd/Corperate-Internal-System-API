using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL.Interface
{
    public interface IDepartmentBLL : IService<Department>
    {
        IQueryable<DepartmentDTO> GetDepartments();
        DepartmentDTO GetById(int decisionTypeId);
        int Save(DepartmentDTO obj, string currentUserId);
        int Delete(int id);
    }
}

