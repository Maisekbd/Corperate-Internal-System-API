import { Deserializable } from "../model/deserializable.model";
import { CouncilType } from "../model/CouncilType.model";
import { Country } from "../model/Country.model";


export class CouncilMember implements Deserializable {
  public Id: number;
  public Name: string;
  public CountryId: number;
  public Country: Country;
  public Position: string;
  public JoiningDate: Date;
  public FinishDate: Date;
  public memberRole: number;
  public PhotoPath: string;
  public IsActive: boolean;
  public CreateDate: Date;
  public CouncilTypeId: number;
  public CouncilType: CouncilType;

  constructor(id: number, name: string, countId: number, country: Country, position: string, joinDate: Date, finishDate: Date,
    memberRole: number, photourl: string, isActive: boolean, createDate: Date, councilTypeId: number
    , council: CouncilType) {
    this.Id = id;
    this.Name = name;
    this.CountryId = countId;
    this.Country = country;
    this.Position = position;
    this.JoiningDate = joinDate;
    this.FinishDate = finishDate;
    this.memberRole = memberRole;
    this.PhotoPath = photourl;
    this.IsActive = isActive;
    this.CreateDate = createDate;
    this.CouncilTypeId = councilTypeId;
    this.CouncilType = council;
  }

  static deserialize(councilMemeber: any): CouncilMember {
    return new CouncilMember(councilMemeber.Id ,councilMemeber.Name, councilMemeber.CountryId, councilMemeber.Country,
      councilMemeber.Position, councilMemeber.JoiningDate, councilMemeber.FinishDate, councilMemeber.memberRole,
      councilMemeber.PhotoPath, councilMemeber.IsActive, councilMemeber.CreateDate, councilMemeber.CouncilTypeId,
      councilMemeber.CouncilType);
  }
}
