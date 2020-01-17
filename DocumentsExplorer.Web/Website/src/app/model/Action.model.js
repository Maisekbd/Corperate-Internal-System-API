"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Action = /** @class */ (function () {
    function Action(treeNum, desc, responsibleId, responsibleName, actionType, agendaItemId, agendaItem) {
        this.TreeNumber = treeNum;
        this.Description = desc;
        this.ResponsibleId = responsibleId;
        this.ResponsibleName = responsibleName;
        this.ActionType = actionType;
        this.AgendaItemId = agendaItemId;
        this.AgendaItem = agendaItem;
    }
    Action.deserialize = function (actionRes) {
        return new Action(actionRes.TreeNumber, actionRes.Description, actionRes.ResponsibleId, actionRes.ResponsibleName, actionRes.ActionType, actionRes.AgendaItemId, actionRes.AgendaItem);
    };
    Action.getActionDefault = function () {
        return new Action("", "", "", "", null, null, null);
    };
    return Action;
}());
exports.Action = Action;
//# sourceMappingURL=Action.model.js.map