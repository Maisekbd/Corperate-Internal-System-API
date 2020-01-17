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
    public interface IDepartmentResponsibleBLL : IService<DepartmentResponsible>
    {
        IQueryable<DepartmentResponsibleDTO> GetDepartmentResponsibles();
        List<int> GetDepartmentByUserId(string userId);
        DepartmentResponsibleDTO GetById(int decisionTypeId);
        int Save(DepartmentResponsibleDTO obj, string currentUserId);
        int Delete(int id);
    }
}

