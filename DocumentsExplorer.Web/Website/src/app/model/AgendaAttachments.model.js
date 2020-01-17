"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AgendaAttachment = /** @class */ (function () {
    function AgendaAttachment(objId, objType, desc, path, createdByName) {
        this.ObjectId = objId;
        this.ObjectType = objType;
        this.Description = desc;
        this.AttachmentPath = path;
        this.CreatedByName = createdByName;
    }
    AgendaAttachment.deserialize = function (attachRes) {
        return new AgendaAttachment(attachRes.ObjectId, attachRes.ObjectType, attachRes.Description, attachRes.AttachmentPath, attachRes.CreatedByName);
    };
    AgendaAttachment.getAgendaAttachmentDefault = function () {
        return new AgendaAttachment(0, "", "", "", "");
    };
    return AgendaAttachment;
}());
exports.AgendaAttachment = AgendaAttachment;
//# sourceMappingURL=AgendaAttachments.model.js.map