import { Deserializable } from "../model/deserializable.model";
import { AgendaItem } from "../model/AgendaItem.model";

export class ReferenceType implements Deserializable {
  public Id: number;
  public Name: string;
  public IsReferenceDecision: boolean;

  constructor(id: number, name: string, isReferenceDecision: boolean) {
    this.Id = id;
    this.Name = name;
    this.IsReferenceDecision = isReferenceDecision;
  }

  static deserialize(detailRes: any): ReferenceType {
    return new ReferenceType(detailRes.Id, detailRes.Name, detailRes.IsReferenceDecision);
  }
  public static getDefault() {
    return new ReferenceType(0, "", false);
  }


}
