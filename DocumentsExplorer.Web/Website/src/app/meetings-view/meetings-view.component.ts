import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { locale, formatMessage, loadMessages } from 'devextreme/localization';
import { SharedService } from '../services/shared.service';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { DictionaryService } from '../services/dictionary.service';
import { MeetingsService } from '../services/meetings.service';
import { Subscription, Subject } from 'rxjs';
import { LookupsService } from '../services/lookups.service';
import { MatStepper } from '@angular/material';
import { takeUntil, finalize } from 'rxjs/operators';
import { Meeting } from '../model/Meetings.model';
import { SystemRoleEnum } from '../model/enum/System.enum';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-meetings-view',
  templateUrl: './meetings-view.component.html',
  styleUrls: ['./meetings-view.component.css']
})
export class MeetingsViewComponent implements OnInit, OnDestroy {
  rtlEnabled = true;
  meetingsDS: any;
  CouncilFilterGridDS: any;
  RoundFilterGridDS: any;
  locale: string;
  destroy$: Subject<boolean> = new Subject<boolean>();
  formatMessage = formatMessage;
  councilSubscribtion: Subscription;
  roundSubscribtion: Subscription;
  @Input() stepperId: MatStepper;
  selectedMeeting: Meeting;
  forAddingDecision: boolean = false;
  EditableMode: string[] = [SystemRoleEnum.MeetingEditor];
  DecisionEditableMode: string[] = [SystemRoleEnum.DecisionEditor];
 

  meetingSubscribtion: Subscription;
  meetingPreviewSubscribiton: Subscription;


  constructor(
    private dictionaryService: DictionaryService,
    private lookupsService: LookupsService,
    private meetingsService: MeetingsService,
    private sharedService: SharedService,
    public AuthService: AuthenticationService,
    private readonly translate: TranslateService,
    private router: Router
  ) {
    this.initMessages();
    locale(this.sharedService.getCurrentLanguage());

  }

  ngOnInit() {
    
    if (this.stepperId != null) {
      this.forAddingDecision = true;
    } else { this.forAddingDecision = false; }
    this.sharedService.langChangeSubscription = this.sharedService.isRtlEmitter.subscribe(value => {
      this.rtlEnabled = this.sharedService.rtlEnabled;
    });
    this.initCouncilFilterList();
    this.initRoundFilterList();
    this.meetingsDS = this.sharedService.CreateDataSourse(this.sharedService.MeetingDSUrl, "//Meeting//Delete");
    this.rtlEnabled = this.sharedService.rtlEnabled;
    this.editClick = this.editClick.bind(this);
    this.addDecision = this.addDecision.bind(this)
    //this.selectMeeting=this.selectMeeting.bind(this)
  }

  initMessages() {
    loadMessages(this.dictionaryService.getMeetingDictionary());
  }

  ShowCouncilDescription(rowData) {
    return rowData.CouncilType.Description;
  }


  CouncilFilterExpression(filterValue, selectedFilterOperation, target) {
    let column = this as any;
    if (target === "filterRow" && filterValue) {
      return ["CouncilType.Description", "contains", filterValue];
    }
    return column.defaultCalculateFilterExpression.apply(this, arguments);
  }

  initRoundFilterList() {
    this.roundSubscribtion = this.lookupsService.getRounds().subscribe(
      (optData) => {
        this.RoundFilterGridDS = optData.map(c => {
          var filterValue: any = {
            text: c.RoundNumber,
            value: ["RoundId", "=", c.Id]
          }
          return filterValue;
        });
      })
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

  editClick(e) {
    this.router.navigate(['/home/Meeting', e.row.data.Id]);
    ///Test purpost Finalize 
    //this.meetingSubscribtion = this.meetingsService.getMeetingById(Number(e.row.data.Id))
    //  .pipe(
    //    takeUntil(this.destroy$),
    //    finalize(() => { this.router.navigate(['/home/Meeting', e.row.data.Id]); }))
    //  .subscribe((meetingData) => {
    //  this.meetingsService.selectedMeetingInfo.next(Meeting.deserialize(meetingData));
    //  });

    //
  }
  addDecision(e) {
    this.router.navigate(['/home/AddDecision', e.row.data.Id])
  }
  //selectMeeting(e) {
  //  this.meetingsService.meetingSelectedForDecision$.next(e.row.data);
  //  console.log(e.row.data)
  //  this.stepperId.next();
  //}
  ngOnDestroy() {
    this.destroy$.next(true);
    if (this.councilSubscribtion)
      this.councilSubscribtion.unsubscribe();
    if (this.roundSubscribtion)
      this.roundSubscribtion.unsubscribe()
    if (this.sharedService.langChangeSubscription)
      this.sharedService.langChangeSubscription.unsubscribe();
    //this.meetingSubscribtion.unsubscribe();
  }

  viewMeeting(data) {
    this.router.navigate(['/home/viewMeeting', data.data.Id]);
    //this.meetingPreviewSubscribiton = this.meetingsService.getMeetingById(Number(data.data.Id))
    //  .pipe(
    //    takeUntil(this.destroy$),
    //    finalize(() => { this.router.navigate(['/home/viewMeeting']); }))
    //  .subscribe((meetingData) => {
    //    this.meetingsService.meetingSelectedForPreview$.next(Meeting.deserialize(meetingData));
    //  });
  }


}
