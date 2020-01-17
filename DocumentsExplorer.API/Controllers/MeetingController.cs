using AAAID.Common;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace DocumentsExplorer.API.Controllers
{
    [RoutePrefix("api/Meeting")]
    public class MeetingController : ApiController
    {
        readonly IMeetingBLL _meetingBLL;
        private readonly IApplicationContext _appicationContext;

        public MeetingController(
            IApplicationContext appicationContext,
            IMeetingBLL meetingBLL)
        {
            _meetingBLL = meetingBLL;
            _appicationContext = appicationContext;
        }


        [HttpGet]
        //[Authorize(Roles = "OperationManagmentAdmin, OperationManagmentCoordinator, OperationDepartmentCoordinator")]
        [Route("DSGets")]
        public HttpResponseMessage Gets(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(DataSourceLoader.Load(_meetingBLL.GetMeetings(), loadOptions));
        }

        [HttpGet]
        [Route("GetLatestMeetings")]
        public List<MeetingDTO> GetLatestMeetings()
        {
            return _meetingBLL.GetLatestMeetings().ToList();
        }

        [HttpGet]
        [Route("GetMeetingsbyCouncilandYear")]
        public IQueryable<MeetingDTO> GetMeetingsbyCouncilandYear(int councilId, int year)
        {
            return _meetingBLL.GetMeetingsbyCouncilandYear(councilId, year);
        }



        [HttpGet]
        //[Authorize(Roles = "OperationManagmentAdmin, OperationManagmentCoordinator, OperationDepartmentCoordinator")]
        [Route("GetById")]
        public MeetingDTO GetById(int id)
        {
            return _meetingBLL.GetById(id);
        }

        [HttpPost]
        [Route("PostMeeting")]
        public IHttpActionResult PostMeeting(MeetingDTO meeting)
        {
            if (ModelState.IsValid)
            {
                meeting.Id = _meetingBLL.InsertMeeting(meeting, _appicationContext.GetUserId());
                return Ok(meeting);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        [HttpPut]
        [Route("PutMeeting")]
        public IHttpActionResult PutMeeting(MeetingDTO meeting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    meeting.Id = _meetingBLL.UpdateMeeting(meeting, _appicationContext.GetUserId());
                    return Ok(meeting);
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpDelete]
        public void Delete(FormDataCollection form)
        {
            try
            {
                var key = Convert.ToInt32(form.Get("key"));
                _meetingBLL.Delete(key);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet]
        [Route("DownloadFile")]
        public HttpResponseMessage DownloadFile(string fileName)
        {

            try
            {
                if (fileName != null)
                {
                    string path = ConfigurationManager.AppSettings["MeetingPath"];
                    string fullPath = Path.Combine(path, fileName);
                    var contentType = MimeMapping.GetMimeMapping(fullPath);
                    var fileBytes = System.IO.File.ReadAllBytes(fullPath);

                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StreamContent(new FileStream(fullPath, FileMode.Open, FileAccess.Read));
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    return response;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("MergeAllFiles")]
        public bool MergeAllFiles(int id)
        {
            try
            {
                return _meetingBLL.MergeAllFiles(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("SendMeetingRequest")]
        public bool SendMeetingRequest(int id)
        {
            try
            {
                return _meetingBLL.SendMeetingRequest(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
