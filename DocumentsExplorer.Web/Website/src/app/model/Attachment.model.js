"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Attachment = /** @class */ (function () {
    function Attachment(name, path, fileEx, meetingId, meeting) {
        this.Name = name;
        this.Path = path;
        this.FileExtension = fileEx;
        this.MeetingId = meetingId;
        this.Meeting = meeting;
    }
    Attachment.deserialize = function (attachRes) {
        return new Attachment(attachRes.Name, attachRes.Path, attachRes.FileExtension, attachRes.MeetingId, attachRes.Meeting);
    };
    return Attachment;
}());
exports.Attachment = Attachment;
//# sourceMappingURL=Attachment.model.js.map