using DocumentsExplorer.Model.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL.Interface
{
    public interface IAttachmentBLL : IService<MeetingAttachment>
    {
        IList<MeetingAttachment> GetAttachmens(int minutesOfMeetingId);
        int Save(MeetingAttachment attachment, string currentUserId);
        int Delete(int id);

        string SaveTempAgendaAttachment(string userId, string meetingNO, string agendaTreeNumber, string FileName, byte[] file);
        string SaveTempAttachmentWithParameter(string userId, string itemNumber, int attachmentType, string FileName, byte[] file);
        string SaveTempDecision(string userId, string FileName, byte[] file);
    }

}