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
    public class CouncilMemberBLL : Service<CouncilMember>, ICouncilMemberBLL
    {
        private readonly IRepositoryAsync<CouncilMember> _repository;
        private readonly IRepositoryAsync<CouncilType> _councilTypeRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public CouncilMemberBLL(IUnitOfWorkAsync unitOfWork,
            IRepositoryAsync<CouncilMember> repository,
            IRepositoryAsync<CouncilType> councilTypeRepository
            ) : base(repository)
        {
            _repository = repository;
            _councilTypeRepository = councilTypeRepository;
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

        public CouncilMemberDTO GetById(int councilMemberId)
        {
            try
            {
                CouncilMemberDTO councilMember = new CouncilMemberDTO();
                Mapper.Map<CouncilMember, CouncilMemberDTO>(_repository
                     .Query()
                     .Include(c => c.Country)
                     //.Include(c => c.CouncilTypes)
                     .SelectQueryable()
                     .Where(c => c.Id == councilMemberId).FirstOrDefault(), councilMember);
                return councilMember;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<CouncilMemberDTO> GetCouncilMembers()
        {
            try
            {
                return _repository
                    .Query()
                    .Include(c => c.Country)
                    //.Include(c => c.CouncilTypes)
                    .SelectQueryable()
                    .ProjectTo<CouncilMemberDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<CouncilMemberDTO> GetCouncilMembers(int councilId)
        {
            try
            {
                return _repository
                    .Query()
                    .Include(c => c.Country)
                    .Include(c => c.CouncilType)
                    //.Include(c => c.CouncilTypes)
                    .SelectQueryable()
                    .Where(c=>c.CouncilTypeId == councilId)
                    .ProjectTo<CouncilMemberDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Save(CouncilMemberDTO obj, string currentUserId)
        {
            CouncilMember councilMember;
            try
            {
                _unitOfWork.BeginTransaction();

                if (obj.Id == 0)
                {
                    councilMember = new CouncilMember();
                    Mapper.Map<CouncilMemberDTO, CouncilMember>(obj, councilMember);
                    //if (obj.SelectedCouncilTypes.Count > 0)
                    //    foreach (int council in obj.SelectedCouncilTypes)
                    //        councilMember.CouncilTypes.Add(_councilTypeRepository.Find(council));
                    councilMember.CreateDate = DateTime.Now;
                    councilMember.CreatedBy = currentUserId;
                    base.Insert(councilMember);
                }
                else
                {
                    councilMember = _repository.Query()
                        //.Include(c=>c.CouncilTypes)
                        .Select().Where(c=> c.Id == obj.Id).FirstOrDefault();
                    Mapper.Map<CouncilMemberDTO, CouncilMember>(obj, councilMember);
                    //foreach (var child in councilMember.CouncilTypes.ToList())
                    //    councilMember.CouncilTypes.Remove(child);
                    //if (obj.SelectedCouncilTypes.Count > 0)
                    //    foreach (int council in obj.SelectedCouncilTypes)
                    //        councilMember.CouncilTypes.Add(_councilTypeRepository.Find(council));
                    councilMember.LastUpdateDate = DateTime.Now;
                    councilMember.LastUpdateBy = currentUserId;
                    base.Update(councilMember);
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return councilMember.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
