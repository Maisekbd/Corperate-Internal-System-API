import { Deserializable } from "../model/deserializable.model";
import { Meeting } from "./Meetings.model";

export class Attachment implements Deserializable {
  
  public Name: string;
  public Path: string;
  public FileExtension: string;
  public MeetingId: number;
  public Meeting: Meeting; 

  constructor(name: string, path: string, fileEx: string, meetingId: number, meeting: Meeting) {
    this.Name = name;
    this.Path = path;
    this.FileExtension = fileEx;
    this.MeetingId = meetingId;
    this.Meeting = meeting;
  }

  static deserialize(attachRes: any): Attachment {
    return new Attachment(attachRes.Name, attachRes.Path, attachRes.FileExtension, attachRes.MeetingId,
      attachRes.Meeting);
  }

}
