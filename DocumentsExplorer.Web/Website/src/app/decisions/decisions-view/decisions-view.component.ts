import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { SharedService } from '../../services/shared.service';

import { TranslateService } from '@ngx-translate/core';
import { DictionaryService } from '../../services/dictionary.service';
import { locale, loadMessages, formatMessage } from 'devextreme/localization';
import 'devextreme-intl'
import { CouncilType } from '../../model/CouncilType.model';
import { Observable, Subscription, Subject } from 'rxjs';
import DataSource from "devextreme/data/data_source";
import { LookupsService } from '../../services/lookups.service';
import { Router } from '@angular/router';
import { DecisionService } from '../../services/decision.service';
import { Decision } from '../../model/decision.model';
import { takeUntil, finalize } from 'rxjs/operators';
import { AuthenticationService } from '../../services/authentication.service';
import { SystemRoleEnum } from '../../model/enum/System.enum';
import { EnumServiceList } from '../../services/enumList.service';
//let messagesAr = require("devextreme/localization/messages/ar.json");
//let MessagesEn = require("devextreme/localization/messages/en.json");


@Component({
  selector: 'app-decisions-view',
  templateUrl: './decisions-view.component.html',
  styleUrls: ['./decisions-view.component.css']
})
export class DecisionsViewComponent implements OnInit, OnDestroy {
  rtlEnabled = true;
  decisionsDS: any;
  mainCategoryDS: any;
  CouncilFilterGridDS: any;
  MainCategoryFilterGridDS: any;
  SubCategoryFilterGridDS: any;
  CountryFilterGridDS: any;
  subCategoryDS: any;
  DecisionStatusDS: any;
  locale: string;
  formatMessage = formatMessage;
  councils$: Observable<CouncilType[]>;
  selectedCouncilId: number;
  selectedMainCategory: string;
  selectedSubCategory: string;
  selectedYear;
  searchtxt: string;
  councilSubscribtion: Subscription;
  mainCatSubscribtion: Subscription;
  subCatSubscribtion: Subscription;
  countrySubscribtion: Subscription;
  decisionSubscribtion: Subscription;
  destroy$: Subject<boolean> = new Subject<boolean>();
  EditableMode: string[] = [SystemRoleEnum.DecisionEditor];

  constructor(
    private dictionaryService: DictionaryService,
    private lookupsService: LookupsService,
    private decisionService: DecisionService,
    private sharedService: SharedService,
    private enumListService: EnumServiceList,
    public AuthService: AuthenticationService,
    private readonly translate: TranslateService,
    private router: Router
  ) {
    this.initMessages();
    locale(this.sharedService.getCurrentLanguage());
    this.onCouncilChanged = this.onCouncilChanged.bind(this);
  }

  ngOnInit() {

    this.refreshMainCategories();
    this.refreshDecisions();
    this.councils$ = this.lookupsService.getCouncils();
    this.sharedService.langChangeSubscription = this.sharedService.isRtlEmitter.subscribe(value => {
      this.rtlEnabled = this.sharedService.rtlEnabled;
    });

    this.decisionService.searchboxTxt.subscribe((searchText) => {
      this.searchtxt = searchText;
      this.refreshDecisions();
    });
    this.initDecisionStatusList();
    this.initCouncilFilterList();
    this.initMainFilterList();
    this.initSubCatFilterList();
    this.initCountryFilterList();
    this.editClick = this.editClick.bind(this);
    this.downloadClick = this.downloadClick.bind(this);
    this.viewClick = this.viewClick.bind(this);
  }





  initMessages() {
    loadMessages(this.dictionaryService.getDecisionDictionary());
  }
  getLocale() {
    var locale = sessionStorage.getItem("locale");
    return locale != null ? locale : "ar";
  }

  setLocale(locale) {
    sessionStorage.setItem("locale", locale);
  }

  ShowCouncilDescription(rowData) {
    return rowData.CouncilType.Description;
  }

  ShowMainCategoryDescription(rowData) {
    return rowData.MainCategory.Description;
  }

  ShowSubCategoryDescription(rowData) {
    return rowData.SubCategory.Description;
  }

  ShowCountryDescription(rowData) {
    return (rowData.Country ? rowData.Country.Name : "");
  }

  CouncilFilterExpression(filterValue, selectedFilterOperation, target) {
    let column = this as any;
    if (target === "filterRow" && filterValue) {
      return ["CouncilType.Description", "contains", filterValue];
    }
    return column.defaultCalculateFilterExpression.apply(this, arguments);
  }


  onCouncilChanged(e) {
    this.refreshMainCategories();
    this.refreshDecisions();
  }

  onMainCategoryChanged(e) {
    this.refreshSubCategories();
    this.selectedSubCategory = null;
    this.refreshDecisions();
  }

  onSubCategoryChanged(e) {
    this.refreshDecisions();
  }

  selectedYearValueChanged(e) {
    this.refreshDecisions();
  }


  refreshMainCategories() {
    this.mainCategoryDS = this.sharedService.CreateddlDataSourse(this.sharedService.MainCategoryDSUrl + this.selectedCouncilId);

  }

  refreshSubCategories() {
    this.subCategoryDS = this.sharedService.CreateddlDataSourse(this.sharedService.SubCategoryDSUrl + this.selectedMainCategory);
  }

  refreshDecisions() {
    let filterQieryString: string = "";
    filterQieryString = (this.selectedCouncilId ? "?councilTypeId=" + this.selectedCouncilId : "")
      + (this.selectedMainCategory ? "&mainCategoryId=" + this.selectedMainCategory : "")
      + (this.selectedSubCategory ? "&subCategoryId=" + this.selectedSubCategory : "");
    filterQieryString += (filterQieryString != "" ? "&" : "?") + (this.selectedYear ? "conferenceYear=" + this.selectedYear : "");
    filterQieryString += (filterQieryString != "" ? "&" : "?") + (this.searchtxt ? "txtSerach=" + this.searchtxt : "");
    this.decisionsDS = this.sharedService.CreateDataSourse(this.sharedService.DecisionsDSUrl
      + filterQieryString
      , "//Decision//Delete");

  }




  initCouncilFilterList() {
    this.councilSubscribtion = this.lookupsService.getCouncils().subscribe(
      (optData) => {
        this.CouncilFilterGridDS = optData.map(c => {
          var filterValue: any = {
            text: c.Description,
            value: ["CouncilTypeId", "=", c.Id]
          }
          return filterValue;
        });
      })
  }

  initDecisionStatusList() {

    this.DecisionStatusDS = this.enumListService.DecisionStatusEnumLst.map(c => {
      var filterValue: any = {
        text: this.sharedService.getCurrentLanguage() == this.sharedService.appArLang ? c.NameAr : c.NameEn,
        value: ["DecisionStatus", "=", c.Id]
      }
      return filterValue;
    });
  }

  initMainFilterList() {
    this.mainCatSubscribtion = this.lookupsService.getMainCategories(this.selectedCouncilId).subscribe(
      (optData) => {
        this.MainCategoryFilterGridDS = optData.map(c => {
          var filterValue: any = {
            text: c.Description,
            value: ["MainCategoryId", "=", c.Id]
          }
          return filterValue;
        });
      })
  }

  initSubCatFilterList() {
    this.subCatSubscribtion = this.lookupsService.getSubCategories(this.selectedCouncilId).subscribe(
      (optData) => {
        this.SubCategoryFilterGridDS = optData.map(c => {
          var filterValue: any = {
            text: c.Description,
            value: ["SubCategoryId", "=", c.Id]
          }
          return filterValue;
        });
      })
  }

  initCountryFilterList() {
    this.countrySubscribtion = this.lookupsService.getCountries().subscribe(
      (optData) => {
        this.CountryFilterGridDS = optData.map(c => {
          var filterValue: any = {
            text: c.Name,
            value: ["CountryId", "=", c.Id]
          }
          return filterValue;
        });
      })
  }

  editClick(e) {
    this.router.navigate(['/home/DecisionEdit', e.row.data.Id]);
  }

  downloadClick(e) {
    this.decisionService.download(e.row.data.DecisionPath);
  }

  viewClick(e) {
    this.decisionService.preview(e.row.data.DecisionPath);
  }
  viewDecision(data) {
    //console.log(data);
    // alert(data.data.Id)
    this.decisionSubscribtion = this.decisionService.getDecisionById(Number(data.data.Id))
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => { this.router.navigate(['/home/viewDecision', data.data.Id]); }))
      .subscribe((decisionData) => {
        this.decisionService.SelectedDecision$.next(Decision.deserialize(decisionData));
      });
    //this.decisionService.SelectedDecision$.next(data.data.Id);
    // this.router.navigate(['/home/viewDecision']);
  }

  ngOnDestroy() {
    this.councilSubscribtion.unsubscribe();
    this.mainCatSubscribtion.unsubscribe();
    this.subCatSubscribtion.unsubscribe();
    this.countrySubscribtion.unsubscribe();
  }

}
