import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { Decision } from '../../model/decision.model';
import { AgendaItem } from '../../model/AgendaItem.model';
import { Meeting } from '../../model/Meetings.model';
import { MeetingsService } from '../../services/meetings.service';
import { MatStepper } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-select-agenda-item',
  templateUrl: './select-agenda-item.component.html',
  styleUrls: ['./select-agenda-item.component.css']
})

export class SelectAgendaItemComponent implements OnInit, OnDestroy {


  //private _parentForm: FormGroup;
  meeting: Meeting;
  private AgendaItem: AgendaItem;
  private AgendaList: AgendaItem[];
  @Input() stepperId: MatStepper;
  agendaItemSub: any;
  agendaDetailSub: any;
  secondStep: number = 0;
  meetingSub: Subscription;

  constructor(private meetingServ: MeetingsService, private route: ActivatedRoute, private router: Router) { }

  /*ParentForm*/
  @Input() set meetingParent(meetingParent: Meeting) {
    if (meetingParent) {
      this.meeting = meetingParent;
      console.log(this.meeting.Id);
    }
  }
  get meetingParent(): Meeting {
    return this.meeting;
  }

  ngOnInit() {
    //if (this.route.snapshot.params["thirdStep"]) {
    //  this.secondStep = Number(this.route.snapshot.params["thirdStep"]);
    //}
    //this.meetingSub = this.meetingServ.meetingSelectedForDecision$.subscribe(val => {
    //  this.meeting = Meeting.deserialize(val);
    //  //console.log(this.meeting.CouncilType.Description)
    //})
  }

  onSelectAgendaItem(parentId:number, Id: number, parentText: string, text: string) {
    if (parentId == 0) {
      //this.meetingServ.agendaItemSelectedForDecisionId$.next(Id);
      this.router.navigate(['/home/AddDecision', this.meeting.Id, Id, 2]);
    }
    else {
      //this.meetingServ.agendaItemSelectedForDecisionId$.next(parentId);
      //this.meetingServ.agendaDetailSelectedForDecisionId$.next(Id);
      this.router.navigate(['/home/AddDecision', this.meeting.Id, parentId ,Id, 2])
    }
    //if (this.secondStep != 0) {
    //  this.router.navigate(['/home/DecisionEdit', 2])
    //}
    //else {
    //  this.stepperId.next();
    //}    
  }
  OnBackStep() {
    //this.meetingServ.agendaDetailSelectedForDecisionId$.next(0);
    this.stepperId.previous();
  }
  viewAgendaAttachmentClick(fileName: string) {
    this.meetingServ.previewMeetingAttachment(fileName);
  }
  ngOnDestroy() {
  //  if (this.meetingSub) {
  //    this.meetingSub.unsubscribe();
  //  }
  //  this.meetingServ.agendaItemSelectedForDecisionId$.next(0);
  //  this.meetingServ.agendaDetailSelectedForDecisionId$.next(0);
  }
}
