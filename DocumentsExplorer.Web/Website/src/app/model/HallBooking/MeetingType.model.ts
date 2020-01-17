import { Deserializable } from "../deserializable.model";

export class MeetingType implements Deserializable {

  public Id: number;
  public Name: string;

  constructor(id: number, name: string) {
    this.Id = id;
    this.Name = name;
  }

  static deserialize(typeRes: any): MeetingType {
    return new MeetingType(typeRes.Id, typeRes.Name);
  }

}
