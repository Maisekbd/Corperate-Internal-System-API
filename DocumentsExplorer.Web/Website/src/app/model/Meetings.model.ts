import { Deserializable } from "../model/deserializable.model";
import { Round } from "../model/Round.model";
import { CouncilType } from "../model/CouncilType.model";
import { AgendaItem } from "../model/AgendaItem.model";
import { MeetingAttendance } from "../model/MeetingAttendance.model";
import { Attachment } from "../model/Attachment.model";


export class Meeting implements Deserializable {
  public Id: number;
  public MeetingNumber: number;
  public MeetingDate: Date;
  public MeetingTime: Date
  public Location: string;
  public PreparedById: string;
  public PreparedByName: string;
  public MeetingIndexNumber: number;
  //round
  public RoundId: number;
  public Round: Round;
  public CouncilTypeId: number;
  public CouncilType: CouncilType;
  public AgendaItems: AgendaItem[];
  public MeetingAttendances: MeetingAttendance[];
  public Attachments: Attachment[];
  public GeneratedMeetingNumber: string;
  public MeetingAgendaAttachmnet: string;
 // public meetingDateStr: Date;

  constructor(id: number, meetingNum: number, date: Date, time: Date, location: string,
    prepById: string, prepByName: string, meetingIndexNum: number, roundId: number, round: Round,
    councilTypeId: number, councilType: CouncilType, agenda: AgendaItem[], Attendances: MeetingAttendance[],
    attachments: Attachment[], generatedMeetingNumber: string, meetingAgendaAttachmnet:string) {
    this.Id = id;
    this.MeetingNumber = meetingNum; //Meeting Number in year
    this.MeetingDate = date;
    this.MeetingTime = time;
    this.Location = location;
    this.PreparedById = prepById;
    this.PreparedByName = prepByName;
    this.MeetingIndexNumber = meetingIndexNum; //meeting number from AAAId established Year
    this.RoundId = roundId;
    this.Round = round;
    this.CouncilTypeId = councilTypeId;
    this.CouncilType = councilType;
    this.AgendaItems = agenda;
    this.MeetingAttendances = Attendances;
    this.Attachments = attachments;
    this.GeneratedMeetingNumber = generatedMeetingNumber;
    this.MeetingAgendaAttachmnet = meetingAgendaAttachmnet;
  }

  static deserialize(meetingResponse: any): Meeting {
    return new Meeting(meetingResponse.Id, meetingResponse.MeetingNumber, meetingResponse.MeetingDate, meetingResponse.MeetingTime,
      meetingResponse.Location, meetingResponse.PreparedById, meetingResponse.PreparedByName, meetingResponse.MeetingIndexNumber,
      meetingResponse.RoundId, meetingResponse.Round, meetingResponse.CouncilTypeId, meetingResponse.CouncilType, meetingResponse.AgendaItems,
      meetingResponse.MeetingAttendances, meetingResponse.Attachments, meetingResponse.GeneratedMeetingNumber, meetingResponse.MeetingAgendaAttachmnet);
  }

  public static getDefault() {
    return new Meeting(0, 0, new Date(), new Date(), "", "", "", 0, 0, null, 0, null, [], [], [], this.generateTempMeetingNumber(),"");
  }

  private static generateTempMeetingNumber() {
    var currentdate = new Date();
    return currentdate.getFullYear() + "_" + currentdate.getMonth() + "_" + currentdate.getDay() + "_" + currentdate.getHours() + "_" + currentdate.getMinutes() + "_" + currentdate.getSeconds();
  }

}
