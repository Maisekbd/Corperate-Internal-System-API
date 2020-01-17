import { Deserializable } from "../model/deserializable.model";
import { Decision } from "./decision.model";
import { Attachment } from "./Attachment.model";

export class DecisionExecution implements Deserializable {

  public Id: number;
  public DepartmentId: number;
  public DepartmentName: string;

  /// <summary>
  /// 0- يحتاج المتابعه
  /// 1- للاطلاع
  /// </summary>
  public ActionType: number;

  /// <summary>
  /// 0- تحت التنفيذ
  /// 1- حذر 
  /// 2- متاخر 
  /// 3- تم التنفيذ
  /// </summary>
  public DecisionStatus: number;

  public ExecutionNotes: string;

  public ExecutionDate: Date;

  public DecisionId: number;

  public Decision: Decision;

  public Attachments: Attachment[];

  public NeedAction: boolean;

  public AttachementName: string;

  constructor(id: number, departmentId: number, departmentName: string, actionType: number, decisionStatus: number,
    executionNotes: string, executionDate: Date, decisionId: number, decision: Decision, attach: Attachment[], needAction: boolean, attachementName: string) {
    this.Id = id;
    this.DepartmentId = departmentId;
    this.DepartmentName = departmentName;
    this.ActionType = actionType;
    this.DecisionStatus = decisionStatus;
    this.ExecutionNotes = executionNotes;
    this.ExecutionDate = executionDate;
    this.DecisionId = decisionId;
    this.Decision = decision;
    this.Attachments = attach;
    this.NeedAction = needAction;
    this.AttachementName = attachementName;
  }

  static deserialize(Res: any): DecisionExecution {
    return new DecisionExecution(Res.Id, Res.DepartmentId, Res.DepartmentName, Res.ActionType, Res.DecisionStatus, Res.ExecutionNotes, Res.ExecutionDate, Res.DecisionId, Res.Decision, Res.Attachments, Res.NeedAction, Res.AttachementName);
  }

  public static getDefault() {
    return new DecisionExecution(0, 0, "", 0, 0, "", new Date(), 0, null, null, false, "");
  }

}
