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
    [RoutePrefix("api/SubCategory")]
    public class SubCategoryController : ApiController
    { 
    readonly ISubCategoryBLL _subCategoryBLL;
        private readonly IApplicationContext _appicationContext;

    public SubCategoryController(
        IApplicationContext appicationContext,
        ISubCategoryBLL subCategoryBLL)
    {
        _subCategoryBLL = subCategoryBLL;
        _appicationContext = appicationContext;
    }

    [HttpGet]
    [Route("Gets")]
    public IQueryable<SubCategoryDTO> Gets(int? maincategoryId)
    {
        return _subCategoryBLL.GetSubCategories(maincategoryId);
    }

    [HttpGet]
    [Route("DSGets")]
    public HttpResponseMessage Gets(DataSourceLoadOptions loadOptions, int? maincategoryId)
    {
        return Request.CreateResponse(DataSourceLoader.Load(_subCategoryBLL.GetSubCategories(maincategoryId), loadOptions));
    }

    [HttpPost]
    [Route("Create")]
    public HttpResponseMessage CreateSubCategory(FormDataCollection form)
    {
        try
        {
            var values = form.Get("values");
            var newSubCategory = new SubCategory();
            JsonConvert.PopulateObject(values, newSubCategory);
            Validate(newSubCategory);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            _subCategoryBLL.Insert(newSubCategory);
            return Request.CreateResponse(HttpStatusCode.Created, newSubCategory);
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    [HttpPut]
    [Route("Update")]
    public HttpResponseMessage UpdateSubCategory(FormDataCollection form)
    {
        try
        {
            var subCategoryId = Convert.ToInt32(form.Get("key"));
            var values = form.Get("values");
            var subCategory = _subCategoryBLL.GetById(subCategoryId);
            JsonConvert.PopulateObject(values, subCategory);
            Validate(subCategory);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            _subCategoryBLL.Update(subCategory);
            subCategory = _subCategoryBLL.GetById(subCategoryId);
            return Request.CreateResponse(HttpStatusCode.OK, subCategory);
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
        _subCategoryBLL.Delete(customerId);
    }


}
}

