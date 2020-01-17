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
    public class CompanyBLL : Service<Company>, ICompanyBLL
    {
        private readonly IRepositoryAsync<Company> _repository;
        private readonly IRepositoryAsync<ActivitySector> _activitySectorRepository;
        //private readonly IRepositoryAsync<Contributor> _contributorRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public CompanyBLL(IUnitOfWorkAsync unitOfWork, 
            IRepositoryAsync<Company> repository,
            //IRepositoryAsync<Contributor> contributorRepository,
            IRepositoryAsync<ActivitySector> activitySectorRepository) : base(repository)
        {
            _repository = repository;
            _activitySectorRepository = activitySectorRepository;
            //_contributorRepository = contributorRepository;
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

        public int DeleteContributor(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                //_contributorRepository.Delete(id);
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

        public CompanyDTO GetById(int companyId)
        {
            try
            {
                return _repository
                .Query()
                .SelectQueryable().ProjectTo<CompanyDTO>().Where(c => c.Id == companyId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<CompanyDTO> GetCompanies()
        {
            try
            {
                return _repository
                .Query()
                .SelectQueryable().ProjectTo<CompanyDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Save(CompanyDTO obj, string currentUserId)
        {
            Company company;
            try
            {
                _unitOfWork.BeginTransaction();

                if (obj.Id == 0)
                {
                    company = new Company();
                    Mapper.Map<CompanyDTO, Company>(obj, company);
                    if (obj.SelectedSectors.Count > 0)
                        foreach (int sector in obj.SelectedSectors)
                            company.ActivitySectors.Add(_activitySectorRepository.Find(sector));
                    company.CreateDate = DateTime.Now;
                    company.CreatedBy = currentUserId;
                    base.Insert(company);
                }
                else
                {
                    company = _repository.Query().Include(c=>c.ActivitySectors).SelectQueryable().Where(c=>c.Id ==  obj.Id).FirstOrDefault();
                    Mapper.Map<CompanyDTO, Company>(obj, company);
                    foreach (var s in company.ActivitySectors)
                        //_meetingAttendanceRepository.Delete(s.Id);
                        company.ActivitySectors.Remove(s);
                    //InsertOrUpdateGraph(minutesOfMeeting);
                    _unitOfWork.SaveChanges();
                    if (obj.SelectedSectors.Count > 0)
                        foreach (int sector in obj.SelectedSectors)
                            company.ActivitySectors.Add(_activitySectorRepository.Find(sector));
                    company.LastUpdateDate = DateTime.Now;
                    company.LastUpdateBy = currentUserId;
                    base.Update(company);
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return company.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public int SaveContributor(ContributorDTO obj, int companyId, string currentUserId)
        //{
        //    Contributor contributor;
        //        try
        //        {
        //            _unitOfWork.BeginTransaction();

        //            if (obj.Id == 0)
        //            {
        //            contributor = new Contributor();
        //                Mapper.Map<ContributorDTO, Contributor>(obj, contributor);
        //            contributor.CompanyId = companyId;
        //            contributor.CreateDate = DateTime.Now;
        //            contributor.CreatedBy = currentUserId;
        //            _contributorRepository.Insert(contributor);
        //            }
        //            else
        //            {
        //            contributor = _contributorRepository.Find(obj.Id);
        //                Mapper.Map<ContributorDTO, Contributor>(obj, contributor);
        //            contributor.LastUpdateDate = DateTime.Now;
        //            contributor.LastUpdateBy = currentUserId;
        //            _contributorRepository.Update(contributor);
        //            }
        //            _unitOfWork.SaveChanges();
        //            _unitOfWork.Commit();
        //            return contributor.Id;
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //}

        //ContributorDTO ICompanyBLL.GetContributorById(int Id)
        //{
        //    try
        //    {
        //        return _contributorRepository
        //        .Query()
        //        .SelectQueryable().ProjectTo<ContributorDTO>().Where(c => c.Id == Id).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
