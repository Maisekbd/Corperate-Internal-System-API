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
    public class DecisionExecutionBLL : Service<DecisionExecution>, IDecisionExecutionBLL
    {
        private readonly IRepositoryAsync<DecisionExecution> _repository;
        private readonly IRepositoryAsync<Decision> _DecisionRepository;
        private readonly IRepositoryAsync<DepartmentResponsible> _departmentResponsibleRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public DecisionExecutionBLL(IUnitOfWorkAsync unitOfWork,
            IRepositoryAsync<DecisionExecution> repository,
            IRepositoryAsync<Decision> decisionRepository,
            IRepositoryAsync<DepartmentResponsible> departmentResponsibleRepository) : base(repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _departmentResponsibleRepository = departmentResponsibleRepository;
            _DecisionRepository = decisionRepository;
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

        public DecisionExecutionDTO GetById(int decisionId, string userId)
        {
            try
            {
                DecisionExecutionDTO decisionExecution = new DecisionExecutionDTO();
                var departmentId = _departmentResponsibleRepository.Query().SelectQueryable().Where(c => c.UserId == userId).FirstOrDefault().DepartmentId;
                Mapper.Map<DecisionExecution, DecisionExecutionDTO>(_repository
                     .Query()
                     .Include(c => c.Decision)
                     .Include(c => c.Department)
                     .SelectQueryable()
                     .Where(c => (c.DecisionId == decisionId && c.DepartmentId == departmentId)).FirstOrDefault(), decisionExecution);
                return decisionExecution;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<DecisionExecutionDTO> GetDecisionExecutions()
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable()
                    .ProjectTo<DecisionExecutionDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Save(DecisionExecutionDTO obj, string currentUserId)
        {
            DecisionExecution DecisionExecution;
            try
            {
                _unitOfWork.BeginTransaction();
                DecisionExecution = _repository.Find(obj.Id);
                Mapper.Map<DecisionExecutionDTO, DecisionExecution>(obj, DecisionExecution);
                DecisionExecution.LastUpdateDate = DateTime.Now;
                DecisionExecution.LastUpdateBy = currentUserId;
                base.Update(DecisionExecution);
                _unitOfWork.SaveChanges();


                var decision = _DecisionRepository.Query().Include(c=>c.DecisionExecutions).SelectQueryable().Where(c => c.Id == obj.DecisionId).FirstOrDefault();
                decision.DecisionStatus = decision.DecisionExecutions.OrderBy(c => c.DecisionStatus).FirstOrDefault().DecisionStatus;
                decision.LastUpdateDate = DateTime.Now;
                decision.LastUpdateBy = currentUserId;
                _DecisionRepository.Update(decision);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return DecisionExecution.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
