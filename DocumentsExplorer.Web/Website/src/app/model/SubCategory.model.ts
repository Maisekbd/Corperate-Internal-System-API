import { Deserializable } from "../model/deserializable.model";
import { CouncilMember } from "../model/CouncilMember.model";

export class SubCategory implements Deserializable {

  public Id: number;
  public Description: string;

  constructor(id: number, desc: string) {
    this.Id = id;
    this.Description = desc;
  }

  static deserialize(typeRes: any): SubCategory {
    return new SubCategory(typeRes.Id, typeRes.Description);
  }

}
