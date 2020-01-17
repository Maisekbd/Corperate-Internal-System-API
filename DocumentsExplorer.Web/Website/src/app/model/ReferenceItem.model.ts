import { Deserializable } from "../model/deserializable.model";
import { AgendaItem } from "../model/AgendaItem.model";
import { ReferenceType } from "./ReferenceType.model";
import { Decision } from "./decision.model";
import { Reference } from "@angular/compiler/src/render3/r3_ast";

export class ReferenceItem implements Deserializable {
  public Id: number;
  public RefereceItemNo: number;
  public Name: string;
  public Description: string;
  public Path: string;
  public FileExtension: string;
  public ReferenceTypeId: number;
  public ReferenceType: ReferenceType;
  public DecisionId: number;
  public Decision: Decision;
  public ReferenceDecisionId: number;
  public RefDecisionPath: string;
  public ReferenceDecision: Decision;

  constructor(id: number, refereceItemNo: number, name: string, desc: string, path: string, fileEx: string, refTypeId: number,
    refType: ReferenceType, decisionID: number, decision: Decision, refDecisionId: number, refDecision: Decision, refDecisionPath: string) {
    this.Id = id;
    this.RefereceItemNo = refereceItemNo;
    this.Name = name;
    this.Description = desc;
    this.Path = path;
    this.FileExtension = fileEx;
    this.ReferenceTypeId = refTypeId;
    this.ReferenceType = refType;
    this.DecisionId = decisionID;
    this.Decision = decision;
    this.ReferenceDecisionId = refDecisionId;
    this.ReferenceDecision = refDecision;
    this.RefDecisionPath = refDecisionPath;
  }

  static deserialize(detailRes: any): ReferenceItem {
    return new ReferenceItem(detailRes.Id, detailRes.RefereceItemNo, detailRes.Name, detailRes.Description, detailRes.Path,
      detailRes.FileExtension, detailRes.ReferenceTypeId, detailRes.ReferenceType, detailRes.DecisionId,
      detailRes.Decision, detailRes.ReferenceDecisionId, detailRes.ReferenceDecision, detailRes.RefDecisionPath);
  }
  public static getDefault() {
    return new ReferenceItem(0, 0, "", "", "", "", 0, null, 0, null, 0, null, "");
  }


}
