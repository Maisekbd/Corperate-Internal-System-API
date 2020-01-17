using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.Model.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL
{
    public class CouncilTypeBLL : Service<CouncilType>, ICouncilTypeBLL
    {
        private readonly IRepositoryAsync<CouncilType> _repository;

        public CouncilTypeBLL(IRepositoryAsync<CouncilType> repository) : base(repository)
        {
            _repository = repository;
        }

        public CouncilType GetById(int councilId)
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable().Where(c=>c.Id == councilId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<CouncilType> GetCouncilTypes()
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
