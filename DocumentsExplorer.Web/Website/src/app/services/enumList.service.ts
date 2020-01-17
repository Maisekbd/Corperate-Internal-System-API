import { tap, map } from 'rxjs/operators';
import { EnumModel } from '../Model/Enum.model';

import { Injectable } from '@angular/core';
import { ActionTypeEnum, ActionTypeEnumArLabel, ActionTypeEnumEnLabel } from '../model/Enum/ActionType.enum';
import { MemeberTypeEnum, MemeberTypeEnumArLabel, MemeberTypeEnumEnLabel } from '../model/enum/MemeberType.enum';
import { DecisionStatusEnum, DecisionStatusEnumArLabel, DecisionStatusEnumEnLabel } from '../model/enum/DecisionStatus.enum';
import { LocationEnum, LocationEnumArLabel, LocationEnumEnLabel } from '../model/enum/Location.enum';




@Injectable({ providedIn: 'root' })
export class EnumServiceList {
  public ActionTypeEnum: EnumModel[] = [
    new EnumModel(ActionTypeEnum.Advise, ActionTypeEnumArLabel.get(ActionTypeEnum.Advise), ActionTypeEnumEnLabel.get(ActionTypeEnum.Advise)),
    new EnumModel(ActionTypeEnum.Execute, ActionTypeEnumArLabel.get(ActionTypeEnum.Execute), ActionTypeEnumEnLabel.get(ActionTypeEnum.Execute)),
    new EnumModel(ActionTypeEnum.Inform, ActionTypeEnumArLabel.get(ActionTypeEnum.Inform), ActionTypeEnumEnLabel.get(ActionTypeEnum.Inform)),
    new EnumModel(ActionTypeEnum.NeedContact, ActionTypeEnumArLabel.get(ActionTypeEnum.NeedContact), ActionTypeEnumEnLabel.get(ActionTypeEnum.NeedContact)),
    new EnumModel(ActionTypeEnum.Review, ActionTypeEnumArLabel.get(ActionTypeEnum.Review), ActionTypeEnumEnLabel.get(ActionTypeEnum.Review)),
    new EnumModel(ActionTypeEnum.Save, ActionTypeEnumArLabel.get(ActionTypeEnum.Save), ActionTypeEnumEnLabel.get(ActionTypeEnum.Save))
  ]
  public MemeberTypeEnum: EnumModel[] = [
    new EnumModel(MemeberTypeEnum.CouncilMemeber, MemeberTypeEnumArLabel.get(MemeberTypeEnum.CouncilMemeber), MemeberTypeEnumEnLabel.get(MemeberTypeEnum.CouncilMemeber)),
    new EnumModel(MemeberTypeEnum.Employee, MemeberTypeEnumArLabel.get(MemeberTypeEnum.Employee), MemeberTypeEnumEnLabel.get(MemeberTypeEnum.Employee)),
    new EnumModel(MemeberTypeEnum.External, MemeberTypeEnumArLabel.get(MemeberTypeEnum.External), MemeberTypeEnumEnLabel.get(MemeberTypeEnum.External))
  ]


  public DecisionStatusEnumLst: EnumModel[] = [
    new EnumModel(DecisionStatusEnum.NotStrated, DecisionStatusEnumArLabel.get(DecisionStatusEnum.NotStrated), DecisionStatusEnumEnLabel.get(DecisionStatusEnum.NotStrated)),
    new EnumModel(DecisionStatusEnum.UnderExecution, DecisionStatusEnumArLabel.get(DecisionStatusEnum.UnderExecution), DecisionStatusEnumEnLabel.get(DecisionStatusEnum.UnderExecution)),
    new EnumModel(DecisionStatusEnum.Executed, DecisionStatusEnumArLabel.get(DecisionStatusEnum.Executed), DecisionStatusEnumEnLabel.get(DecisionStatusEnum.Executed))
  ]

  public LocationEnumLst: EnumModel[] = [
    new EnumModel(LocationEnum.Dubai, LocationEnumArLabel.get(LocationEnum.Dubai), LocationEnumEnLabel.get(LocationEnum.Dubai)),
    new EnumModel(LocationEnum.Sudan, LocationEnumArLabel.get(LocationEnum.Sudan), LocationEnumEnLabel.get(LocationEnum.Sudan)),
    new EnumModel(LocationEnum.Shared, LocationEnumArLabel.get(LocationEnum.Shared), LocationEnumEnLabel.get(LocationEnum.Shared)),
  ]

}
