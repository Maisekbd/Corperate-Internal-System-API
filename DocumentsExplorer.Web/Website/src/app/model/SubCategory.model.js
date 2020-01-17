"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var SubCategory = /** @class */ (function () {
    function SubCategory(id, desc) {
        this.Id = id;
        this.Description = desc;
    }
    SubCategory.deserialize = function (typeRes) {
        return new SubCategory(typeRes.Id, typeRes.Description);
    };
    return SubCategory;
}());
exports.SubCategory = SubCategory;
//# sourceMappingURL=SubCategory.model.js.map