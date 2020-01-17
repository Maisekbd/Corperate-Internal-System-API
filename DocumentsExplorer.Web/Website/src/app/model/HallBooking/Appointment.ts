import { Hall } from "./Hall.model";
import { Deserializable } from "../deserializable.model";

export class Appointment implements Deserializable {
  public Id: number;
  public Name: string;
  public MeetingroomId: number;
  public Meetingroom: Hall;
  public StartDate: Date;
  public EndDate: Date;

  constructor(id: number, name: string, roomId: number, room: Hall, startDate: Date, endDate: Date) {
    this.Id = id;
    this.Name = name;
    this.MeetingroomId = roomId;
    this.Meetingroom = room;
    this.StartDate = startDate;
    this.EndDate = endDate;
  }

  static deserialize(res: any): Appointment {
    return new Appointment(res.Id, res.Name, res.MeetingroomId, res.Meetingroom, res.StartDate, res.EndDate
    );
  }

  public static getDefault() {
    return new Appointment(0,"",0, null,null,null);
  }
}
