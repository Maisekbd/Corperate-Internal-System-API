import { Deserializable } from "../model/deserializable.model";

export class Currency implements Deserializable {
  public Id: number;
  public Name: string;

  constructor(id: number,name: string) {
    this.Id = id;
    this.Name = name;
  }

  static deserialize(typeRes: any): Currency {
    return new Currency(typeRes.Id, typeRes.Name);
  }

}
