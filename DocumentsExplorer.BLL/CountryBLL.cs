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
    public class CountryBLL : Service<Country>, ICountryBLL
    {
        private readonly IRepositoryAsync<Country> _repository;

        public CountryBLL(IRepositoryAsync<Country> repository) : base(repository)
        {
            _repository = repository;
        }

        public Country GetById(int countryId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Country> GetCountries()
        {
            return _repository
                    .Query()
                    .SelectQueryable();
        }
    }
}
