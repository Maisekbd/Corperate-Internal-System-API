import { Component, OnInit, OnDestroy } from '@angular/core';
import { DecisionService } from '../../services/decision.service';
import { Decision } from '../../model/decision.model';
import { Subscription, Subject } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { MeetingsService } from '../../services/meetings.service';
import { LookupsService } from '../../services/lookups.service';
import { SharedService } from '../../services/shared.service';
import { locale } from 'devextreme/localization';

@Component({
  selector: 'app-decision-read-only',
  templateUrl: './decision-read-only.component.html',
  styleUrls: ['./decision-read-only.component.css']
})
export class DecisionReadOnlyComponent implements OnInit, OnDestroy {
  uploadWithParameterURL: string = this.sharedService.serviceUrl + "/Attachment/UploadAttachmentWithParameter";
  headers: any;
  Id: number = 0;
  decisionExeAttName: string = "";
  executionComment: string = "";
  decisionSubscription: Subscription;
  decision: Decision;
  refernceDecisionList: number[] = [];
  destroy$: Subject<boolean> = new Subject<boolean>();
  show: boolean = false;

  constructor(private decisionServ: DecisionService,
    private lookupServ: LookupsService,
      private sharedService: SharedService,
     private meetingServ: MeetingsService,
    private route: ActivatedRoute,
    private router: Router) {
    locale(this.sharedService.getCurrentLanguage());
  }

  ngOnInit() {

    this.headers = { 'Authorization': 'Bearer ' + JSON.parse(localStorage.getItem('currentUser')) };
    if (this.route.snapshot.params["Id"]) {
      this.Id = Number(this.route.snapshot.params["Id"]);
    }

    this.decisionSubscription = this.decisionServ.getDecisionById(this.Id).pipe(takeUntil(this.destroy$)).subscribe((data) => {
      this.decision = Decision.deserialize(data);
      //console.log(this.decision)
      this.show = true;
      console.log(this.decision.AgendaItem);
    })

    this.lookupServ.getReferences().pipe(takeUntil(this.destroy$)).subscribe(
      (optData) => {
        this.refernceDecisionList = optData.filter(c => c.IsReferenceDecision).map(c => Number(c.Id));
      })

  }
  previewDecision(fileName: string) {
    this.decisionServ.preview(fileName)
  }
  viewAgendaAttachmentClick(fileName: string) {
    this.meetingServ.previewMeetingAttachment(fileName);
  }
  ngOnDestroy() {
    if (this.decisionSubscription)
      this.decisionSubscription.unsubscribe();
    this.destroy$.unsubscribe();
  }

  viewRefITem(refPath: string) {
    this.decisionServ.preview(refPath);
  }

  ExecuteDecision(executionDataId: number) {
    this.decisionServ.executeDecision(executionDataId, this.decisionExeAttName, this.executionComment).subscribe((data) => {
      this.router.navigate(['/home/viewDecision', this.Id ]);
    });
  }


  addUploaderParameter(e) {
    this.uploadWithParameterURL = this.sharedService.updateQueryStringParameter(this.uploadWithParameterURL, "attachmentType", 3);
    this.uploadWithParameterURL = this.sharedService.updateQueryStringParameter(this.uploadWithParameterURL, "itemNumber", this.decision.Id);
    e.component.option("uploadUrl", this.uploadWithParameterURL);
  }

  onUploadedFile(e) {
    this.decisionExeAttName = e.request.responseText;
  }
}
