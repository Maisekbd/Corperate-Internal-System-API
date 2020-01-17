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
    [RoutePrefix("api/DecisionType")]
    public class DecisionTypeController : ApiController
    {
        readonly IDecisionTypeBLL _DecisionTypeBLL;
        private readonly IApplicationContext _appicationContext;

        public DecisionTypeController(
            IApplicationContext appicationContext,
            IDecisionTypeBLL DecisionTypeBLL)
        {
            _DecisionTypeBLL = DecisionTypeBLL;
            _appicationContext = appicationContext;
        }

        [HttpGet]
        [Route("Gets")]
        public IQueryable<DecisionTypeDTO> Gets()
        {
            return _DecisionTypeBLL.GetDecisionTypes();
        }

        [HttpGet]
        [Route("DSGets")]
        public HttpResponseMessage Gets(DataSourceLoadOptions loadOptions, int? councilTypeId)
        {
            return Request.CreateResponse(DataSourceLoader.Load(_DecisionTypeBLL.GetDecisionTypes(), loadOptions));
        }

        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage CreateDecisionType(FormDataCollection form)
        {
            try
            {
                var values = form.Get("values");
                var newDecisionType = new DecisionType();
                JsonConvert.PopulateObject(values, newDecisionType);
                Validate(newDecisionType);
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                _DecisionTypeBLL.Insert(newDecisionType);
                //newDecisionType = _DecisionTypeBLL.GetById(newDecisionType.Id);
                return Request.CreateResponse(HttpStatusCode.Created, newDecisionType);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPut]
        [Route("Update")]
        public HttpResponseMessage UpdateDecisionType(FormDataCollection form)
        {
            try
            {
                var DecisionTypeId = Convert.ToInt32(form.Get("key"));
                var values = form.Get("values");
                var DecisionType = _DecisionTypeBLL.GetById(DecisionTypeId);
                JsonConvert.PopulateObject(values, DecisionType);
                Validate(DecisionType);
                if (!ModelState.IsValid)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                _DecisionTypeBLL.Save(DecisionType);
                DecisionType = _DecisionTypeBLL.GetById(DecisionTypeId);
                return Request.CreateResponse(HttpStatusCode.OK, DecisionType);
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
            _DecisionTypeBLL.Delete(customerId);
        }


    }
}