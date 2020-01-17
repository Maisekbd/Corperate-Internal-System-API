using DocumentsExplorer.BLL.Interface;
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
    public class MainCategoryBLL : Service<MainCategory>, IMainCategoryBLL
    {
        private readonly IRepositoryAsync<MainCategory> _repository;

        public MainCategoryBLL(IRepositoryAsync<MainCategory> repository) : base(repository)
        {
            _repository = repository;
        }

        public IQueryable<MainCategory> GetMainCategories(int? councilTypeId)
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable()
                    .Where(c => (councilTypeId != null && c.CouncilTypeId == councilTypeId.Value));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public MainCategory GetById(int mainCatId)
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable().Where(c => c.Id == mainCatId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
