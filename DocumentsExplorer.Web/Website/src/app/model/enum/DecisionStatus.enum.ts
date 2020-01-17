export enum DecisionStatusEnum {
  NotStrated = 0,
  UnderExecution = 1,
  Executed = 2
}

export const DecisionStatusEnumArLabel = new Map<number, string>([
  [DecisionStatusEnum.NotStrated, 'لم يبدأ '],
  [DecisionStatusEnum.UnderExecution, 'تحت التنفيذ'],
  [DecisionStatusEnum.Executed, 'تم التنفيذ']
]);


export const DecisionStatusEnumEnLabel = new Map<number, string>([
  [DecisionStatusEnum.NotStrated, 'Not started'],
  [DecisionStatusEnum.UnderExecution, 'Under Execution'],
  [DecisionStatusEnum.Executed, 'Finished']
]);
