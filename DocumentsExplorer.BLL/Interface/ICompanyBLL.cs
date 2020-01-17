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
   public interface ICompanyBLL : IService<Company>
    {
        IQueryable<CompanyDTO> GetCompanies();
        CompanyDTO GetById(int companyId);
        //ContributorDTO GetContributorById(int contributorId);
        int Save(CompanyDTO obj, string currentUserId);
        int Delete(int id);
        //int DeleteContributor(int id);
        //int SaveContributor(ContributorDTO obj, int companyId, string currentUserid);
    }
}


