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
    public interface IRoundBLL : IService<Round>
    {
        IQueryable<RoundDTO> GetRounds();
        RoundDTO GetById(int roundId);
        int Save(RoundDTO obj, string currentUserId);
        int Delete(int id);
    }
}

