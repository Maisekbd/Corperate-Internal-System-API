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
    public class ActivitySectorBLL : Service<ActivitySector>, IActivitySectorBLL
    {
        private readonly IRepositoryAsync<ActivitySector> _repository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ActivitySectorBLL(IRepositoryAsync<ActivitySector> repository,
            IUnitOfWorkAsync unitOfWork) : base(repository)
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

        public IQueryable<ActivitySectorDTO> GetActivitySectors()
        {
            try
            {
                return _repository
                .Query()
                .SelectQueryable().ProjectTo<ActivitySectorDTO>(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }

        public ActivitySectorDTO GetById(int sectorId)
        {
            try
            {
                return _repository
                .Query()
                .SelectQueryable().ProjectTo<ActivitySectorDTO>().Where(c=>c.Id == sectorId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save(ActivitySectorDTO obj, string currentUserId)
        {
            ActivitySector activitySector;
            try
            {
                _unitOfWork.BeginTransaction();

                if (obj.Id == 0)
                {
                    activitySector = new ActivitySector();
                    Mapper.Map<ActivitySectorDTO, ActivitySector>(obj, activitySector);
                    activitySector.CreateDate = DateTime.Now;
                    activitySector.CreatedBy = currentUserId;
                    base.Insert(activitySector);
                }
                else
                {
                    activitySector = _repository.Find(obj.Id);
                    Mapper.Map<ActivitySectorDTO, ActivitySector>(obj, activitySector);
                    activitySector.LastUpdateDate = DateTime.Now;
                    activitySector.LastUpdateBy = currentUserId;
                    base.Update(activitySector);
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return activitySector.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
