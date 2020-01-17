"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var DecisionExecution = /** @class */ (function () {
    function DecisionExecution(id, departmentId, departmentName, actionType, decisionStatus, executionNotes, executionDate, decisionId, decision, attach, needAction, attachementName) {
        this.Id = id;
        this.DepartmentId = departmentId;
        this.DepartmentName = departmentName;
        this.ActionType = actionType;
        this.DecisionStatus = decisionStatus;
        this.ExecutionNotes = executionNotes;
        this.ExecutionDate = executionDate;
        this.DecisionId = decisionId;
        this.Decision = decision;
        this.Attachments = attach;
        this.NeedAction = needAction;
        this.AttachementName = attachementName;
    }
    DecisionExecution.deserialize = function (Res) {
        return new DecisionExecution(Res.Id, Res.DepartmentId, Res.DepartmentName, Res.ActionType, Res.DecisionStatus, Res.ExecutionNotes, Res.ExecutionDate, Res.DecisionId, Res.Decision, Res.Attachments, Res.NeedAction, Res.AttachementName);
    };
    DecisionExecution.getDefault = function () {
        return new DecisionExecution(0, 0, "", 0, 0, "", new Date(), 0, null, null, false, "");
    };
    return DecisionExecution;
}());
exports.DecisionExecution = DecisionExecution;
//# sourceMappingURL=DecisionExecution.model.js.map