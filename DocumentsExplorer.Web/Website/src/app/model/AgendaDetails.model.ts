import { Deserializable } from "../model/deserializable.model";
import { AgendaItem } from "../model/AgendaItem.model";

export class AgendaDetails implements Deserializable {
  public Id: number;
  public TreeNumber: string;
  public Description: string;
  public AgendaItemId: number;
  public AgendaItem: AgendaItem;
  public AttachementName: string;


  constructor(id: number, treeNum: string, desc: string, agendaItemId: number, agendaItem: AgendaItem, attachementName: string) {
    this.Id = id;
    this.TreeNumber = treeNum;
    this.Description = desc;
    this.AgendaItemId = agendaItemId;
    this.AgendaItem = agendaItem;
    this.AttachementName = attachementName;
  }

  static deserialize(detailRes: any): AgendaDetails {
    return new AgendaDetails(detailRes.Id, detailRes.TreeNumber, detailRes.Description, detailRes.AgendaItemId, detailRes.AgendaItem, detailRes.AgendaItem);
  }
  public static getAgendaDetailsDefault() {
    return new AgendaDetails(0, "", "", 0, null, "");
  }


}
