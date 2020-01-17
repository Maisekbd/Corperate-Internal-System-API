import { Deserializable } from "../deserializable.model";
import { LocationEnum } from "../enum/Location.enum";
import { Hall } from "./Hall.model";

export class BookingRequest implements Deserializable {
  public Id: number;
  public text: string;
  public Title: string;
  public Description: string;
  public MeetingTypeId: number;
  public MeetingTypeName: string;
  public HallId: number;
  public HallName: string;
  
  public Location: number;
  public StartDate: Date;
  public EndDate: Date;
  public IsEditable: boolean;
  public IsMine: boolean;
  public SudanHallId: number;
  
  

  constructor(id: number, name: string, loc: LocationEnum, title: string, description: string, meetingTypeId: number, meetingTypeName: string, hallId: number,
    hallName: string, startDate: Date, endDate: Date, location: number, isEditable: boolean, isMine: boolean, sudanHallId: number) {
    this.Id = id;
    this.text = name;
    this.Location = loc;
    this.Title = title;
    this.Description = description;
    this.MeetingTypeId = meetingTypeId;
    this.HallId = hallId;
    this.HallName = hallName;
    this.StartDate = new Date(startDate.toLocaleString("en-US", { timeZone: Intl.DateTimeFormat().resolvedOptions().timeZone }));
    this.EndDate = new Date(endDate.toLocaleString("en-US", { timeZone: Intl.DateTimeFormat().resolvedOptions().timeZone }));
    //this.StartDate = startDate;
    //this.EndDate = endDate;
    this.Location = location;
    this.IsEditable = isEditable;
    this.IsMine = isMine;
    this.SudanHallId = sudanHallId;
  }

  static deserialize(res: any): BookingRequest {
    return new BookingRequest(res.Id, res.text, res.Location, res.Title, res.Description, res.MeetingTypeId, res.meetingTypeName, res.HallId,
      res.Hall, res.StartDate, res.EndDate, res.Location, res.IsEditable, res.IsMine, res.SudanHallId);
  }

  public static getDefault() {
    return new BookingRequest(0, "", 0, "", "", null,"", null, "", new Date(), new Date(),0,false,true,0);
  }
}
