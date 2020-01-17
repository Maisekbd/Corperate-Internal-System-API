using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL
{
    public class DecisionTypeBLL : Service<DecisionType>, IDecisionTypeBLL
    {
        private readonly IRepositoryAsync<DecisionType> _repository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public DecisionTypeBLL(IUnitOfWorkAsync unitOfWork, IRepositoryAsync<DecisionType> repository) : base(repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public int Delete(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _repository.Delete(id);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
            return 0;
        }

        public DecisionTypeDTO GetById(int decisionTypeId)
        {
            try
            {
                DecisionTypeDTO decisionType = new DecisionTypeDTO();
                Mapper.Map<DecisionType, DecisionTypeDTO>(_repository
                     .Query()
                     .SelectQueryable()
                     .Where(c => c.Id == decisionTypeId).FirstOrDefault(), decisionType);
                return decisionType;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<DecisionTypeDTO> GetDecisionTypes()
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable()
                    .ProjectTo<DecisionTypeDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Save(DecisionTypeDTO obj)
        {
            DecisionType decisionType;
            try
            {
                _unitOfWork.BeginTransaction();

                if (obj.Id == 0)
                {
                    decisionType = new DecisionType();
                    Mapper.Map<DecisionTypeDTO, DecisionType>(obj, decisionType);
                    //decisionType.CreateDate = DateTime.Now;
                    //decisionType.CreatedBy = currentUserId;
                    base.Insert(decisionType);
                }
                else
                {
                    decisionType = _repository.Find(obj.Id);
                    Mapper.Map<DecisionTypeDTO, DecisionType>(obj, decisionType);
                    //decisionType.LastUpdateDate = DateTime.Now;
                    //decisionType.LastUpdateBy = currentUserId;
                    base.Update(decisionType);
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return decisionType.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
