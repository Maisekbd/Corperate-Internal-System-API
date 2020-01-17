import { Injectable, Inject, HostBinding, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subject } from 'rxjs';
import { APP_CONFIG, AppConfig } from '../app-config.module';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { CouncilType } from "../model/CouncilType.model";
import { Observable, Subscription } from "rxjs";
import { tap, map } from 'rxjs/operators';
import { MainCategory } from '../model/MainCategory.model';
import { SubCategory } from '../model/SubCategory.model';
import { Country } from '../model/Country.model';
import { Round } from '../model/Round.model';
import { CouncilMember } from '../model/CouncilMember.model';
import { ReferenceType } from '../model/ReferenceType.model';
import { Company } from '../model/Company.model';
import { DecisionType } from '../model/DecisionType.model';

@Injectable({ providedIn: 'root' })
export class LookupsService {



  constructor(@Inject(APP_CONFIG) private config: AppConfig,
    private httpClient: HttpClient) { }


  public getRounds() {
    return this.httpClient.get<Round[]>(`${this.config.apiEndpoint}/Round/Gets`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (roundsData) => {

        const rounds: Round[] = [];
        for (let round of roundsData) {
          round = Round.deserialize(round);
          rounds.push(round);
        }
        return rounds;

      }
    ));
  }

  public getCompanies() {
    return this.httpClient.get<Company[]>(`${this.config.apiEndpoint}/Company/Gets`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (companyTypesData) => {

        const companyList: Company[] = [];
        for (let company of companyTypesData) {
          company = Company.deserialize(company);
          companyList.push(company);
        }
        return companyList;

      }
    ));
  }

  public getCouncils() {
    return this.httpClient.get<CouncilType[]>(`${this.config.apiEndpoint}/CouncilType/Gets`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (councilTypesData) => {

        const councilTypes: CouncilType[] = [];
        for (let council of councilTypesData) {
          council = CouncilType.deserialize(council);
          councilTypes.push(council);
        }
        return councilTypes;

      }
    ));
  }

  public getDecisionTypes() {
    return this.httpClient.get<DecisionType[]>(`${this.config.apiEndpoint}/DecisionType/Gets`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (decisionTypesData) => {

        const decisionTypes: DecisionType[] = [];
        for (let decisionType of decisionTypesData) {
          decisionType = DecisionType.deserialize(decisionType);
          decisionTypes.push(decisionType);
        }
        return decisionTypes;

      }
    ));
  }

  public getCouncilMemberByCouncilId(councilId: number) {
    return this.httpClient.get<CouncilMember[]>(`${this.config.apiEndpoint}/CouncilType/GetCoucilMembers?councilId=` + councilId, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (memberData) => {
        const councilMember: CouncilMember[] = [];
        for (let member of memberData) {
          member = CouncilMember.deserialize(member);
          councilMember.push(member);
        }
        return councilMember;
      }
    ));
  }

  public getMainCategories(councilTypeId: number) {
    return this.httpClient.get<MainCategory[]>(`${this.config.apiEndpoint}/MainCategory/Gets?councilTypeId=` + councilTypeId, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (mainCategorysData) => {
        const mainCategorys: MainCategory[] = [];
        for (let mainCat of mainCategorysData) {
          mainCat = MainCategory.deserialize(mainCat);
          mainCategorys.push(mainCat);
        }
        return mainCategorys;

      }
    ));
  }



  

  public getSubCategories(maincategoryId: number) {
    return this.httpClient.get<MainCategory[]>(`${this.config.apiEndpoint}/SubCategory/Gets?maincategoryId=` + maincategoryId, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (subCategorysData) => {
        const subCategorys: SubCategory[] = [];
        for (let subCat of subCategorysData) {
          subCat = MainCategory.deserialize(subCat);
          subCategorys.push(subCat);
        }
        return subCategorys;

      }
    ));
  }

  public getCountries() {
    return this.httpClient.get<Country[]>(`${this.config.apiEndpoint}/Country/Gets`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (CountriesData) => {
        const Countries: Country[] = [];
        for (let country of CountriesData) {
          country = Country.deserialize(country);
          Countries.push(country);
        }
        return Countries;

      }
    ));
  }

  public getReferences() {
    return this.httpClient.get<ReferenceType[]>(`${this.config.apiEndpoint}/ReferenceType/Gets`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (referenceTypeData) => {

        const referenceType: ReferenceType[] = [];
        for (let item of referenceTypeData) {
          item = ReferenceType.deserialize(item);
          referenceType.push(item);
        }
        return referenceType;

      }
    ));
  }


  public getReferenceTypeById(referenceTypeId: number) {
    return this.httpClient.get<ReferenceType>(`${this.config.apiEndpoint}/ReferenceType/GetById?id=` + referenceTypeId, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (referenceTypeData) => {
        return ReferenceType.deserialize(referenceTypeData);
      }
    ));
  }


}
