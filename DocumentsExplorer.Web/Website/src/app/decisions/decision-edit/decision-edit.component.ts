import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Meeting } from "../../model/Meetings.model";
import { Decision } from "../../model/decision.model";
import { MeetingsService } from '../../services/meetings.service';
import { LookupsService } from '../../services/lookups.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AgendaItem } from "../../model/AgendaItem.model";
import { AgendaDetails } from "../../model/AgendaDetails.model";
import { MeetingsViewComponent } from '../../meetings-view/meetings-view.component';
import { MatStepper } from '@angular/material';
@Component({
  selector: 'app-decision-edit',
  templateUrl: './decision-edit.component.html',
  styleUrls: ['./decision-edit.component.css']
})
export class DecisionEditComponent implements OnInit, OnDestroy {

  formDecision: FormGroup;
  meetingId: number;
  decision: Decision;
  meetingSubscription: Subscription;
  meeting: Meeting;
  destroy$: Subject<boolean> = new Subject<boolean>();
  agendaMainLst: AgendaItem[];
  agendaDetailLst: AgendaDetails[];
  isReadOnly: boolean;
  @ViewChild('stepper') private Dstepper: MatStepper;
  thirdStep: number;
  meetingSub: any;
  constructor(private fb: FormBuilder, private lookupServ: LookupsService,
    private meetingServ: MeetingsService, private route: ActivatedRoute,
    private router: Router ) {
       
  } 

  ngOnInit() {
    if (this.route.snapshot.params["meetingId"]) {
      this.meetingId = this.route.snapshot.params["meetingId"];
      this.meetingSub = this.meetingServ.getMeetingById(this.meetingId).subscribe(data => {
        this.meeting = Meeting.deserialize(data);
      })
      this.Dstepper.selectedIndex = 1;
    }

    if (this.route.snapshot.params["Id"]) {
      this.Dstepper.selectedIndex = 2;
    }
    if (this.route.snapshot.params["stepperId"]) {
      this.thirdStep = Number(this.route.snapshot.params["stepperId"]);
      this.Dstepper.selectedIndex = this.thirdStep
    }

  }

  ngOnDestroy() {
    if (this.meetingSub)
      this.meetingSub.unsubscribe();
  }

}
