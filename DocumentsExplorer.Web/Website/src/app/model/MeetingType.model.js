"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var MeetingType = /** @class */ (function () {
    function MeetingType(id, name) {
        this.Id = id;
        this.Name = name;
    }
    MeetingType.deserialize = function (typeRes) {
        return new MeetingType(typeRes.Id, typeRes.Name);
    };
    return MeetingType;
}());
exports.MeetingType = MeetingType;
//# sourceMappingURL=MeetingType.model.js.map