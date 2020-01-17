import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { CouncilType } from '../../model/CouncilType.model';
import { LookupsService } from '../../services/lookups.service';
import { MeetingsService } from '../../services/meetings.service';
import { Meeting } from '../../model/Meetings.model';
import { DecisionService } from '../../services/decision.service';
import { Decision } from '../../model/decision.model';
import { Router, ActivatedRoute } from '@angular/router';

///print libraries
import * as jspdf from 'jspdf';
import html2canvas from 'html2canvas'; 
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-decision-status',
  templateUrl: './decision-status.component.html',
  styleUrls: ['./decision-status.component.css']
})
export class DecisionStatusComponent implements OnInit {

  councils$: Observable<CouncilType[]>;
  meetings$: Observable<Meeting[]>;
  year: number;
  selectedmeetingId: number;
  councilTypeID: number = 1;
  meetingSub: Subscription;
  decisionSub: Subscription;
  meeting: Meeting = null;
  show: boolean = false;
  decisions: Decision[];
  showDecision: boolean = false;
  rtlEnabled: boolean = true;

  constructor(private lookupsService: LookupsService,
    private meetingSrv: MeetingsService,
    public sharedService: SharedService,
    private decisionSrv: DecisionService,
    private route: ActivatedRoute) {
    this.year = (new Date()).getFullYear();
    this.rtlEnabled = this.sharedService.rtlEnabled;
  }

  ngOnInit() {
    this.councils$ = this.lookupsService.getCouncils();

    if (this.route.snapshot.params['meetingId']) {
      this.selectedmeetingId = Number(this.route.snapshot.params['invoiceIds']);
      this.meetingSub = this.meetingSrv.getMeetingById(this.selectedmeetingId).subscribe(
        (data) => {
          this.meeting = data;
          this.show = true;
        });
      this.decisionSub = this.decisionSrv.getExecutedDecisions(this.selectedmeetingId).subscribe((dData) => {
        this.decisions = dData;
        this.showDecision = true;
      });
    }
  }

  onCouncilIdChanged(e) {
    this.councilTypeID = e.selectedItem.Id;
    this.getMeetingsByCouncilYear(this.councilTypeID, this.year);
  }

  onYearChanged(e) {
    this.getMeetingsByCouncilYear(this.councilTypeID, this.year);
  }

  getMeetingsByCouncilYear(councilId: number, year: number) {
    this.meetings$ = this.meetingSrv.getMeetingsByCouncilYear(councilId, year);
  }
  onSelectionChanged(e) {
    this.selectedmeetingId = e.selectedItem.Id;
    this.meetingSub = this.meetingSrv.getMeetingById(e.selectedItem.Id).subscribe(
      (data) => {
        this.meeting = data;
        this.show = true;
      });
    this.decisionSub = this.decisionSrv.getExecutedDecisions(e.selectedItem.Id).subscribe((dData) => {
      this.decisions = dData;
      this.showDecision = true;
    });
  }

  onPrintDoc() {
    var data = document.getElementById('print-section');
    html2canvas(data).then(canvas => {
      // Few necessary setting options  
      var imgWidth = 208;
      var pageHeight = 295;
      var imgHeight = canvas.height * imgWidth / canvas.width;
      var heightLeft = imgHeight;

      const contentDataURL = canvas.toDataURL('image/png')
      let pdf = new jspdf('p', 'mm', 'a4'); // A4 size page of PDF  
      var position = 0;
      pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
      pdf.save('DecisionReports.pdf'); // Generated PDF   
    }); 
  }
}
