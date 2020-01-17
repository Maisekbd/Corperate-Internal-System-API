import { Deserializable } from "../model/deserializable.model";

export class DecisionType implements Deserializable {

  public Id: number;
  public Name: string;

  constructor(id: number, name: string) {
    this.Id = id;
    this.Name = name;
  }

  static deserialize(typeRes: any): DecisionType {
    return new DecisionType(typeRes.Id, typeRes.Name);
  }

}
