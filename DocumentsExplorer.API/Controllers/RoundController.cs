using AAAID.Common;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DocumentsExplorer.API.Controllers
{
    [RoutePrefix("api/Round")]
    public class RoundController : ApiController
    {
        readonly IRoundBLL _RoundBLL;
        private readonly IApplicationContext _appicationContext;

        public RoundController(
            IApplicationContext appicationContext,
            IRoundBLL roundBLL)
        {
            _RoundBLL = roundBLL;
            _appicationContext = appicationContext;
        }

        [HttpGet]
        [Route("Gets")]
        public IQueryable<RoundDTO> Gets()
        {
            return _RoundBLL.GetRounds();
        }

        [HttpGet]
        [Route("DSGets")]
        public HttpResponseMessage Gets(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(DataSourceLoader.Load(_RoundBLL.GetRounds(), loadOptions));
        }

        [HttpGet]
        [Route("GetById")]
        public RoundDTO GetById(int id)
        {
            return _RoundBLL.GetById(id);
        }
    }
}
