"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AgendaItem = /** @class */ (function () {
    function AgendaItem(id, txt, agendaNumber, presentedBy, conc, meetingNum, meeting, details, rowIndex, attachmentName) {
        this.Id = id;
        this.AgendaText = txt;
        this.AgendaNumber = agendaNumber;
        this.PresentedBy = presentedBy;
        this.Conclusion = conc;
        this.MeetingId = meetingNum;
        this.Meeting = meeting;
        this.AgendaDetails = details;
        this.RowIndex = rowIndex;
        this.AttachementName = attachmentName;
    }
    AgendaItem.deserialize = function (agendaRes) {
        return new AgendaItem(agendaRes.Id, agendaRes.AgendaText, agendaRes.AgendaNumber, agendaRes.PresentedBy, agendaRes.Conclusion, agendaRes.MeetingId, agendaRes.Meeting, agendaRes.AgendaDetails, 0, agendaRes.AttachementName);
    };
    AgendaItem.getAgendaDefault = function () {
        return new AgendaItem(0, null, "1", "", "", null, null, null, 0, "");
    };
    return AgendaItem;
}());
exports.AgendaItem = AgendaItem;
//# sourceMappingURL=AgendaItem.model.js.map