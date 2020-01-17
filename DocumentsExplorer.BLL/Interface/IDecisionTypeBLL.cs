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
    public interface IDecisionTypeBLL : IService<DecisionType>
    {
        IQueryable<DecisionTypeDTO> GetDecisionTypes();
        DecisionTypeDTO GetById(int decisionTypeId);
        int Save(DecisionTypeDTO obj);
        int Delete(int id);
    }
}

