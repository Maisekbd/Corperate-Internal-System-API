import { Deserializable } from "../model/deserializable.model";
import { CouncilMember } from "../model/CouncilMember.model";
import { Meeting } from "../model/Meetings.model";

export class MeetingAttendance implements Deserializable {
  public Id: number;
  public Name: string;
  public Email: string;
  public JobDescription: string;
  public EmployeId: string;
  public EmployeName: string;
  public CompanyName: string
  public IsAttendant: boolean;
  public MemberType: number;
  public CouncilMemberId: number;
  public CouncilMember: CouncilMember;
  public Role: number;
  public CauseOfAbsence: string;
  public MeetingId: number;
  public Meeting: Meeting;
  public DepartmentId: number;
  public DepartmentName: string;

  constructor(id: number, name: string, email: string, jobDesc: string, empId: string, empName: string, companyName: string, isAttend: boolean,
    memeberType: number, councilMemberId: number, councilMemeber: CouncilMember, role: number, causeOfAbs: string,
    meetingId: number, meeting: Meeting, departmentId: number, departmentName: string) {
    this.Id = id;
    this.Name = name;
    this.Email = email;
    this.JobDescription = jobDesc;
    this.EmployeId = empId;
    this.EmployeName = empName;
    this.CompanyName = companyName;
    this.IsAttendant = isAttend;
    this.MemberType = memeberType;
    this.CouncilMemberId = councilMemberId;
    this.CouncilMember = councilMemeber;
    this.Role = role;
    this.CauseOfAbsence = causeOfAbs;
    this.MeetingId = meetingId;
    this.Meeting = meeting;
    this.DepartmentId = departmentId;
    this.DepartmentName = departmentName;
  }

  static deserialize(attRes: any): MeetingAttendance {
    return new MeetingAttendance(attRes.Id, attRes.name, attRes.Email, attRes.JobDescription, attRes.EmployeId,
      attRes.EmployeName, attRes.CompanyName, attRes.IsAttendant, attRes.MemberType, attRes.CouncilMemberId,
      attRes.CouncilMember, attRes.Role, attRes.CauseOfAbsence, attRes.MeetingId, attRes.Meeting, attRes.DepartmentId, attRes.DepartmentName);
  }
  public static getMeetingAttendanceDefault() {
    return new MeetingAttendance(0, "", "", "", "", "", "", null, 0, 0, null, null, "", 0, null, 0, "");
  }
}
