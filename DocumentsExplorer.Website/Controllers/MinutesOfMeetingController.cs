using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocumentsExplorer.Website.Controllers
{
    [Authorize(Roles = "MinutesOfMeetingEditor")]
    public class MinutesOfMeetingController : SharedController
    {
        readonly IMinutesOfMeetingBLL _minutesOfMeetingBLL;
        readonly ICouncilMemberBLL _councilMemberBLL;
        readonly IAttachmentBLL _attachmentBLL;
        readonly IRoundBLL _roundBLL;

        public MinutesOfMeetingController(
            IMinutesOfMeetingBLL minutesOfMeetingBLL,
            ICouncilMemberBLL councilMemberBLL,
            IAttachmentBLL attachmentBLL,
            IRoundBLL roundBLL,
            ICouncilTypeBLL councilTypeBLL) : base(councilTypeBLL)
        {
            _minutesOfMeetingBLL = minutesOfMeetingBLL;
            _councilMemberBLL = councilMemberBLL;
            _attachmentBLL = attachmentBLL;
            _roundBLL = roundBLL;
        }

        // GET: MinutesOfMeeting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int? id)
        {
            MinutesOfMeetingDTO model;
            if (!id.HasValue)
                model = new MinutesOfMeetingDTO();
            else
            {
                model = _minutesOfMeetingBLL.GetById(id.Value);
                //model.SelectedAbsents = model.MeetingAttendances.Where(c => !c.IsAttendant).Select(c => c.CouncilMemberId).ToList();
                //model.SelectedAttendances = model.MeetingAttendances.Where(c => c.IsAttendant).Select(c => c.CouncilMemberId).ToList();
                model.Absents = _councilMemberBLL.GetCouncilMembers().Where(c => model.SelectedAbsents.Contains(c.Id)).ToList();
                model.Attendances = _councilMemberBLL.GetCouncilMembers().Where(c => model.SelectedAttendances.Contains(c.Id)).ToList();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MinutesOfMeetingDTO obj, IEnumerable<HttpPostedFileBase> files, string MeetingSummary)
        {
            if (ModelState.IsValid)
            {
                obj.MeetingSummary = HttpUtility.HtmlDecode(MeetingSummary);
                var minutesOfMeetingId = _minutesOfMeetingBLL.Save(obj, CurrentUser.Id);

                if (files != null)
                {
                    //if (!Directory.Exists(Server.MapPath("~/AttachmentPath")))
                    //    Directory.CreateDirectory(Server.MapPath("~/AttachmentPath"));
                    //if (!Directory.Exists(Server.MapPath("~/AttachmentPath/" + minutesOfMeetingId.ToString())))
                    //    Directory.CreateDirectory(Server.MapPath("~/AttachmentPath/" + minutesOfMeetingId.ToString()));
                    if (!Directory.Exists(ConfigurationManager.AppSettings["AttachmentPath"]))
                        Directory.CreateDirectory(ConfigurationManager.AppSettings["AttachmentPath"]);
                    if (!Directory.Exists(ConfigurationManager.AppSettings["AttachmentPath"]+ "//" + minutesOfMeetingId.ToString()))
                        Directory.CreateDirectory(ConfigurationManager.AppSettings["AttachmentPath"] + "//"  + minutesOfMeetingId.ToString());
                    foreach (var file in files)
                    {
                        MeetingAttachment attachment = new MeetingAttachment();
                        attachment.Name = Path.GetFileName(file.FileName);
                        attachment.FileExtension = Path.GetExtension(file.FileName);
                        attachment.Path = Path.Combine(minutesOfMeetingId.ToString(), attachment.Name + Path.GetExtension(file.FileName));
                        attachment.MeetingId = minutesOfMeetingId;
                        //var physicalPath = Path.Combine(Server.MapPath("~/AttachmentPath/" + minutesOfMeetingId.ToString()), attachment.Name);
                        var physicalPath = Path.Combine(ConfigurationManager.AppSettings["AttachmentPath"] + "//" + minutesOfMeetingId.ToString(), attachment.Name);

                        file.SaveAs(physicalPath);
                        _attachmentBLL.Save(attachment, CurrentUser.Id);
                    }
                }
            }
            return RedirectToAction("Index");

        }


        public JsonResult ReadRounds(string text)
        {
            var rounds = _roundBLL.GetRounds();
            if (!string.IsNullOrEmpty(text))
            {
                rounds = rounds.Where(p => p.RoundNumber.Contains(text));
            }
            return Json(rounds, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadCouncilMembers(string text)
        {
            var members = _councilMemberBLL.GetCouncilMembers();
            if (!string.IsNullOrEmpty(text))
            {
                members = members.Where(p => p.Name.Contains(text));
            }
            return Json(members, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadCouncilTypes(string text)
        {

            var councilTypes = _councilTypeBLL.GetCouncilTypes();

            if (!string.IsNullOrEmpty(text))
            {
                councilTypes = councilTypes.Where(p => p.Description.Contains(text));
            }

            return Json(councilTypes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadMinutesOfMeetings([DataSourceRequest]DataSourceRequest request)
        {
            return Json(_minutesOfMeetingBLL.GetMinutesOfMeetings().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, MinutesOfMeetingDTO obj)
        {
            if (obj != null)
            {
                _minutesOfMeetingBLL.Delete(obj.Id);
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public JsonResult ReadAttachments([DataSourceRequest]DataSourceRequest request, string minutesOfMeetingId)
        {
            return Json(_attachmentBLL.GetAttachmens(Convert.ToInt32(minutesOfMeetingId)).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteAttachment([DataSourceRequest] DataSourceRequest request, Attachment obj)
        {
            if (obj != null)
            {
                _attachmentBLL.Delete(obj.Id);
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Download(string fileName, string fileExtension, string minutesOfMeetingId)
        {
            if (fileName != null)
            {
                //string fullPath = Path.Combine(Server.MapPath("~/AttachmentPath/" + minutesOfMeetingId), fileName + fileExtension);
                string fullPath = Path.Combine(ConfigurationManager.AppSettings["AttachmentPath"]+"//" + minutesOfMeetingId, fileName);
                var contentType = MimeMapping.GetMimeMapping(fullPath);
                var fileBytes = System.IO.File.ReadAllBytes(fullPath);

                // Convert to ContentDisposition
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = fileName,
                    Inline = false,
                };
                Response.AppendHeader("Content-Disposition", cd.ToString());

                // View document
                return File(fileBytes, contentType);
            }

            return null;

        }

        public ActionResult Preview(string fileName, string fileExtension, string minutesOfMeetingId)
        {
           

            string fullPath = Path.Combine(ConfigurationManager.AppSettings["AttachmentPath"]+"\\" + minutesOfMeetingId, fileName);
            var contentType = MimeMapping.GetMimeMapping(fullPath);
            //string fullPath = Path.Combine(Server.MapPath("~/AttachmentPath/" + minutesOfMeetingId), fileName + fileExtension);
            return File(fullPath, contentType);
        }
    }
}