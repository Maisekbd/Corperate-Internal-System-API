"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ReferenceType = /** @class */ (function () {
    function ReferenceType(id, name, isReferenceDecision) {
        this.Id = id;
        this.Name = name;
        this.IsReferenceDecision = isReferenceDecision;
    }
    ReferenceType.deserialize = function (detailRes) {
        return new ReferenceType(detailRes.Id, detailRes.Name, detailRes.IsReferenceDecision);
    };
    ReferenceType.getDefault = function () {
        return new ReferenceType(0, "", false);
    };
    return ReferenceType;
}());
exports.ReferenceType = ReferenceType;
//# sourceMappingURL=ReferenceType.model.js.map