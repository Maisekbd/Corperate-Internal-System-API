"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ActivitySector = /** @class */ (function () {
    function ActivitySector(id, name, createDate, companies, decisions) {
        this.Id = id;
        this.Name = name;
        this.CreateDate = createDate;
        this.Companies = companies;
        this.Decisions = decisions;
    }
    ActivitySector.deserialize = function (typeRes) {
        return new ActivitySector(typeRes.Id, typeRes.Name, typeRes.CreateDate, typeRes.Companies, typeRes.Decisions);
    };
    ActivitySector.getDefault = function () {
        return new ActivitySector(0, "", null, null, null);
    };
    return ActivitySector;
}());
exports.ActivitySector = ActivitySector;
//# sourceMappingURL=ActivitySector.model.js.map