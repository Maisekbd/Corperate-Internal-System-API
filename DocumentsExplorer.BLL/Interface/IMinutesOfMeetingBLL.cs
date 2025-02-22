﻿using DocumentsExplorer.DTO;
using DocumentsExplorer.Model.Models;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.BLL.Interface
{
    public interface IMinutesOfMeetingBLL : IService<Meeting>
    {
        IQueryable<MinutesOfMeetingDTO> GetMinutesOfMeetings();
        MinutesOfMeetingDTO GetById(int minutesOfMeetingId);
        int Save(MinutesOfMeetingDTO obj, string currentUserId);
        int Delete(int id);
    }
}

