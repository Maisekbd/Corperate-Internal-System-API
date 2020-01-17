import { Deserializable } from "../model/deserializable.model";
import { Meeting } from "../model/Meetings.model";
import { AgendaDetails } from "../model/AgendaDetails.model";

export class AgendaItem implements Deserializable {
  public Id: number;
  public AgendaText: string;
  public AgendaNumber: string;
  public PresentedBy: string;
  public Conclusion: string;
  public MeetingId: number;
  public Meeting: Meeting;
  public AgendaDetails: AgendaDetails[];
  public AttachementName: string;

  ///For UI Developing needs
  public RowIndex: number;

    constructor(id: number,txt: string, agendaNumber: string, presentedBy: string, conc: string, meetingNum: number,
        meeting: Meeting, details: AgendaDetails[], rowIndex: number, attachmentName: string) {
        this.Id = id;
    this.AgendaText = txt;
    this.AgendaNumber = agendaNumber;
    this.PresentedBy = presentedBy;
    this.Conclusion = conc;
    this.MeetingId = meetingNum;
    this.Meeting = meeting;
    this.AgendaDetails = details;
      this.RowIndex = rowIndex;
      this.AttachementName = attachmentName;
  }

  static deserialize(agendaRes: any): AgendaItem {
    return new AgendaItem(agendaRes.Id, agendaRes.AgendaText, agendaRes.AgendaNumber, agendaRes.PresentedBy,
      agendaRes.Conclusion, agendaRes.MeetingId, agendaRes.Meeting, agendaRes.AgendaDetails, 0, agendaRes.AttachementName);
  }

  public static getAgendaDefault() {
    return new AgendaItem(0,null,"1","","",null,null,null,0,"");
  }
}
