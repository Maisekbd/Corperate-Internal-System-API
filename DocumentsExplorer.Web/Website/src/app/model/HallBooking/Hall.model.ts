import { Deserializable } from "../deserializable.model";

export class Hall implements Deserializable {
  public Id: number;
  public Name: string;
  public Location: number

  
  constructor(id: number, name: string, loc: number) {
    this.Id = id;
    this.Name = name;
    this.Location = loc;

  }

  static deserialize(res: any): Hall {
    return new Hall(res.Id, res.Name, res.Location);
  }

  public static getDefault() {
    return new Hall(0, "", 0);
  }
}
