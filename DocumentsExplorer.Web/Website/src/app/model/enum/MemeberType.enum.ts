export enum MemeberTypeEnum {
  CouncilMemeber=0,
  Employee=1,
  External=2
}

export const MemeberTypeEnumArLabel = new Map<number, string>([
  [MemeberTypeEnum.CouncilMemeber, 'أعضاء المجلس'],
  [MemeberTypeEnum.Employee, 'من الهيئة'],
  [MemeberTypeEnum.External, 'من الشركات ']
]);


export const MemeberTypeEnumEnLabel = new Map<number, string>([
  [MemeberTypeEnum.CouncilMemeber, 'Council'],
  [MemeberTypeEnum.Employee, 'from Institute'],
  [MemeberTypeEnum.External, 'from Companies']
]);
