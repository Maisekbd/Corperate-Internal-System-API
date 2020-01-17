import { Deserializable } from "../model/deserializable.model";
import { CouncilMember } from "../model/CouncilMember.model";

export class CouncilType implements Deserializable {

  public Id: number;
  public Description: string;
  public CouncilMembers: CouncilMember[];

  constructor(id: number, desc: string, councilMembers: CouncilMember[]) {
    this.Id = id;
    this.Description = desc;
    this.CouncilMembers = councilMembers
  }

  static deserialize(typeRes: any): CouncilType {
    return new CouncilType(typeRes.Id, typeRes.Description, typeRes.CouncilMembers);
  }

}
