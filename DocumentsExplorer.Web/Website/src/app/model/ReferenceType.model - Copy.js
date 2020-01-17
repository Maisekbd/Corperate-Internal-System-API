"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ReferenceItem = /** @class */ (function () {
    function ReferenceItem(id, name, desc, path, fileEx, refTypeId, refType, decisionID, decision, refDecisionId, refDecision) {
        this.Id = id;
        this.Name = name;
        this.Description = desc;
        this.Path = path;
        this.FileExtension = fileEx;
        this.ReferenceTypeId = refTypeId;
        this.ReferenceType = refType;
        this.DecisionId = decisionID;
        this.Decision = decision;
        this.ReferenceTypeId = refDecisionId;
        this.ReferenceDecision = refDecision;
    }
    ReferenceItem.deserialize = function (detailRes) {
        return new ReferenceItem(detailRes.Id, detailRes.Name, detailRes.Description, detailRes.Path, detailRes.FileExtension, detailRes.ReferenceTypeId, detailRes.ReferenceType, detailRes.DecisionId, detailRes.Decision, detailRes.ReferenceDecisionId, detailRes.ReferenceDecision);
    };
    ReferenceItem.getDefault = function () {
        return new ReferenceItem(null, "", '', '', '', 0, null, 0, null, 0, null);
    };
    return ReferenceItem;
}());
exports.ReferenceItem = ReferenceItem;
//# sourceMappingURL=ReferenceType.model - Copy.js.map