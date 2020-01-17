"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AgendaDetails = /** @class */ (function () {
    function AgendaDetails(id, treeNum, desc, agendaItemId, agendaItem, attachementName) {
        this.Id = id;
        this.TreeNumber = treeNum;
        this.Description = desc;
        this.AgendaItemId = agendaItemId;
        this.AgendaItem = agendaItem;
        this.AttachementName = attachementName;
    }
    AgendaDetails.deserialize = function (detailRes) {
        return new AgendaDetails(detailRes.Id, detailRes.TreeNumber, detailRes.Description, detailRes.AgendaItemId, detailRes.AgendaItem, detailRes.AgendaItem);
    };
    AgendaDetails.getAgendaDetailsDefault = function () {
        return new AgendaDetails(0, "", "", 0, null, "");
    };
    return AgendaDetails;
}());
exports.AgendaDetails = AgendaDetails;
//# sourceMappingURL=AgendaDetails.model.js.map