export enum LocationEnum {
  Dubai=0,
  Sudan = 1,
  Shared = 2,
}

export const LocationEnumArLabel = new Map<number, string>([
  [LocationEnum.Dubai, "دبي"],
  [LocationEnum.Sudan, 'السودان'],
  [LocationEnum.Shared, 'مشترك'],
]);


export const LocationEnumEnLabel = new Map<number, string>([
  [LocationEnum.Dubai, "Dubai"],
  [LocationEnum.Sudan, 'Sudan'],
  [LocationEnum.Shared, 'Shared'],
]);


