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
    public class SubCategoryBLL : Service<SubCategory>, ISubCategoryBLL
    {
        private readonly IRepositoryAsync<SubCategory> _repository;
        readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public SubCategoryBLL(
            IRepositoryAsync<SubCategory> repository,
            IUnitOfWorkAsync unitOfWorkAsync) : base(repository)
        {
            _repository = repository;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public SubCategoryDTO GetById(int value)
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable()
                    .Where(c => c.Id == value).ProjectTo<SubCategoryDTO>().FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<SubCategoryDTO> GetSubCategories(int? mainCategoryId)
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable()
                    .Where(c => (mainCategoryId != null && c.MainCategoryId == mainCategoryId.Value)).ProjectTo<SubCategoryDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Update(SubCategoryDTO dtoObject)
        {
            try
            {
                _unitOfWorkAsync.BeginTransaction();
                SubCategory subCategory = new SubCategory();
                Mapper.Map<SubCategoryDTO, SubCategory>(dtoObject, subCategory);
                Update(subCategory);
                _unitOfWorkAsync.SaveChanges();
                _unitOfWorkAsync.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWorkAsync.Rollback();
                throw ex;
            }
        }

    }
}
