using AAAID.Common;
using DocumentsExplorer.BLL.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DocumentsExplorer.API.Controllers
{
    [RoutePrefix("api/Attachment")]
    //[AllowAnonymous]
    public class AttachmentController : ApiController
    {

        private readonly IApplicationContext _appicationContext;
        readonly IAttachmentBLL _AttachmentBLL;

        public AttachmentController(IApplicationContext appicationContext,
            IAttachmentBLL attachmentBLL
            )
        {
            _appicationContext = appicationContext;
            _AttachmentBLL = attachmentBLL;
        }


        [HttpPost]
        [Route("UploadAttachment")]
        // "file" is the value of the FileUploader's "name" option
        public IHttpActionResult UploadAttachment(string meetingNo, string agendaNumber)
        {
            string returnedValue = "";
            //     var file = HttpContext.Current.Request.Files.Count > 0 ?
            //HttpContext.Current.Request.Files[0] : null;
            // Specifies the target location for the uploaded files
            //string targetLocation = Server.MapPath("~/Files/");

            //// Specifies the maximum size allowed for the uploaded files (700 kb)
            //int maxFileSize = 1024 * 700;

            //// Checks whether or not the request contains a file and if this file is empty or not
            //if (file == null || file.ContentLength <= 0)
            //{
            //    throw new HttpException("File is not specified");
            //}

            //// Checks that the file size does not exceed the allowed size
            //if (file.ContentLength > maxFileSize)
            //{
            //    throw new HttpException("File is too big");
            //}

            //// Checks that the file is an image
            //if (!file.ContentType.Contains("image"))
            //{
            //    throw new HttpException("Invalid file type");
            //}

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    using (var binaryReader = new BinaryReader(postedFile.InputStream))
                    {
                        returnedValue = _AttachmentBLL.SaveTempAgendaAttachment(_appicationContext.GetUserId(), meetingNo, agendaNumber, postedFile.FileName, binaryReader.ReadBytes(postedFile.ContentLength));
                    }

                }
                return Ok(returnedValue);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("UploadAttachmentWithParameter")]
        // "file" is the value of the FileUploader's "name" option
        public IHttpActionResult UploadAttachmentWithParameter(string itemNumber, int attachmentType)
        {
            string returnedValue = "";
            //     var file = HttpContext.Current.Request.Files.Count > 0 ?
            //HttpContext.Current.Request.Files[0] : null;
            // Specifies the target location for the uploaded files
            //string targetLocation = Server.MapPath("~/Files/");

            //// Specifies the maximum size allowed for the uploaded files (700 kb)
            //int maxFileSize = 1024 * 700;

            //// Checks whether or not the request contains a file and if this file is empty or not
            //if (file == null || file.ContentLength <= 0)
            //{
            //    throw new HttpException("File is not specified");
            //}

            //// Checks that the file size does not exceed the allowed size
            //if (file.ContentLength > maxFileSize)
            //{
            //    throw new HttpException("File is too big");
            //}

            //// Checks that the file is an image
            //if (!file.ContentType.Contains("image"))
            //{
            //    throw new HttpException("Invalid file type");
            //}

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    using (var binaryReader = new BinaryReader(postedFile.InputStream))
                    {
                        returnedValue = _AttachmentBLL.SaveTempAttachmentWithParameter(_appicationContext.GetUserId(), itemNumber, attachmentType, postedFile.FileName, binaryReader.ReadBytes(postedFile.ContentLength));
                    }

                }
                return Ok(returnedValue);
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpPost]
        [Route("UploadAttachment")]
        public IHttpActionResult UploadDecision()
        {
            string returnedValue = "";
            //     var file = HttpContext.Current.Request.Files.Count > 0 ?
            //HttpContext.Current.Request.Files[0] : null;
            // Specifies the target location for the uploaded files
            //string targetLocation = Server.MapPath("~/Files/");

            //// Specifies the maximum size allowed for the uploaded files (700 kb)
            //int maxFileSize = 1024 * 700;

            //// Checks whether or not the request contains a file and if this file is empty or not
            //if (file == null || file.ContentLength <= 0)
            //{
            //    throw new HttpException("File is not specified");
            //}

            //// Checks that the file size does not exceed the allowed size
            //if (file.ContentLength > maxFileSize)
            //{
            //    throw new HttpException("File is too big");
            //}

            //// Checks that the file is an image
            //if (!file.ContentType.Contains("image"))
            //{
            //    throw new HttpException("Invalid file type");
            //}

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    using (var binaryReader = new BinaryReader(postedFile.InputStream))
                    {
                        returnedValue = _AttachmentBLL.SaveTempDecision(_appicationContext.GetUserId(), postedFile.FileName, binaryReader.ReadBytes(postedFile.ContentLength));
                    }

                }
                return Ok(returnedValue);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
