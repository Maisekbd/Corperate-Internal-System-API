using AAAID.HR.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL
{
    public class NotificationBLL : Service<Notification>, INotificationBLL
    {
        private readonly IRepositoryAsync<Notification> _repository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public NotificationBLL(IUnitOfWorkAsync unitOfWork, IRepositoryAsync<Notification> repository) : base(repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public int Delete(int id)
        {
            try
            {
                //_unitOfWork.BeginTransaction();
                _repository.Delete(id);
                _unitOfWork.SaveChanges();
                //_unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
            return 0;
        }

        public NotificationDTO GetById(int roundId)
        {
            try
            {
                NotificationDTO notification = new NotificationDTO();
                Mapper.Map<Notification, NotificationDTO>(_repository
                     .Query()
                     .SelectQueryable()
                     .Where(c => c.Id == roundId).FirstOrDefault(), notification);
                return notification;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<NotificationDTO> GetNotifications(string userId)
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable()
                    .Where(c => c.UserId == userId && !c.IsOpen)
                    .OrderByDescending(c=>c.DueDate)
                    .ProjectTo<NotificationDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddNotification(NotificationDTO obj)
        {

            try
            {
                _unitOfWork.BeginTransaction();
                Notification notification = new Notification();
                Mapper.Map<NotificationDTO, Notification>(obj, notification);
                base.Insert(notification);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return notification.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Notification AddNotification(Employee employee, int? decisionId, string title, string body, DateTime dueDate, int notificationType)
        {
            try
            {
                Notification notification = new Notification()
                {
                    Body = body,
                    DecisionId = decisionId,
                    Decision = null,
                    DueDate = dueDate,
                    EmployeeId = employee.Id,
                    EmployeeMail = employee.Email,
                    IsOpen = false,
                    NotificationType = notificationType,
                    status = (int)EnumMailStatus.NotSend,
                    Title = title,
                    UserId = employee.UserId
                };
                _repository.Insert(notification);
                return notification;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool NotificationIsSend(int generatedNotificationId)
        {
            try
            {
                var notification = _repository.Find(generatedNotificationId);
                notification.status = (int)EnumMailStatus.Send;
                notification.Decision = null;
                base.Update(notification);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
