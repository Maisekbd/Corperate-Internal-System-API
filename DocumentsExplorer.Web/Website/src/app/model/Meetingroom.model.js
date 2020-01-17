"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Meetingroom = /** @class */ (function () {
    function Meetingroom(id, name, loc, title, meetingTypeId, hallId) {
        this.Id = id;
        this.text = name;
        this.Location = loc;
        this.Title = title;
        this.MeetingTypeId = meetingTypeId;
        this.HallId = hallId;
    }
    Meetingroom.deserialize = function (res) {
        return new Meetingroom(res.Id, res.text, res.Location, res.title, res.meetingTypeId, res.hallId);
    };
    Meetingroom.getDefault = function () {
        return new Meetingroom(0, "", 0, "", 0, 0);
    };
    return Meetingroom;
}());
exports.Meetingroom = Meetingroom;
//# sourceMappingURL=Meetingroom.model.js.map