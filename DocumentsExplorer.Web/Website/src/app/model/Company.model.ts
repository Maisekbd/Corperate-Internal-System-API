import { Deserializable } from "../model/deserializable.model";
import { ActivitySector } from "./ActivitySector.model";
import { Decision } from "./decision.model";
import { Country } from "./Country.model";
import { Currency } from "./Currency.model";

export class Company implements Deserializable {
  public Id: number;
  public Name: string;

  public Description: string;
  public DateOfIncorporation: Date;  
  public CountryId: number;
  public Country: Country;
  public CreateDate: Date;
  public ActivitySectors: ActivitySector[];
  public Decisions: Decision[];

  public City: string;

  public Captial: number;
  public SubscribedCapital: number;
  public PaidUpCapital: number;

  public AAAIDSharesNum: number;

  public AAAIDShareValue: number;

  public AAAIDPaidCapital: number;

  public AAAIDRemainCapital: number;

  public Address: string;
  public PhoneNo: string;
  public Email: string;
  public WebSite: string;
  public Fax: string;
  public CompanyManager: string;

  public CurrencyId: number;
  public Currency: Currency;
  public FinYearStart: Date;
  public FinYearEnd : Date;
  public CompanyEstablishDate : Date;
  public AAAIDShareDate : Date;
  public InvestOpportunity: string;

  constructor(id: number, name: string, desc: string, dateOfIncorp: Date,countryId:number, country: Country,
    createdDate: Date, activeSectors: ActivitySector[], decisions: Decision[], city: string, capital: number,
    subscribedCapital: number, paidUpCapital: number, aAAIDSharesNum: number, aAAIDShareValue: number,
    aAAIDPaidCapital: number, aAAIDRemainCapital: number, address: string, phoneNo: string, email: string,
    webSite: string, fax: string, companyManager: string, currencyId: number, currency: Currency,
    finYearStart: Date, finYearEnd: Date, companyEstablishDate: Date, aAAIDShareDate: Date,
    investOpportunity: string
  ) {
    this.Id = id;
    this.Name = name;
    this.Description = desc;
    this.CountryId = countryId;
    this.Country = country;
    this.DateOfIncorporation = dateOfIncorp;
    this.CreateDate = createdDate;
    this.Decisions = decisions;
    this.ActivitySectors = activeSectors;
    this.City = city;
    this.Captial = capital;
    this.SubscribedCapital = subscribedCapital;
    this.PaidUpCapital = paidUpCapital;
    this.AAAIDSharesNum = aAAIDSharesNum;
    this.AAAIDShareValue = aAAIDShareValue;
    this.AAAIDPaidCapital = aAAIDPaidCapital;
    this.AAAIDRemainCapital = aAAIDRemainCapital;
    this.Address = address;
    this.PhoneNo = phoneNo;
    this.Email = email;
    this.WebSite = webSite;
    this.Fax = fax;
    this.CompanyManager = companyManager;
    this.CurrencyId = currencyId;
    this.Currency = currency;
    this.FinYearStart = finYearStart;
    this.FinYearEnd = finYearEnd;
    this.CompanyEstablishDate = companyEstablishDate;
    this.AAAIDShareDate = aAAIDShareDate;
    this.InvestOpportunity = investOpportunity;
  }

  static deserialize(typeRes: any): Company {
    return new Company(typeRes.Id, typeRes.Name, typeRes.Description, typeRes.DateOfIncorporation,
      typeRes.CountryId, typeRes.Country, typeRes.CreateDate, typeRes.ActivitySectors, typeRes.Decisions,
      typeRes.City, typeRes.Captial, typeRes.SubscribedCapital, typeRes.PaidUpCapital, typeRes.AAAIDSharesNum,
      typeRes.AAAIDShareValue, typeRes.AAAIDPaidCapital, typeRes.AAAIDRemainCapital, typeRes.Address,
      typeRes.PhoneNo, typeRes.Email, typeRes.WebSite, typeRes.Fax, typeRes.CompanyManager, typeRes.CurrencyId,
      typeRes.Currency, typeRes.FinYearStart, typeRes.FinYearEnd, typeRes.CompanyEstablishDate,
      typeRes.AAAIDShareDate, typeRes.InvestOpportunity
    );
  }

  public static getDefault() {
    return new Company(0,"","",null,0,null,null,null,null,"",0,0,0,0,0,0,0,"","","","","","",0,null,null,null,null,null,"");
  }
}
