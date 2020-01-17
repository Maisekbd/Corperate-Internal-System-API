using AAAID.Common;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DocumentsExplorer.API.Controllers
{
    [RoutePrefix("api/Company")]
    public class CompanyController : ApiController
    {
        readonly ICompanyBLL _CompanyBLL;
        private readonly IApplicationContext _appicationContext;

        public CompanyController(
            IApplicationContext appicationContext,
            ICompanyBLL CompanyBLL)
        {
            _CompanyBLL = CompanyBLL;
            _appicationContext = appicationContext;
        }

        [HttpGet]
        [Route("Gets")]
        public IQueryable<CompanyDTO> Gets()
        {
            return _CompanyBLL.GetCompanies();
        }

        [HttpGet]
        [Route("DSGets")]
        public HttpResponseMessage Gets(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(DataSourceLoader.Load(_CompanyBLL.GetCompanies(), loadOptions));
        }

        [HttpGet]
        [Route("GetById")]
        public CompanyDTO GetById(int id)
        {
            return _CompanyBLL.GetById(id);
        }


    }
}