export enum ActionTypeEnum {
  NoValue=0,
  Advise = 1,
  Execute = 2,
  Save = 3,
  Inform = 4,
  Review = 5,
  NeedContact = 6
}

export const ActionTypeEnumArLabel = new Map<number, string>([
  [ActionTypeEnum.NoValue , "اختر.."],
  [ActionTypeEnum.Advise, 'للاستشارة'],
  [ActionTypeEnum.Execute, 'للتنفيذ'],
  [ActionTypeEnum.Save, 'حفظ'],
  [ActionTypeEnum.Inform, 'للعلم'],
  [ActionTypeEnum.Review, 'للمراجعة'],
  [ActionTypeEnum.NeedContact, 'تحتاج تواصل'],
]);


export const ActionTypeEnumEnLabel = new Map<number, string>([
  [ActionTypeEnum.NoValue, "NoValue.."],
  [ActionTypeEnum.Advise, 'Advise'],
  [ActionTypeEnum.Execute, 'Execute'],
  [ActionTypeEnum.Save, 'Save'],
  [ActionTypeEnum.Inform, 'Inform'],
  [ActionTypeEnum.Review, 'Review'],
  [ActionTypeEnum.NeedContact, 'NeedContact'],
]);
