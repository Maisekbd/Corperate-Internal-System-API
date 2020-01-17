using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
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
    public class ReferenceTypeBLL : Service<ReferenceType>, IReferenceTypeBLL
    {
        private readonly IRepositoryAsync<ReferenceType> _repository;

        public ReferenceTypeBLL(IRepositoryAsync<ReferenceType> repository) : base(repository)
        {
            _repository = repository;
        }

        public ReferenceTypeDTO GetById(int referenceTypId)
        {
            try
            {
                ReferenceTypeDTO decisionType = new ReferenceTypeDTO();
                Mapper.Map<ReferenceType, ReferenceTypeDTO>(_repository
                     .Query()
                     .SelectQueryable()
                     .Where(c => c.Id == referenceTypId).FirstOrDefault(), decisionType);
                return decisionType;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<ReferenceTypeDTO> GetReferenceTypes()
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable()
                    .ProjectTo<ReferenceTypeDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
