import { Deserializable } from "../model/deserializable.model";

export class AgendaAttachment implements Deserializable {
  
  public ObjectId: number;
  public ObjectType: string;
  public Description: string;
  public AttachmentPath: string;
  public CreatedByName: string;

  constructor(objId: number, objType: string, desc: string, path: string, createdByName: string) {
    this.ObjectId = objId;
    this.ObjectType = objType;
    this.Description = desc;
    this.AttachmentPath = path;
    this.CreatedByName = createdByName;
  }

  static deserialize(attachRes: any): AgendaAttachment {
    return new AgendaAttachment(attachRes.ObjectId, attachRes.ObjectType, attachRes.Description,
      attachRes.AttachmentPath, attachRes.CreatedByName);
  }
  public static getAgendaAttachmentDefault() {
    return new AgendaAttachment(0,"","","","");
  }


}
