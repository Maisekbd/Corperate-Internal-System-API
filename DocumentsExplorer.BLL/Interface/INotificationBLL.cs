using AAAID.HR.Entities;
using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL.Interface
{
    public interface INotificationBLL : IService<Notification>
    {
        IQueryable<NotificationDTO> GetNotifications(string UserId);
        NotificationDTO GetById(int roundId);
        int AddNotification(NotificationDTO obj);
        Notification AddNotification(Employee employee, int? decisionId, string title, string body,DateTime dueDate, int notificationType);
        int Delete(int id);
        bool NotificationIsSend(int generatedNotificationId);
    }
}

