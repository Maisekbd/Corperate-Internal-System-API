using AAAID.HR.ServiceInterface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentsExplorer.BLL.Helpers;
using DocumentsExplorer.BLL.Interface;
using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL
{
    public class MeetingBLL : Service<Meeting>, IMeetingBLL
    {
        private readonly IRepositoryAsync<Meeting> _repository;
        private readonly ICouncilMemberBLL _councilMemberBLL;
        private readonly IRepositoryAsync<MeetingAttendance> _meetingAttendanceRepository;
        private readonly IRepositoryAsync<Round> _roundRepository;
        private readonly INotificationBLL _notificationBLL;
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public MeetingBLL(
            IUnitOfWorkAsync unitOfWork,
            IRepositoryAsync<Meeting> repository,
            IRepositoryAsync<MeetingAttendance> meetingAttendanceRepository,
            IRepositoryAsync<Round> roundRepository,
            INotificationBLL notificationBLL,
            IEmployeeService employeeService,
            ICouncilMemberBLL councilMemberBLL) : base(repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _councilMemberBLL = councilMemberBLL;
            _notificationBLL = notificationBLL;
            _roundRepository = roundRepository;
            _employeeService = employeeService;
            _meetingAttendanceRepository = meetingAttendanceRepository;
        }


        public MeetingDTO GetById(int id)
        {
            try
            {
                MeetingDTO meeting = new MeetingDTO();
                Mapper.Map<Meeting, MeetingDTO>(_repository
                     .Query()
                     .Include(c => c.CouncilType)
                     .Include(c => c.Round)
                     .Include(c => c.MeetingAttendances)
                     .Include(c => c.AgendaItems.Select(d => d.AgendaDetails))
                     .SelectQueryable()
                     .Where(c => c.Id == id).FirstOrDefault(), meeting);
                return meeting;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<MeetingDTO> GetMeetings()
        {
            return _repository
                    .Query()
                    .Include(c => c.CouncilType)
                    .Include(c => c.Round)
                    .SelectQueryable()
                    .ProjectTo<MeetingDTO>();
        }


        public IQueryable<MeetingDTO> GetLatestMeetings()
        {
            try
            {
                return _repository
                     .Query()
                     .Include(c => c.CouncilType)
                     .Include(c => c.Round)
                     .Include(c => c.AgendaItems.Select(d => d.AgendaDetails))
                     .SelectQueryable()
                     .OrderByDescending(c => c.MeetingDate)
                     .Take(3)
                     .ProjectTo<MeetingDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public IQueryable<MeetingDTO> GetMeetingsbyCouncilandYear(int councilId, int year)
        {
            try
            {
                return _repository
                            .Query()
                            .Include(c => c.CouncilType)
                            .SelectQueryable()
                            .Where(c => c.CouncilType.Id == councilId && c.MeetingDate.Year == year)
                            .OrderByDescending(c => c.MeetingDate)
                            .ProjectTo<MeetingDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public int InsertMeeting(MeetingDTO objDTO, string userId)
        {
            Meeting meetingObject = new Meeting();
            objDTO.MeetingIndexNumber = _repository.Query().SelectQueryable().Max(c => c.MeetingIndexNumber) + 1;
            objDTO.MeetingNumber = _repository.Query().SelectQueryable().Where(c => c.MeetingDate.Year == objDTO.MeetingDate.Year).Any() ? _repository.Query().SelectQueryable().Where(c => c.MeetingDate.Year == objDTO.MeetingDate.Year).Max(c => c.MeetingNumber) + 1 : 1;
            try
            {
                _unitOfWork.BeginTransaction();
                objDTO.RoundId = _roundRepository.Query().SelectQueryable().Where(c => c.CouncilTypeId == objDTO.CouncilTypeId && c.IsCurrent).FirstOrDefault().Id;
                Mapper.Map<MeetingDTO, Meeting>(objDTO, meetingObject);
                //operationObject.RefernceNo = GenerateRefernceNo(operationObject);
                objDTO.MeetingAttendances.ToList().ForEach(c =>
                {
                    MeetingAttendance externalattendence = new MeetingAttendance();
                    Mapper.Map<MeetingAttendanceDTO, MeetingAttendance>(c, externalattendence);
                    externalattendence.MemberType = (int)EnumMemberType.External;
                    meetingObject.MeetingAttendances.Add(externalattendence);
                });
                //meetingObject.MeetingAttendances.ToList().ForEach(c => c.MemberType = (int)EnumMemberType.External);
                objDTO.SelectedCouncilMembers.ToList().ForEach(c =>
                {
                    var councilMember = _councilMemberBLL.GetById(c);
                    meetingObject.MeetingAttendances.Add(new MeetingAttendance()
                    {
                        CouncilMemberId = councilMember.Id,
                        Name = councilMember.Name,
                        MemberType = (int)EnumMemberType.CouncilMember,
                        Email = councilMember.Email,
                    });

                });
                objDTO.SelectedEmployees.ToList().ForEach(c =>
                {
                    var emp = _employeeService.GetById(c.Id);
                    meetingObject.MeetingAttendances.Add(new MeetingAttendance()
                    {
                        Name = c.Name,
                        EmployeId = c.Id.ToString(),
                        DepartmentId = c.DepartmentId,
                        DepartmentName = c.DepartmentName,
                        MemberType = (int)EnumMemberType.Employee,
                        Email = emp.Email,

                    });

                });
                base.Insert(meetingObject);
                if (_unitOfWork.SaveChanges() > 0)
                {
                    objDTO.AgendaItems.Where(c => c.AttachementName != "").ToList().ForEach(c =>
                      {
                          meetingObject
                          .AgendaItems
                          .Where(d => d.AgendaNumber == c.AgendaNumber).FirstOrDefault()
                          .AttachementName = CopyAttachment(
                              userId,
                              c.AttachementName,
                              meetingObject.Id,
                              meetingObject.AgendaItems.Where(d => d.AgendaNumber == c.AgendaNumber).FirstOrDefault().Id
                              , null);
                          var adendaItemId = meetingObject.AgendaItems.Where(item => item.AgendaNumber == c.AgendaNumber).FirstOrDefault().Id;
                          c.AgendaDetails.ToList().ForEach(d =>
                          {
                              meetingObject.AgendaItems.Where(f => f.Id == adendaItemId).SelectMany(y => y.AgendaDetails).Where(det => det.TreeNumber == d.TreeNumber).FirstOrDefault()
                              .AttachementName = CopyAttachment(
                                  userId,
                                  d.AttachementName,
                                  meetingObject.Id,
                                  adendaItemId,
                                  meetingObject.AgendaItems.Where(f => f.Id == adendaItemId).SelectMany(y => y.AgendaDetails).Where(det => det.TreeNumber == d.TreeNumber).FirstOrDefault().Id);
                          });
                          _unitOfWork.SaveChanges();
                      });

                }
                _unitOfWork.Commit();
                return meetingObject.Id;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        public int UpdateMeeting(MeetingDTO objDTO, string userId)
        {
            Meeting meetingObject = new Meeting();
            objDTO.MeetingNumber = _repository.Query().SelectQueryable().Where(c => c.MeetingDate.Year == objDTO.MeetingDate.Year && c.Id != objDTO.Id).Any() ? _repository.Query().SelectQueryable().Where(c => c.MeetingDate.Year == objDTO.MeetingDate.Year && c.Id != objDTO.Id).Max(c => c.MeetingNumber) + 1 : 1;

            try
            {
                _unitOfWork.BeginTransaction();
                meetingObject = _repository
                       .Query()
                       .SelectQueryable().Where(c => c.Id == objDTO.Id).FirstOrDefault();

                objDTO.RoundId = _roundRepository.Query().SelectQueryable().Where(c => c.CouncilTypeId == objDTO.CouncilTypeId && c.IsCurrent).FirstOrDefault().Id;

                var meetingCounsilmembers = _meetingAttendanceRepository.Query().SelectQueryable().Where(c => c.MeetingId == meetingObject.Id && c.MemberType == (int)EnumMemberType.CouncilMember).Select(c => new { key = c.Id, value = c.CouncilMemberId.Value }).ToList();
                foreach (var member in meetingCounsilmembers)
                    if (!objDTO.SelectedCouncilMembers.ToList().Contains(member.value))
                        _meetingAttendanceRepository.Delete(member.key);

                _unitOfWork.SaveChanges();
                var meetingEmployeeMembers = _meetingAttendanceRepository.Query().SelectQueryable().Where(c => c.MeetingId == meetingObject.Id && c.MemberType == (int)EnumMemberType.Employee).Select(c => new { key = c.Id, value = c.EmployeId }).ToList();
                foreach (var member in meetingEmployeeMembers)
                    if (!objDTO.SelectedEmployees.Select(c => c.Id).ToList().Contains(Convert.ToInt32(member.value)))
                        _meetingAttendanceRepository.Delete(member.key);
                var ExternalMembers = _meetingAttendanceRepository.Query().SelectQueryable().Where(c => c.MeetingId == meetingObject.Id && c.MemberType == (int)EnumMemberType.External).Select(c => c.Id).ToList();
                foreach (var member in ExternalMembers)
                    if (!objDTO.MeetingAttendances.Select(c => c.Id).ToList().Contains(member))
                        _meetingAttendanceRepository.Delete(member);
                _unitOfWork.SaveChanges();

                objDTO.MeetingAttendances.ToList().ForEach(c =>
                {

                    var temp = new MeetingAttendance();
                    Mapper.Map<MeetingAttendanceDTO, MeetingAttendance>(c, temp);
                    temp.MeetingId = meetingObject.Id;
                    temp.MemberType = (int)EnumMemberType.External;
                    if (c.Id != 0)
                    {
                        //_meetingAttendanceRepository.Update(temp);
                    }
                    else
                    {
                        _meetingAttendanceRepository.Insert(temp);
                    }

                });
                _unitOfWork.SaveChanges();
                objDTO.SelectedCouncilMembers.ToList().ForEach(c =>
                {
                    if (!meetingCounsilmembers.Select(cm => cm.value).Contains(c))
                    {
                        var councilMember = _councilMemberBLL.GetById(c);
                        meetingObject.MeetingAttendances.Add(new MeetingAttendance()
                        {
                            CouncilMemberId = councilMember.Id,
                            Name = councilMember.Name,
                            MemberType = (int)EnumMemberType.CouncilMember

                        });
                    }

                });
                objDTO.SelectedEmployees.ToList().ForEach(c =>
                {
                    if (!meetingEmployeeMembers.Select(emp => Convert.ToInt32(emp.value)).Contains(c.Id))
                    {
                        meetingObject.MeetingAttendances.Add(new MeetingAttendance()
                        {
                            Name = c.Name,
                            EmployeId = c.Id.ToString(),
                            DepartmentId = c.DepartmentId,
                            DepartmentName = c.DepartmentName,
                            MemberType = (int)EnumMemberType.Employee

                        });
                    }
                });
                _unitOfWork.SaveChanges();
                Mapper.Map<MeetingDTO, Meeting>(objDTO, meetingObject);
                meetingObject.TrackingState = TrackableEntities.TrackingState.Modified;

                foreach (var agendaItem in meetingObject.AgendaItems)
                {
                    if (agendaItem.Id == 0)
                        agendaItem.TrackingState = TrackableEntities.TrackingState.Added;
                    else
                        agendaItem.TrackingState = TrackableEntities.TrackingState.Modified;
                    if (agendaItem.AgendaDetails.Any())
                        foreach (var agendaDetail in agendaItem.AgendaDetails)
                        {
                            if (agendaDetail.Id == 0)
                                agendaDetail.TrackingState = TrackableEntities.TrackingState.Added;
                            else
                                agendaDetail.TrackingState = TrackableEntities.TrackingState.Modified;
                        }
                }
                meetingObject.MeetingAttendances.ToList().ForEach(c => c.TrackingState = TrackableEntities.TrackingState.Modified);
                meetingObject.Round = _roundRepository.Query().SelectQueryable().Where(c => c.CouncilTypeId == objDTO.CouncilTypeId && c.IsCurrent).FirstOrDefault();
                base.InsertOrUpdateGraph(meetingObject);
                if (_unitOfWork.SaveChanges() > 0)
                {
                    objDTO.AgendaItems.Where(c => c.AttachementName != "").ToList().ForEach(c =>
                    {
                        var agendanewfile = CopyAttachment(
                            userId,
                            c.AttachementName,
                            meetingObject.Id,
                            meetingObject.AgendaItems.Where(d => d.AgendaNumber == c.AgendaNumber).FirstOrDefault().Id
                            , null);
                        if (!String.IsNullOrEmpty(agendanewfile))
                            meetingObject
                            .AgendaItems
                            .Where(d => d.AgendaNumber == c.AgendaNumber).FirstOrDefault()
                            .AttachementName = agendanewfile;
                        var adendaItemId = meetingObject.AgendaItems.Where(item => item.AgendaNumber == c.AgendaNumber).FirstOrDefault().Id;
                        c.AgendaDetails.ToList().ForEach(d =>
                        {
                            var returnedVal = CopyAttachment(
                                userId,
                                d.AttachementName,
                                meetingObject.Id,
                                adendaItemId,
                                meetingObject.AgendaItems.Where(f => f.Id == adendaItemId).SelectMany(y => y.AgendaDetails).Where(det => det.TreeNumber == d.TreeNumber).FirstOrDefault().Id);
                            if (!String.IsNullOrEmpty(returnedVal))
                                meetingObject.AgendaItems.Where(f => f.Id == adendaItemId).SelectMany(y => y.AgendaDetails).Where(det => det.TreeNumber == d.TreeNumber).FirstOrDefault()
                                .AttachementName = returnedVal;
                        });
                        _unitOfWork.SaveChanges();
                    });

                }
                _unitOfWork.Commit();
                return meetingObject.Id;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private string CopyAttachment(string userId, string tempFileName, int meetingId, int agendaItemId, int? agendaDetailId)
        {
            string newFilePath = "";
            string returnedValue = "";
            if (!Directory.Exists(ConfigurationManager.AppSettings["MeetingPath"]))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["MeetingPath"]);
            if (!Directory.Exists(ConfigurationManager.AppSettings["MeetingPath"] + "//" + meetingId))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["MeetingPath"] + "//" + meetingId);
            if (!Directory.Exists(ConfigurationManager.AppSettings["MeetingPath"] + "//" + meetingId + "//" + agendaItemId))
                Directory.CreateDirectory(ConfigurationManager.AppSettings["MeetingPath"] + "//" + meetingId + "//" + agendaItemId);
            returnedValue = meetingId + "//" + agendaItemId + "//";
            newFilePath = ConfigurationManager.AppSettings["MeetingPath"] + "//" + returnedValue;

            if (agendaDetailId.HasValue)
            {
                if (!Directory.Exists(ConfigurationManager.AppSettings["MeetingPath"] + "//" + meetingId + "//" + agendaItemId + "//" + agendaDetailId.Value))
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["MeetingPath"] + "//" + meetingId + "//" + agendaItemId + "//" + agendaDetailId.Value);
                returnedValue = meetingId + "//" + agendaItemId + "//" + agendaDetailId.Value + "//";
                newFilePath = ConfigurationManager.AppSettings["MeetingPath"] + "//" + returnedValue;
            }

            string fileToCopy = String.Format("{0}//{1}//{2}", ConfigurationManager.AppSettings["TempMeetingPath"], userId, tempFileName.Replace("\"", ""));
            if (File.Exists(fileToCopy))
            {
                foreach (string file in Directory.GetFiles(newFilePath))
                {
                    File.Delete(file);
                }
                File.Copy(fileToCopy, newFilePath + Path.GetFileName(fileToCopy));
                File.Delete(fileToCopy);
                //Directory.Delete(Path.GetDirectoryName(fileToCopy));
                return returnedValue + Path.GetFileName(fileToCopy);

            }
            else
                return "";
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

        public bool MergeAllFiles(int id)
        {
            try
            {
                var returnedValue = false;
                List<string> listOfFiles = new List<string>();
                var meeting = _repository
                         .Query()
                         .Include(c => c.AgendaItems.Select(d => d.AgendaDetails))
                         .SelectQueryable()
                         .Where(c => c.Id == id).FirstOrDefault();
                string meetingAgendaSource = ConfigurationManager.AppSettings["MeetingPath"] + "//" + meeting.Id + "//";

                meeting.AgendaItems.Where(c => !String.IsNullOrEmpty(c.AttachementName)).ToList()
                    .ForEach(c => listOfFiles.Add(ConfigurationManager.AppSettings["MeetingPath"] + "//" + c.AttachementName));
                meeting.AgendaItems.ToList().ForEach(d => d.AgendaDetails.Where(c => !String.IsNullOrEmpty(c.AttachementName)).ToList()
                    .ForEach(c => listOfFiles.Add(ConfigurationManager.AppSettings["MeetingPath"] + "//" + c.AttachementName)));
                meetingAgendaSource = meetingAgendaSource + string.Format("{0}_{1}.pdf", "Meeting_Agenda", meeting.MeetingNumber);
                if (File.Exists(meetingAgendaSource))
                    File.Delete(meetingAgendaSource);
                if (listOfFiles.Any())
                {
                    returnedValue = PdfMerger.MergeFiles(meetingAgendaSource, listOfFiles);
                    if (returnedValue)
                    {
                        _unitOfWork.BeginTransaction();
                        var meetingObj = _repository.Find(id);
                        meetingObj.MeetingAgendaAttachmnet = string.Format("{0}//{1}_{2}.pdf", meetingObj.Id, "Meeting_Agenda", meeting.MeetingNumber);
                        meetingObj.TrackingState = TrackableEntities.TrackingState.Modified;
                        _repository.Update(meetingObj);
                        _unitOfWork.SaveChanges();
                        _unitOfWork.Commit();
                    }
                }
                return returnedValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool SendMeetingRequest(int id)
        {
            try
            {
                var meeting = _repository.Query().SelectQueryable()
                    .Include(c => c.MeetingAttendances)
                    .Where(c => c.Id == id).FirstOrDefault();

                List<NotificationDTO> notifications = new List<NotificationDTO>();
                meeting.MeetingAttendances.ToList().ForEach(c =>
                {
                    if (!String.IsNullOrEmpty(c.Email))
                    {
                        var notication = new NotificationDTO()
                        {
                            Body = "دعوه لحضور إجتماع",
                            DueDate = meeting.MeetingDate,
                            EmployeeId = 0,
                            EmployeeMail = c.Email,
                            IsOpen = false,
                            NotificationType = (int)EnumNotificationType.MeetingRequest,
                            status = (int)EnumMailStatus.Send,
                            Title = "دعوه لحضور إجتماع",
                            MeetingId = meeting.Id,
                            UserId = ""
                        };
                        notication.Id = _notificationBLL.AddNotification(notication);
                        notifications.Add(notication);
                    }
                }
                );
                foreach (NotificationDTO notification in notifications)
                    Mail.SendEmail(_notificationBLL.GetById(notification.Id), "", notification.EmployeeMail);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
