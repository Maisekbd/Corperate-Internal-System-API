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
    public interface IDecisionExecutionBLL : IService<DecisionExecution>
    {
        IQueryable<DecisionExecutionDTO> GetDecisionExecutions();
        DecisionExecutionDTO GetById(int decisionId, string userId);
        int Save(DecisionExecutionDTO obj, string currentUserId);
        int Delete(int id);
    }
}


