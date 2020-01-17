using AAAID.Common;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace DocumentsExplorer.API.Controllers
{
    [RoutePrefix("api/CouncilType")]
    public class CouncilTypeController : ApiController
    {
        readonly ICouncilTypeBLL _councilTypeBLL;
        readonly ICouncilMemberBLL _councilMemberBLL;
        private readonly IApplicationContext _appicationContext;

        public CouncilTypeController(
            IApplicationContext appicationContext,
            ICouncilTypeBLL councilTypeBLL,
            ICouncilMemberBLL councilMemberBLL
            )
        {
            _councilTypeBLL = councilTypeBLL;
            _councilMemberBLL = councilMemberBLL;
            _appicationContext = appicationContext;
        }

        [HttpGet]
        [Route("Gets")]
        public IQueryable<CouncilType> Gets()
        {
            return _councilTypeBLL.GetCouncilTypes();
        }

        [HttpGet]
        [Route("GetCoucilMembers")]
        public IQueryable<CouncilMemberDTO> GetCoucilMembers(int councilId)
        {
            return _councilMemberBLL.GetCouncilMembers(councilId);
        }


        [HttpGet]
        [Route("DSGets")]
        public HttpResponseMessage Gets(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(DataSourceLoader.Load(_councilTypeBLL.GetCouncilTypes(), loadOptions));
        }

        [HttpGet]
        [Route("GetById")]
        public CouncilType GetById(int id)
        {
            return _councilTypeBLL.GetById(id);
        }



        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage CreateCouncilType(FormDataCollection form)
        {
            try
            {
                var values = form.Get("values");
                var newCouncilType = new CouncilType();
                JsonConvert.PopulateObject(values, newCouncilType);
                Validate(newCouncilType);
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                _councilTypeBLL.Insert(newCouncilType);
                newCouncilType = _councilTypeBLL.GetById(newCouncilType.Id);
                return Request.CreateResponse(HttpStatusCode.Created, newCouncilType);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPut]
        [Route("Update")]
        public HttpResponseMessage UpdateCouncilType(FormDataCollection form)
        {
            try
            {
                var councilTypeId = Convert.ToInt32(form.Get("key"));
                var values = form.Get("values");
                var councilType = _councilTypeBLL.GetById(councilTypeId);
                JsonConvert.PopulateObject(values, councilType);
                Validate(councilType);
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                _councilTypeBLL.Update(councilType);
                councilType = _councilTypeBLL.GetById(councilTypeId);
                return Request.CreateResponse(HttpStatusCode.OK, councilType);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpDelete]
        public void Delete(FormDataCollection form)
        {
            var customerId = Convert.ToInt32(form.Get("key"));
            _councilTypeBLL.Delete(customerId);
        }

       
    }
}