using AAAID.Common;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DocumentsExplorer.API.Controllers
{
    [RoutePrefix("api/Country")]
    public class CountryController : ApiController
    {
        readonly ICountryBLL _CountryBLL;
        private readonly IApplicationContext _appicationContext;

        public CountryController(
            IApplicationContext appicationContext,
            ICountryBLL CountryBLL)
        {
            _CountryBLL = CountryBLL;
            _appicationContext = appicationContext;
        }

        [HttpGet]
        [Route("Gets")]
        public IQueryable<Country> Gets()
        {
            return _CountryBLL.GetCountries();
        }

        [HttpGet]
        [Route("DSGets")]
        public HttpResponseMessage Gets(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(DataSourceLoader.Load(_CountryBLL.GetCountries(), loadOptions));
        }

        [HttpGet]
        [Route("GetById")]
        public Country GetById(int id)
        {
            return _CountryBLL.GetById(id);
        }


    }
}