import { Deserializable } from "../model/deserializable.model";
import { CouncilType } from './CouncilType.model';
import { MainCategory } from "./MainCategory.model";
import { SubCategory } from "./SubCategory.model";
import { Country } from "./Country.model";
import { DecisionType } from "./DecisionType.model";
import { Company } from "./Company.model";
import { DecisionExecution } from "./DecisionExecution.model";
import { AgendaItem } from "./AgendaItem.model";
import { AgendaDetails } from "./AgendaDetails.model";
import { Department } from "./Department.model";
import { ReferenceItem } from "./ReferenceItem.model";

export class Decision implements Deserializable {

  public Id: number;
  public Subject: string;
  public DecisionNumber: string;
  public DecisionText: string;
  public DecisionPath: string;
  public MainCategoryId: number;
  public MainCategory: MainCategory;
  public SubCategoryId: number;
  public SubCategory: SubCategory;
  public CountryId: number;
  public Country: Country;
  public DecisionType: DecisionType;
  public DecisionTypeId: number;
  public ConferenceYear: number;
  public ConferenceIndex: number;
  public KeyWords: string;
  public AgendaItemId: number;
  public AgendaItem: AgendaItem;
  public AgendaDetailId: number;
  public AgendaDetail: AgendaDetails;
  public DecisionAnnexPath: string;
  public DecisionStatus: number;
  public SuggestedExecutionDate: Date;
  public ReferenceItems: ReferenceItem[];
  public Companies: Company[];
  public SelectedCompaniesIds: number[];
  public DecisionExecutions: DecisionExecution[];
  public CouncilTypeId: number;
  public CouncilType: CouncilType;
  public Departments: Department[];
  public ExecutionNotes: string;
  public ExecutionDate: Date;
  public fileAtta: any;
  public IsExecutable: boolean;
  public KeyWordList: string[];

  constructor(id: number, subject: string, decisionNum: string, decisiontxt: string, decPath: string, maincatId: number, mainCat: MainCategory,
    SubCategoryId: number, SubCategory: SubCategory, CountryId: number, Country: Country, DecisionType: DecisionType,
    DecisionTypeId: number, ConferenceYear: number, ConferenceIndex: number, KeyWords: string, AgendaItemId: number,
    AgendaItem: AgendaItem, AgendaDetailId: number, AgendaDetail: AgendaDetails, DecisionAnnexPath: string,
    DecisionStatus: number, SuggestedExecutionDate: Date, ReferenceItems: ReferenceItem[], Companies: Company[], DecisionExecutions: DecisionExecution[],
    CouncilTypeId: number, CouncilType: CouncilType, Departments: Department[], ExecutionNotes: string,
    ExecutionDate: Date, fileAtta: any, IsExecutable: boolean, SelectedCompaniesIds: [], keyWordList: string[]) {
    this.Id = id;
    this.Subject = subject;
    this.DecisionNumber = decisionNum;
    this.DecisionText = decisiontxt;
    this.DecisionPath = decPath;
    this.MainCategoryId = maincatId;
    this.MainCategory = mainCat;
    this.SubCategoryId = SubCategoryId;
    this.SubCategory = SubCategory;
    this.CountryId = CountryId;
    this.Country = Country;
    this.DecisionType = DecisionType;
    this.DecisionTypeId = DecisionTypeId;
    this.ConferenceYear = ConferenceYear;
    this.ConferenceIndex = ConferenceIndex;
    this.KeyWords = KeyWords;
    this.AgendaItemId = AgendaItemId;
    this.AgendaItem = AgendaItem;
    this.AgendaDetailId = AgendaDetailId;
    this.AgendaDetail = AgendaDetail;
    this.DecisionAnnexPath = DecisionAnnexPath;
    this.DecisionStatus = DecisionStatus;
    this.SuggestedExecutionDate = SuggestedExecutionDate;
    this.ReferenceItems = ReferenceItems;
    this.Companies = Companies;
    this.DecisionExecutions = DecisionExecutions;
    this.CouncilTypeId = CouncilTypeId;
    this.CouncilType = CouncilType;
    this.Departments = Departments;
    this.ExecutionNotes = ExecutionNotes;
    this.ExecutionDate = ExecutionDate;
    this.fileAtta = fileAtta;
    this.IsExecutable = IsExecutable;
    this.SelectedCompaniesIds = SelectedCompaniesIds;
    this.KeyWordList = keyWordList;
  }
  static deserialize(res: any): Decision {
    return new Decision(res.Id, res.Subject, res.DecisionNumber, res.DecisionText, res.DecisionPath, res.MainCategoryId, res.MainCategory,
      res.SubCategoryId, res.SubCategory, res.CountryId, res.Country, res.DecisionType, res.DecisionTypeId,
      res.ConferenceYear, res.ConferenceIndex, res.KeyWords, res.AgendaItemId, res.AgendaItem, res.AgendaDetailId,
      res.AgendaDetail, res.DecisionAnnexPath, res.DecisionStatus, res.SuggestedExecutionDate, res.ReferenceItems, res.Companies,
      res.DecisionExecutions, res.CouncilTypeId, CouncilType.deserialize(res.CouncilType), res.Departments, res.ExecutionNotes,
      res.ExecutionDate, res.fileAtta, res.IsExecutable, res.SelectedCompaniesIds, res.KeyWordList
    );
  }


  public static getDecisionDefault() {
    return new Decision(0, "", "", "", "", 0, null, 0, null, 0, null, null, 0, 0, 0, "", 0, null, 0, null, "", 0, null, null, null, null, 0, null,
      null, "", null, null, false, [], []);
  }

}
