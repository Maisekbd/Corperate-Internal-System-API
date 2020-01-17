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
    [RoutePrefix("api/ReferenceType")]
    public class ReferenceTypeController : ApiController
    {
        readonly IReferenceTypeBLL _ReferenceTypeBLL;
        private readonly IApplicationContext _appicationContext;

        public ReferenceTypeController(
            IApplicationContext appicationContext,
            IReferenceTypeBLL referenceTypeBLL)
        {
            _ReferenceTypeBLL = referenceTypeBLL;
            _appicationContext = appicationContext;
        }

        [HttpGet]
        [Route("Gets")]
        public IQueryable<ReferenceTypeDTO> Gets()
        {
            return _ReferenceTypeBLL.GetReferenceTypes();
        }

        [HttpGet]
        [Route("DSGets")]
        public HttpResponseMessage Gets(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(DataSourceLoader.Load(_ReferenceTypeBLL.GetReferenceTypes(), loadOptions));
        }

        [HttpGet]
        [Route("GetById")]
        public ReferenceTypeDTO GetById(int id)
        {
            return _ReferenceTypeBLL.GetById(id);
        }
    }
}
