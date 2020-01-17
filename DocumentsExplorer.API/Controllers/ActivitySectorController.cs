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
    [RoutePrefix("api/ActivitySector")]
    public class ActivitySectorController : ApiController
    {
        readonly IActivitySectorBLL _activitySectorBLL;
        private readonly IApplicationContext _appicationContext;

        public ActivitySectorController(
            IApplicationContext appicationContext,
            IActivitySectorBLL activitySectorBLL)
        {
            _activitySectorBLL = activitySectorBLL;
            _appicationContext = appicationContext;
        }

        [HttpGet]
        [Route("Gets")]
        public IQueryable<ActivitySectorDTO> Gets()
        {
            return _activitySectorBLL.GetActivitySectors();
        }

        [HttpGet]
        [Route("DSGets")]
        public HttpResponseMessage Gets(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(DataSourceLoader.Load(_activitySectorBLL.GetActivitySectors(), loadOptions));
        }

        [HttpGet]
        [Route("GetById")]
        public ActivitySectorDTO GetById(int id)
        {
            return _activitySectorBLL.GetById(id);
        }


    }
}