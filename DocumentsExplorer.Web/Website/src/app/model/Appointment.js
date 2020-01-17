"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Appointment = /** @class */ (function () {
    function Appointment(id, name, roomId, room, startDate, endDate) {
        this.Id = id;
        this.Name = name;
        this.MeetingroomId = roomId;
        this.Meetingroom = room;
        this.StartDate = startDate;
        this.EndDate = endDate;
    }
    Appointment.deserialize = function (res) {
        return new Appointment(res.Id, res.Name, res.MeetingroomId, res.Meetingroom, res.StartDate, res.EndDate);
    };
    Appointment.getDefault = function () {
        return new Appointment(0, "", 0, null, null, null);
    };
    return Appointment;
}());
exports.Appointment = Appointment;
//# sourceMappingURL=Appointment.js.map