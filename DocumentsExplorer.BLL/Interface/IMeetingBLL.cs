using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL.Interface
{
    public interface IMeetingBLL : IService<Meeting>
    {
        IQueryable<MeetingDTO> GetMeetings();
        IQueryable<MeetingDTO> GetLatestMeetings();
        IQueryable<MeetingDTO> GetMeetingsbyCouncilandYear(int councilId, int year);
        int InsertMeeting(MeetingDTO obj,string userId);
        int UpdateMeeting(MeetingDTO obj, string userId);
        MeetingDTO GetById(int id);
        int Delete(int id);
        bool MergeAllFiles(int id);
        bool SendMeetingRequest(int id);
        

    }
}
