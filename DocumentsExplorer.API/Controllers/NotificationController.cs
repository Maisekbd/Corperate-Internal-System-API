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
    [RoutePrefix("api/Notification")]
    public class NotificationController : ApiController
    {
        readonly INotificationBLL _notificationBLL;
        private readonly IApplicationContext _appicationContext;

        public NotificationController(
            IApplicationContext appicationContext,
            INotificationBLL notificationBLL)
        {
            _appicationContext = appicationContext;
            _notificationBLL = notificationBLL;
        }

        [HttpGet]
        [Route("GetLatestNotifications")]
        public IQueryable<NotificationDTO> GetLatestNotifications()
        {
            return _notificationBLL.GetNotifications(_appicationContext.GetUserId());
        }
    }
}