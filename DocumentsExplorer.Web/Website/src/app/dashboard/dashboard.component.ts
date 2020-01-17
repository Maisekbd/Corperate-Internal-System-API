import { Component, OnInit, OnDestroy } from '@angular/core';
import { DecisionService } from '../services/decision.service';
import { MeetingsService } from '../services/meetings.service';
import { Decision } from '../model/decision.model';
import { Observable, Subscription, BehaviorSubject } from 'rxjs';
import { Meeting } from '../model/Meetings.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  providers: [DatePipe]
})
export class DashboardComponent implements OnInit, OnDestroy {
  meet: Meeting;
  decisionsLst: Decision[];
  delayDecisionsLst: Decision[];
  meetingsLst$: Observable<Meeting[]>;
  decSubscription: Subscription;
  delayDecSubscription: Subscription;
  today: any;

  constructor(
    private decisionServ: DecisionService,
    private meetingServ: MeetingsService,
    private datePipe: DatePipe) { }

  ngOnInit() {
    this.decSubscription = this.decisionServ.getLatestDecision().subscribe(result => {
      this.decisionsLst = result;
      this.delayDecSubscription = this.decisionServ.getDelayDecision().subscribe(delayResult =>
        this.delayDecisionsLst = delayResult);
    });
    this.meetingsLst$ = this.meetingServ.getLatestMeetings();
    this.today = new Date();
  }

  ngOnDestroy() {
    if (this.decSubscription)
      this.decSubscription.unsubscribe();
    if (this.delayDecSubscription)
      this.delayDecSubscription.unsubscribe();
  }
}
