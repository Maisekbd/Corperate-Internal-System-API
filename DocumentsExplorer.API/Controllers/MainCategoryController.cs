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
    [RoutePrefix("api/MainCategory")]
    public class MainCategoryController : ApiController
    { 
    readonly IMainCategoryBLL _mainCategoryBLL;
        private readonly IApplicationContext _appicationContext;

    public MainCategoryController(
        IApplicationContext appicationContext,
        IMainCategoryBLL mainCategoryBLL)
    {
        _mainCategoryBLL = mainCategoryBLL;
        _appicationContext = appicationContext;
    }

    [HttpGet]
    [Route("Gets")]
    public IQueryable<MainCategory> Gets(int? councilTypeId)
        {
            return _mainCategoryBLL.GetMainCategories(councilTypeId);
    }

    [HttpGet]
    [Route("DSGets")]
    public HttpResponseMessage Gets(DataSourceLoadOptions loadOptions, int? councilTypeId)
    {
        return Request.CreateResponse(DataSourceLoader.Load(_mainCategoryBLL.GetMainCategories(councilTypeId), loadOptions));
    }

    [HttpPost]
    [Route("Create")]
    public HttpResponseMessage CreateMainCategory(FormDataCollection form)
    {
        try
        {
            var values = form.Get("values");
            var newMainCategory = new MainCategory();
            JsonConvert.PopulateObject(values, newMainCategory);
            Validate(newMainCategory);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            _mainCategoryBLL.Insert(newMainCategory);
            newMainCategory = _mainCategoryBLL.GetById(newMainCategory.Id);
            return Request.CreateResponse(HttpStatusCode.Created, newMainCategory);
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    [HttpPut]
    [Route("Update")]
    public HttpResponseMessage UpdateMainCategory(FormDataCollection form)
    {
        try
        {
            var mainCategoryId = Convert.ToInt32(form.Get("key"));
            var values = form.Get("values");
            var mainCategory = _mainCategoryBLL.GetById(mainCategoryId);
            JsonConvert.PopulateObject(values, mainCategory);
            Validate(mainCategory);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            _mainCategoryBLL.Update(mainCategory);
            mainCategory = _mainCategoryBLL.GetById(mainCategoryId);
            return Request.CreateResponse(HttpStatusCode.OK, mainCategory);
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
        _mainCategoryBLL.Delete(customerId);
    }


}
}
