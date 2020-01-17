"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ActionTypeEnum;
(function (ActionTypeEnum) {
    ActionTypeEnum[ActionTypeEnum["NoValue"] = 0] = "NoValue";
    ActionTypeEnum[ActionTypeEnum["Advise"] = 1] = "Advise";
    ActionTypeEnum[ActionTypeEnum["Execute"] = 2] = "Execute";
    ActionTypeEnum[ActionTypeEnum["Save"] = 3] = "Save";
    ActionTypeEnum[ActionTypeEnum["Inform"] = 4] = "Inform";
    ActionTypeEnum[ActionTypeEnum["Review"] = 5] = "Review";
    ActionTypeEnum[ActionTypeEnum["NeedContact"] = 6] = "NeedContact";
})(ActionTypeEnum = exports.ActionTypeEnum || (exports.ActionTypeEnum = {}));
exports.ActionTypeEnumArLabel = new Map([
    [ActionTypeEnum.NoValue, "اختر.."],
    [ActionTypeEnum.Advise, 'للاستشارة'],
    [ActionTypeEnum.Execute, 'للتنفيذ'],
    [ActionTypeEnum.Save, 'حفظ'],
    [ActionTypeEnum.Inform, 'للعلم'],
    [ActionTypeEnum.Review, 'للمراجعة'],
    [ActionTypeEnum.NeedContact, 'تحتاج تواصل'],
]);
exports.ActionTypeEnumEnLabel = new Map([
    [ActionTypeEnum.NoValue, "NoValue.."],
    [ActionTypeEnum.Advise, 'Advise'],
    [ActionTypeEnum.Execute, 'Execute'],
    [ActionTypeEnum.Save, 'Save'],
    [ActionTypeEnum.Inform, 'Inform'],
    [ActionTypeEnum.Review, 'Review'],
    [ActionTypeEnum.NeedContact, 'NeedContact'],
]);
//# sourceMappingURL=ActionType.enum.js.map