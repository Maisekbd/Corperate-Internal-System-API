using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL
{
    public class AttachmentBLL : Service<MeetingAttachment>, IAttachmentBLL
    {
        private readonly IRepositoryAsync<MeetingAttachment> _repository;
        private readonly IRepositoryAsync<Meeting> _minutesOfMeetingRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public AttachmentBLL(IUnitOfWorkAsync unitOfWork,
            IRepositoryAsync<Meeting> minutesOfMeetingRepository,
            IRepositoryAsync<MeetingAttachment> repository
            ) : base(repository)
        {
            _repository = repository;
            _minutesOfMeetingRepository = minutesOfMeetingRepository;
            _unitOfWork = unitOfWork;
        }

        public int Delete(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _repository.Delete(id);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
            return 0;
        }

        public IList<MeetingAttachment> GetAttachmens(int minutesOfMeetingId)
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable()
                    .Where(c => c.Id == minutesOfMeetingId).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Save(MeetingAttachment attachment, string currentUserId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                if (attachment.Id == 0)
                {
                    attachment.CreationDate = DateTime.Now;
                    attachment.CreatedBy = currentUserId;
                    attachment.TrackingState = TrackableEntities.TrackingState.Added;
                    var Mi = _minutesOfMeetingRepository.Query().Include(c => c.Attachments).SelectQueryable().Where(c => c.Id == attachment.MeetingId).FirstOrDefault();
                    Mi.Attachments.Add(attachment);
                    Mi.TrackingState = TrackableEntities.TrackingState.Modified;
                    _minutesOfMeetingRepository.InsertOrUpdateGraph(Mi);
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return attachment.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string SaveTempAgendaAttachment(string userId, string itemNumber, string agendaTreeNumber, string FileName, byte[] file)
        {
            try
            {
                if (!Directory.Exists(ConfigurationManager.AppSettings["TempMeetingPath"]))
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["TempMeetingPath"]);
                if (!Directory.Exists(ConfigurationManager.AppSettings["TempMeetingPath"] + "//" + userId))
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["TempMeetingPath"] + "//" + userId);
                if (!Directory.Exists(ConfigurationManager.AppSettings["TempMeetingPath"] + "//" + userId + "//" + itemNumber))
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["TempMeetingPath"] + "//" + userId + "//" + itemNumber);
                //if (!Directory.Exists(ConfigurationManager.AppSettings["TempMeetingPath"] + "//" + userId + "//" + meetingNO + "//" + agendaTreeNumber))
                //    Directory.CreateDirectory(ConfigurationManager.AppSettings["TempMeetingPath"] + "//" + userId + "//" + meetingNO + "//" + agendaTreeNumber);
                File.WriteAllBytes(String.Format("{0}//{1}//{2}//{3}", ConfigurationManager.AppSettings["TempMeetingPath"], userId, itemNumber, FileName), file);

                return String.Format("{0}//{1}", itemNumber, FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveTempAttachmentWithParameter(string userId, string itemNumber, int attachmentType, string FileName, byte[] file)
        {
            try
            {
                string rootFolder = "";
                if (attachmentType == (int)EnumAttachmentType.ReferenceItem)
                {
                    rootFolder = ConfigurationManager.AppSettings["TempDecisonPath"];
                   
                }
                else if (attachmentType == (int)EnumAttachmentType.DecisionExecution)
                {
                    rootFolder = ConfigurationManager.AppSettings["TempDecisionExecutionPath"];

                }
                else { }
                if (!Directory.Exists(rootFolder))
                    Directory.CreateDirectory(rootFolder);
                if (!Directory.Exists(rootFolder + "//" + userId))
                    Directory.CreateDirectory(rootFolder + "//" + userId);
                if (!Directory.Exists(rootFolder + "//" + userId + "//" + itemNumber))
                    Directory.CreateDirectory(rootFolder + "//" + userId + "//" + itemNumber);
                //if (!Directory.Exists(ConfigurationManager.AppSettings["TempMeetingPath"] + "//" + userId + "//" + meetingNO + "//" + agendaTreeNumber))
                //    Directory.CreateDirectory(ConfigurationManager.AppSettings["TempMeetingPath"] + "//" + userId + "//" + meetingNO + "//" + agendaTreeNumber);
                File.WriteAllBytes(String.Format("{0}//{1}//{2}//{3}", rootFolder, userId, itemNumber, FileName), file);

                return String.Format("{0}//{1}", itemNumber, FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveTempDecision(string userId, string FileName, byte[] file)
        {
            try
            {
                if (!Directory.Exists(ConfigurationManager.AppSettings["TempDecisonPath"]))
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["TempDecisonPath"]);
                if (!Directory.Exists(ConfigurationManager.AppSettings["TempDecisonPath"] + "//" + userId))
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["TempDecisonPath"] + "//" + userId);
                File.WriteAllBytes(String.Format("{0}//{1}//{2}", ConfigurationManager.AppSettings["TempDecisonPath"], userId, FileName), file);

                return String.Format("{0}",FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
