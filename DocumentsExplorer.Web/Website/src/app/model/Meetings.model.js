"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Meeting = /** @class */ (function () {
    // public meetingDateStr: Date;
    function Meeting(id, meetingNum, date, time, location, prepById, prepByName, meetingIndexNum, roundId, round, councilTypeId, councilType, agenda, Attendances, attachments, generatedMeetingNumber, meetingAgendaAttachmnet) {
        this.Id = id;
        this.MeetingNumber = meetingNum; //Meeting Number in year
        this.MeetingDate = date;
        this.MeetingTime = time;
        this.Location = location;
        this.PreparedById = prepById;
        this.PreparedByName = prepByName;
        this.MeetingIndexNumber = meetingIndexNum; //meeting number from AAAId established Year
        this.RoundId = roundId;
        this.Round = round;
        this.CouncilTypeId = councilTypeId;
        this.CouncilType = councilType;
        this.AgendaItems = agenda;
        this.MeetingAttendances = Attendances;
        this.Attachments = attachments;
        this.GeneratedMeetingNumber = generatedMeetingNumber;
        this.MeetingAgendaAttachmnet = meetingAgendaAttachmnet;
    }
    Meeting.deserialize = function (meetingResponse) {
        return new Meeting(meetingResponse.Id, meetingResponse.MeetingNumber, meetingResponse.MeetingDate, meetingResponse.MeetingTime, meetingResponse.Location, meetingResponse.PreparedById, meetingResponse.PreparedByName, meetingResponse.MeetingIndexNumber, meetingResponse.RoundId, meetingResponse.Round, meetingResponse.CouncilTypeId, meetingResponse.CouncilType, meetingResponse.AgendaItems, meetingResponse.MeetingAttendances, meetingResponse.Attachments, meetingResponse.GeneratedMeetingNumber, meetingResponse.MeetingAgendaAttachmnet);
    };
    Meeting.getDefault = function () {
        return new Meeting(0, 0, new Date(), new Date(), "", "", "", 0, 0, null, 0, null, [], [], [], this.generateTempMeetingNumber(), "");
    };
    Meeting.generateTempMeetingNumber = function () {
        var currentdate = new Date();
        return currentdate.getFullYear() + "_" + currentdate.getMonth() + "_" + currentdate.getDay() + "_" + currentdate.getHours() + "_" + currentdate.getMinutes() + "_" + currentdate.getSeconds();
    };
    return Meeting;
}());
exports.Meeting = Meeting;
//# sourceMappingURL=Meetings.model.js.map