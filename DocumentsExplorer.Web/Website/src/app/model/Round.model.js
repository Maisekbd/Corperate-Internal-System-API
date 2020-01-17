"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Round = /** @class */ (function () {
    function Round(id, roundNum, fromDate, toDate, createDate, counTypeId, counType) {
        this.Id = id;
        this.RoundNumber = roundNum;
        this.FromDate = fromDate;
        this.ToDate = toDate;
        this.CreateDate = createDate;
        this.CouncilTypeId = counTypeId;
        this.CouncilType = counType;
    }
    Round.deserialize = function (roundRes) {
        return new Round(roundRes.Id, roundRes.RoundNumber, roundRes.FromDate, roundRes.ToDate, roundRes.CreateDate, roundRes.CouncilTypeId, roundRes.CouncilType);
    };
    return Round;
}());
exports.Round = Round;
//# sourceMappingURL=Round.model.js.map