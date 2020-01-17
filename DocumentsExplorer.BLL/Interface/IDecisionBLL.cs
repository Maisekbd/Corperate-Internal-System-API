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
    public interface IDecisionBLL : IService<Decision>
    {
        int Insert(DecisionDTO obj, string userId);
        int Update(DecisionDTO obj, string userId);
        IQueryable<DecisionDTO> GetDecisions();
        IQueryable<DecisionDTO> GetDelayDecisions(string currentUserID);
        IList<DecisionDTO> GetExecutedDecisions(int meetingId);
        IQueryable<DecisionDTO> GetLatestDecisions(string currentUserID);
        IQueryable<DecisionDTO> GetDecisions(string currentUserID, int councilTypeId, int mainCategoryId, int subCategoryId, string txtSerach, string decisionNO, int conferenceYear, int countryId);
        int Save(DecisionDTO obj, string currentUserId);
        DecisionDTO GetById(int id, string userId);
        int Delete(int id);
        List<KeyValuePair<string, int>> GetDecisionsGroupedByCountry(int forYear);
        List<KeyValuePair<string, int>> GetLoanDecisionsByCompany(int forYear);
        List<KeyValuePair<string, int>> GetDecisionsGroupedBySector(int forYear);
        int ExecuteDecision(DecisionExecutionDTO decisionExecution, string userId);
    }
}
