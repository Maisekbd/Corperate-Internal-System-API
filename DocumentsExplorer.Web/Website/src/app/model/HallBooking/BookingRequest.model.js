"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var BookingRequest = /** @class */ (function () {
    function BookingRequest(id, name, loc, title, description, meetingTypeId, meetingTypeName, hallId, hallName, startDate, endDate, location, isEditable, isMine, sudanHallId) {
        this.Id = id;
        this.text = name;
        this.Location = loc;
        this.Title = title;
        this.Description = description;
        this.MeetingTypeId = meetingTypeId;
        this.HallId = hallId;
        this.HallName = hallName;
        this.StartDate = new Date(startDate.toLocaleString("en-US", { timeZone: Intl.DateTimeFormat().resolvedOptions().timeZone }));
        this.EndDate = new Date(endDate.toLocaleString("en-US", { timeZone: Intl.DateTimeFormat().resolvedOptions().timeZone }));
        //this.StartDate = startDate;
        //this.EndDate = endDate;
        this.Location = location;
        this.IsEditable = isEditable;
        this.IsMine = isMine;
        this.SudanHallId = sudanHallId;
    }
    BookingRequest.deserialize = function (res) {
        return new BookingRequest(res.Id, res.text, res.Location, res.Title, res.Description, res.MeetingTypeId, res.meetingTypeName, res.HallId, res.Hall, res.StartDate, res.EndDate, res.Location, res.IsEditable, res.IsMine, res.SudanHallId);
    };
    BookingRequest.getDefault = function () {
        return new BookingRequest(0, "", 0, "", "", null, "", null, "", new Date(), new Date(), 0, false, true, 0);
    };
    return BookingRequest;
}());
exports.BookingRequest = BookingRequest;
//# sourceMappingURL=BookingRequest.model.js.map