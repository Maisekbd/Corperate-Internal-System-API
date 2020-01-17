using DocumentsExplorer.Model.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL.Interface
{
    public interface ICouncilTypeBLL : IService<CouncilType>
    {
        IQueryable<CouncilType> GetCouncilTypes();
        CouncilType GetById(int councilId);
    }
}

