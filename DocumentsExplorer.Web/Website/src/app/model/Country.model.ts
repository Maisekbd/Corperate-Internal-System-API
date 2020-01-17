import { Deserializable } from "../model/deserializable.model";

export class Country implements Deserializable {
  public Id: number;
  public Name: string;

  constructor(id: number,name: string) {
    this.Id = id;
    this.Name = name;
  }

  static deserialize(typeRes: any): Country {
    return new Country(typeRes.Id, typeRes.Name);
  }

}
