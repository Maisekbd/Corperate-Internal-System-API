"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var CouncilType = /** @class */ (function () {
    function CouncilType(id, desc, councilMembers) {
        this.Id = id;
        this.Description = desc;
        this.CouncilMembers = councilMembers;
    }
    CouncilType.deserialize = function (typeRes) {
        return new CouncilType(typeRes.Id, typeRes.Description, typeRes.CouncilMembers);
    };
    return CouncilType;
}());
exports.CouncilType = CouncilType;
//# sourceMappingURL=CouncilType.model.js.map