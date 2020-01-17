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
    public interface ISubCategoryBLL : IService<SubCategory>
    {
        IQueryable<SubCategoryDTO> GetSubCategories(int? mainCategoryId);
        SubCategoryDTO GetById(int value);
        void Update(SubCategoryDTO dtoObject);
    }
}


