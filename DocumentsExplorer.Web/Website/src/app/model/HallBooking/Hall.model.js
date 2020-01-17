"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Hall = /** @class */ (function () {
    function Hall(id, name, loc) {
        this.Id = id;
        this.Name = name;
        this.Location = loc;
    }
    Hall.deserialize = function (res) {
        return new Hall(res.Id, res.Name, res.Location);
    };
    Hall.getDefault = function () {
        return new Hall(0, "", 0);
    };
    return Hall;
}());
exports.Hall = Hall;
//# sourceMappingURL=Hall.model.js.map