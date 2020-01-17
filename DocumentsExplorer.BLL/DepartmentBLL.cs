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
    public class DepartmentBLL : Service<Department>, IDepartmentBLL
    {
        private readonly IRepositoryAsync<Department> _repository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public DepartmentBLL(IUnitOfWorkAsync unitOfWork, IRepositoryAsync<Department> repository) : base(repository)
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

        public DepartmentDTO GetById(int departmentId)
        {
            try
            {
                DepartmentDTO Department = new DepartmentDTO();
                Mapper.Map<Department, DepartmentDTO>(_repository
                     .Query()
                     .SelectQueryable()
                     .Where(c => c.Id == departmentId).FirstOrDefault(), Department);
                return Department;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<DepartmentDTO> GetDepartments()
        {
            try
            {
                var lst = _repository
                    .Query()
                    .SelectQueryable()
                    .ProjectTo<DepartmentDTO>().ToList();
                return _repository
                    .Query()
                    .SelectQueryable()
                    .ProjectTo<DepartmentDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Save(DepartmentDTO obj, string currentUserId)
        {
            Department department;
            try
            {
                _unitOfWork.BeginTransaction();

                if (obj.Id == 0)
                {
                    department = new Department();
                    Mapper.Map<DepartmentDTO, Department>(obj, department);
                    department.CreateDate = DateTime.Now;
                    department.CreatedBy = currentUserId;
                    base.Insert(department);
                }
                else
                {
                    department = _repository.Find(obj.Id);
                    Mapper.Map<DepartmentDTO, Department>(obj, department);
                    department.LastUpdateDate = DateTime.Now;
                    department.LastUpdateBy = currentUserId;
                    base.Update(department);
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return department.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
