"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var MainCategory = /** @class */ (function () {
    function MainCategory(id, desc) {
        this.Id = id;
        this.Description = desc;
    }
    MainCategory.deserialize = function (typeRes) {
        return new MainCategory(typeRes.Id, typeRes.Description);
    };
    return MainCategory;
}());
exports.MainCategory = MainCategory;
//# sourceMappingURL=MainCategory.model.js.map