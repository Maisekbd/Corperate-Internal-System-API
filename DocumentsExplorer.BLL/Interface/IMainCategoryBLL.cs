using DocumentsExplorer.Model.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL.Interface
{
    public interface IMainCategoryBLL : IService<MainCategory>
    {
        IQueryable<MainCategory> GetMainCategories(int? councilTypeId);

        MainCategory GetById(int mainCategory);
    }
}


