import { Deserializable } from "../model/deserializable.model";
import { CouncilType } from "../model/CouncilType.model";

export class Round implements Deserializable {
  public Id: number;
  public RoundNumber: string;
  public FromDate: Date;
  public ToDate: Date;
  public CreateDate: Date;
  //Council
  public CouncilTypeId: number;
  public CouncilType: CouncilType;

  constructor(id: number, roundNum: string, fromDate: Date, toDate: Date, createDate: Date, counTypeId: number,
    counType: CouncilType) {
    this.Id = id;
    this.RoundNumber = roundNum;
    this.FromDate = fromDate;
    this.ToDate = toDate;
    this.CreateDate = createDate;
    this.CouncilTypeId = counTypeId;
    this.CouncilType = counType;
  }

  static deserialize(roundRes: any): Round {
    return new Round(roundRes.Id, roundRes.RoundNumber, roundRes.FromDate, roundRes.ToDate, roundRes.CreateDate,
      roundRes.CouncilTypeId, roundRes.CouncilType);
  }

}
