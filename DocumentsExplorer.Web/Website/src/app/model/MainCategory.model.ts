import { Deserializable } from "../model/deserializable.model";
import { CouncilMember } from "../model/CouncilMember.model";

export class MainCategory implements Deserializable {

  public Id: number;
  public Description: string;

  constructor(id: number, desc: string) {
    this.Id = id;
    this.Description = desc;
  }

  static deserialize(typeRes: any): MainCategory {
    return new MainCategory(typeRes.Id, typeRes.Description);
  }

}
