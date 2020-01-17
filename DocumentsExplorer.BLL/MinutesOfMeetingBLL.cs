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
    public class MinutesOfMeetingBLL : Service<Meeting>, IMinutesOfMeetingBLL
    {
        private readonly IRepositoryAsync<Meeting> _repository;
        private readonly IRepositoryAsync<MeetingAttendance> _meetingAttendanceRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public MinutesOfMeetingBLL(IUnitOfWorkAsync unitOfWork,
            IRepositoryAsync<Meeting> repository,
            IRepositoryAsync<MeetingAttendance> meetingAttendanceRepository) : base(repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _meetingAttendanceRepository = meetingAttendanceRepository;
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

        public MinutesOfMeetingDTO GetById(int minutesOfMeetingId)
        {
            try
            {
                MinutesOfMeetingDTO minutesOfMeeting = new MinutesOfMeetingDTO();
                Mapper.Map<Meeting, MinutesOfMeetingDTO>(_repository
                     .Query()
                     .Include(c=>c.MeetingAttendances)
                     .SelectQueryable()
                     .Where(c => c.Id == minutesOfMeetingId).FirstOrDefault(), minutesOfMeeting);
                return minutesOfMeeting;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<MinutesOfMeetingDTO> GetMinutesOfMeetings()
        {
            try
            {
                return _repository
                    .Query()
                    .SelectQueryable()
                    .ProjectTo<MinutesOfMeetingDTO>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Save(MinutesOfMeetingDTO obj, string currentUserId)
        {
            Meeting minutesOfMeeting;
            try
            {
                _unitOfWork.BeginTransaction();

                if (obj.Id == 0)
                {
                    minutesOfMeeting = new Meeting();
                    Mapper.Map<MinutesOfMeetingDTO, Meeting>(obj, minutesOfMeeting);
                    minutesOfMeeting.MeetingAttendances = new List<MeetingAttendance>();
                    if (obj.SelectedAttendances != null && obj.SelectedAttendances.Any())
                        foreach (int attendent in obj.SelectedAttendances)
                            minutesOfMeeting.MeetingAttendances.Add(new MeetingAttendance() { CouncilMemberId = attendent, IsAttendant = true, TrackingState = TrackableEntities.TrackingState.Added });

                    if (obj.SelectedAbsents != null &&  obj.SelectedAbsents.Any())
                        foreach (int absent in obj.SelectedAbsents)
                            minutesOfMeeting.MeetingAttendances.Add(new MeetingAttendance() { CouncilMemberId = absent, IsAttendant = false, TrackingState = TrackableEntities.TrackingState.Added });

                    //minutesOfMeeting.CreateDate = DateTime.Now;
                    minutesOfMeeting.CreatedBy = currentUserId;
                    base.Insert(minutesOfMeeting);
                }
                else
                {
                    minutesOfMeeting = new Meeting();
                    minutesOfMeeting = Mapper.Map<MinutesOfMeetingDTO, Meeting>(obj);
                    minutesOfMeeting.TrackingState = TrackableEntities.TrackingState.Modified;
                    base.Update(minutesOfMeeting);
                    var attendenceLst = _meetingAttendanceRepository.Query().Select().Where(c => c.MeetingId == obj.Id);
                    foreach (var s in attendenceLst)
                        _meetingAttendanceRepository.Delete(s.Id);
                    //InsertOrUpdateGraph(minutesOfMeeting);
                    _unitOfWork.SaveChanges();
                    if (obj.SelectedAttendances.Count > 0)
                        foreach (int attendent in obj.SelectedAttendances)
                            minutesOfMeeting.MeetingAttendances.Add(new MeetingAttendance() { CouncilMemberId = attendent, IsAttendant = true, TrackingState = TrackableEntities.TrackingState.Added });

                    if (obj.SelectedAbsents.Count > 0)
                        foreach (int absent in obj.SelectedAbsents)
                            minutesOfMeeting.MeetingAttendances.Add(new MeetingAttendance() { CouncilMemberId = absent, IsAttendant = false, TrackingState = TrackableEntities.TrackingState.Added });

                    minutesOfMeeting.LastUpdateDate = DateTime.Now;
                    minutesOfMeeting.LastUpdateBy = currentUserId;
                    base.Update(minutesOfMeeting);
                }
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return minutesOfMeeting.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}