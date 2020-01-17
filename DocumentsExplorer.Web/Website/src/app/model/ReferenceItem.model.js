"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ReferenceItem = /** @class */ (function () {
    function ReferenceItem(id, refereceItemNo, name, desc, path, fileEx, refTypeId, refType, decisionID, decision, refDecisionId, refDecision, refDecisionPath) {
        this.Id = id;
        this.RefereceItemNo = refereceItemNo;
        this.Name = name;
        this.Description = desc;
        this.Path = path;
        this.FileExtension = fileEx;
        this.ReferenceTypeId = refTypeId;
        this.ReferenceType = refType;
        this.DecisionId = decisionID;
        this.Decision = decision;
        this.ReferenceDecisionId = refDecisionId;
        this.ReferenceDecision = refDecision;
        this.RefDecisionPath = refDecisionPath;
    }
    ReferenceItem.deserialize = function (detailRes) {
        return new ReferenceItem(detailRes.Id, detailRes.RefereceItemNo, detailRes.Name, detailRes.Description, detailRes.Path, detailRes.FileExtension, detailRes.ReferenceTypeId, detailRes.ReferenceType, detailRes.DecisionId, detailRes.Decision, detailRes.ReferenceDecisionId, detailRes.ReferenceDecision, detailRes.RefDecisionPath);
    };
    ReferenceItem.getDefault = function () {
        return new ReferenceItem(0, 0, "", "", "", "", 0, null, 0, null, 0, null, "");
    };
    return ReferenceItem;
}());
exports.ReferenceItem = ReferenceItem;
//# sourceMappingURL=ReferenceItem.model.js.map