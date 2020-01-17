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
    public class DepartmentResponsibleBLL : Service<DepartmentResponsible>, IDepartmentResponsibleBLL
    {
        private readonly IRepositoryAsync<DepartmentResponsible> _repository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public DepartmentResponsibleBLL(IUnitOfWorkAsync unitOfWork, IRepositoryAsync<DepartmentResponsible> repository) : base(repository)
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

        public List<int> GetDepartmentByUserId(string userId)
        {
            return _repository
                    .Query()
                    .Include(c=>c.Department)
                    .SelectQueryable().Where(c=>c.UserId == userId).Select(c=>c.DepartmentId).ToList();
        }

        public IQueryable<DepartmentResponsibleDTO> GetDepartmentResponsibles()
        {
            return _repository
                       .Query()
                       .Include(c => c.Department)
                       .SelectQueryable().ProjectTo<DepartmentResponsibleDTO>();
        }

        public int Save(DepartmentResponsibleDTO obj, string currentUserId)
        {
            DepartmentResponsible departmentResponsible;
            try
            {
                _unitOfWork.BeginTransaction();

                if (obj.Id == 0)
                {
                    departmentResponsible = new DepartmentResponsible();
                    Mapper.Map<DepartmentResponsibleDTO, DepartmentResponsible>(obj, departmentResponsible);
                    departmentResponsible.CreateDate = DateTime.Now;
                    departmentResponsible.CreatedBy = currentUserId;
                    base.Insert(departmentResponsible);
                }
                else
                {
                    departmentResponsible = _repository.Find(obj.Id);
                    Mapper.Map<DepartmentResponsibleDTO, DepartmentResponsible>(obj, departmentResponsible);
                    departmentResponsible.LastUpdateDate = DateTime.Now;
                    departmentResponsible.LastUpdateBy = currentUserId;
                    base.Update(departmentResponsible);
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return departmentResponsible.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DepartmentResponsibleDTO GetById(int departmentResponsibleId)
        {
            try
            {
                DepartmentResponsibleDTO departmentResponsible = new DepartmentResponsibleDTO();
                Mapper.Map<DepartmentResponsible, DepartmentResponsibleDTO>(_repository
                     .Query()
                     .Include(c=>c.Department)
                     .SelectQueryable()
                     .Where(c => c.Id == departmentResponsibleId).FirstOrDefault(), departmentResponsible);
                return departmentResponsible;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}