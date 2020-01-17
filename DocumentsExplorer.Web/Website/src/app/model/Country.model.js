"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Country = /** @class */ (function () {
    function Country(id, name) {
        this.Id = id;
        this.Name = name;
    }
    Country.deserialize = function (typeRes) {
        return new Country(typeRes.Id, typeRes.Name);
    };
    return Country;
}());
exports.Country = Country;
//# sourceMappingURL=Country.model.js.map