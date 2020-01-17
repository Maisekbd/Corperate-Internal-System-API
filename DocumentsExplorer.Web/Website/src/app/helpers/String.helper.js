"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.StringFormat = function (str) {
    var args = [];
    for (var _i = 1; _i < arguments.length; _i++) {
        args[_i - 1] = arguments[_i];
    }
    return str.replace(/{(\d+)}/g, function (match, index) { return args[index] || ''; });
};
//# sourceMappingURL=String.helper.js.map