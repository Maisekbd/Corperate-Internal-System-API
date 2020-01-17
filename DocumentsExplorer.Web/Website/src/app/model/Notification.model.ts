import { Deserializable } from "../model/deserializable.model";

export class Notification implements Deserializable {
  public Id: number;
  public Title: string;
  public Body: string;
  public status: number;
  public DueDate: Date;
  public IsOpen: boolean;

  /// 0- related To Decision Execution
  /// 1- Personal Notification
  public NotificationType: number;
  public DecisionId: number;
  public MeetingId: number;

  constructor(id: number, title: string, body: string, status: number, dueDate: Date, isOpen: boolean, notificationType: number, decisionId: number, meetingId: number) {
    this.Id = id;
    this.Title = title;
    this.Body = body;
    this.status = status,
    this.DecisionId = decisionId;
    this.DueDate = dueDate;
    this.IsOpen = isOpen;
    this.NotificationType = notificationType;
    this.MeetingId = meetingId;
}

  static deserialize(notificationRes: any): Notification {
    return new Notification(notificationRes.Id, notificationRes.Title, notificationRes.Body, notificationRes.status, notificationRes.DueDate,
    notificationRes.IsOpen, notificationRes.NotificationType, notificationRes.DecisionId, notificationRes.MeetingId);
}

}
