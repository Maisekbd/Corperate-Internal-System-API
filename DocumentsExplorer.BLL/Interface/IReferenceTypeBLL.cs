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
    public interface IReferenceTypeBLL : IService<ReferenceType>
    {
        IQueryable<ReferenceTypeDTO> GetReferenceTypes();
        ReferenceTypeDTO GetById(int referenceTypId);
    }
}
