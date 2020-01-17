import { Deserializable } from "../model/deserializable.model";
import { Decision } from "./decision.model";
import { Company } from "./Company.model";

export class ActivitySector implements Deserializable {
  public Id: number;
  public Name: string;
  public CreateDate: Date;
  public Companies: Company[];
  public Decisions: Decision[]

  constructor(id: number, name: string, createDate:Date, companies: Company[], decisions:Decision[]) {
    this.Id = id;
    this.Name = name;
    this.CreateDate = createDate;
    this.Companies = companies;
    this.Decisions = decisions;
  }

  static deserialize(typeRes: any): ActivitySector {
    return new ActivitySector(typeRes.Id, typeRes.Name, typeRes.CreateDate, typeRes.Companies,
    typeRes.Decisions);
  }
  public static getDefault() {
    return new ActivitySector(0, "", null,null,null);
  }
}
