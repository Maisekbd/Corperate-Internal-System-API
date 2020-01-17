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
    public class RoundBLL : Service<Round>, IRoundBLL
    {
        private readonly IRepositoryAsync<Round> _repository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public RoundBLL(IUnitOfWorkAsync unitOfWork, IRepositoryAsync<Round> repository) : base(repository)
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

        public RoundDTO GetById(int roundId)
        {
            try
            {
                RoundDTO round = new RoundDTO();
                Mapper.Map<Round, RoundDTO>(_repository
                     .Query()
                     .SelectQueryable()
                     .Where(c => c.Id == roundId).FirstOrDefault(), round);
                return round;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<RoundDTO> GetRounds()
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable()
                    .ProjectTo<RoundDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Save(RoundDTO obj, string currentUserId)
        {
            Round round;
            try
            {
                _unitOfWork.BeginTransaction();

                if (obj.Id == 0)
                {
                    round = new Round();
                    Mapper.Map<RoundDTO, Round>(obj, round);
                    round.CreateDate = DateTime.Now;
                    round.CreatedBy = currentUserId;
                    base.Insert(round);
                }
                else
                {
                    round = _repository.Find(obj.Id);
                    Mapper.Map<RoundDTO, Round>(obj, round);
                    round.LastUpdateDate = DateTime.Now;
                    round.LastUpdateBy = currentUserId;
                    base.Update(round);
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return round.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
