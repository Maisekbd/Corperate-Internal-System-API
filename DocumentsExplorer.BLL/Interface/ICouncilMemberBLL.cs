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
    public interface ICouncilMemberBLL : IService<CouncilMember>
    {
        IQueryable<CouncilMemberDTO> GetCouncilMembers();
        IQueryable<CouncilMemberDTO> GetCouncilMembers(int councilId);
        CouncilMemberDTO GetById(int councilMemberId);
        int Save(CouncilMemberDTO obj, string currentUserId);
        int Delete(int id);
    }

}
