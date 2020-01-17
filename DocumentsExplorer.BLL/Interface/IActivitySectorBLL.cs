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
    public interface IActivitySectorBLL : IService<ActivitySector>
    {
        IQueryable<ActivitySectorDTO> GetActivitySectors();
        ActivitySectorDTO GetById(int sectorId);
        int Save(ActivitySectorDTO obj, string currentUserId);
        int Delete(int id);
    }
}


