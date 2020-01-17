"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var CouncilMember = /** @class */ (function () {
    function CouncilMember(id, name, countId, country, position, joinDate, finishDate, memberRole, photourl, isActive, createDate, councilTypeId, council) {
        this.Id = id;
        this.Name = name;
        this.CountryId = countId;
        this.Country = country;
        this.Position = position;
        this.JoiningDate = joinDate;
        this.FinishDate = finishDate;
        this.memberRole = memberRole;
        this.PhotoPath = photourl;
        this.IsActive = isActive;
        this.CreateDate = createDate;
        this.CouncilTypeId = councilTypeId;
        this.CouncilType = council;
    }
    CouncilMember.deserialize = function (councilMemeber) {
        return new CouncilMember(councilMemeber.Id, councilMemeber.Name, councilMemeber.CountryId, councilMemeber.Country, councilMemeber.Position, councilMemeber.JoiningDate, councilMemeber.FinishDate, councilMemeber.memberRole, councilMemeber.PhotoPath, councilMemeber.IsActive, councilMemeber.CreateDate, councilMemeber.CouncilTypeId, councilMemeber.CouncilType);
    };
    return CouncilMember;
}());
exports.CouncilMember = CouncilMember;
//# sourceMappingURL=CouncilMember.model.js.map