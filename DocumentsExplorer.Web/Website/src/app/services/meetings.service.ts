import { Injectable, Inject, HostBinding, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Subject, BehaviorSubject } from 'rxjs';
import { APP_CONFIG, AppConfig } from '../app-config.module';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, Subscription } from "rxjs";
import { map, takeUntil } from 'rxjs/operators';
import { CouncilMember } from "../model/CouncilMember.model";
import { Meeting } from '../model/Meetings.model';

@Injectable({ providedIn: 'root' })

export class MeetingsService {

  meetingSubscribtion: Subscription;
  destroy$: Subject<boolean> = new Subject<boolean>();

  private meetingUrl: string = `${this.config.apiEndpoint}/Meeting/`;
  selectedMeeting: Meeting = Meeting.getDefault();
  meetingSelectedForDecision$ = new BehaviorSubject<Meeting>(Meeting.getDefault());
  meetingSelectedForPreview$ = new BehaviorSubject<Meeting>(Meeting.getDefault());
  agendaItemSelectedForDecisionId$ = new BehaviorSubject<number>(0);
  agendaDetailSelectedForDecisionId$ = new BehaviorSubject<number>(0);

  selectedMeetingInfo = new BehaviorSubject<Meeting>(Meeting.getDefault());
  isMeetingReadOnly$ = new BehaviorSubject<boolean>(false);

  constructor(@Inject(APP_CONFIG) private config: AppConfig,
    private httpClient: HttpClient) { }






  postMeeting(meetingObject: Meeting) {
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let options = {
      headers: httpHeaders
    };
    return this.httpClient.post<Meeting>(this.meetingUrl + `PostMeeting`, meetingObject, options);
  }

  putMeeting(meetingObject: Meeting) {
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let options = {
      headers: httpHeaders
    };
    return this.httpClient.put<Meeting>(this.meetingUrl + `PutMeeting`, meetingObject, options);
  }
   /***Get meeting for report***/

  public getMeetingsByCouncilYear(councilId: number, year: number) {
    console.log(councilId);
    return this.httpClient.get<Meeting[]>(this.meetingUrl + `GetMeetingsbyCouncilandYear?councilId=` + councilId + `&year=` + year, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map((meetingData) => {
      const meetingsLst: Meeting[] = [];
      for (let meeting of meetingData) {
        meeting = Meeting.deserialize(meeting);
        meetingsLst.push(meeting);
      }      
      return meetingsLst;
    })
    );
  }

  public getMeetingById(id: number) {
    return this.httpClient.get<Meeting>(this.meetingUrl + `GetById?id=` + id, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map((meetingData) => {
      
      this.selectedMeeting = Meeting.deserialize(meetingData);
      //this.selectedMeetingInfo.next(Meeting.deserialize(meetingData));
      return this.selectedMeeting;
    })
    );
  }


  public GetMeeting(id: number) {
    this.meetingSubscribtion = this.getMeetingById(id).pipe(takeUntil(this.destroy$)).subscribe((meetingData) => {
      this.selectedMeetingInfo.next(Meeting.deserialize(meetingData));
    });
  }

  /***Get the latest three meeting for dashboard***/


  public getLatestMeetings() {
    return this.httpClient.get<Meeting[]>(this.meetingUrl + `GetLatestMeetings`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map((meetingData) => {
      const meetingsLst: Meeting[] = [];
      for (let meeting of meetingData) {
        meeting = Meeting.deserialize(meeting);
        meetingsLst.push(meeting);
      }
      return meetingsLst;
    })
    )
  }


  public previewMeetingAttachment(fileName: string) {
    this.getFile(fileName).subscribe(res => {
      const fileURL = URL.createObjectURL(res);
      window.open(fileURL, '_blank');
    });
  }


  public getFile(fileName: string) {
    let params = new HttpParams();
    params = params.append('fileName', fileName);
    const headers = new HttpHeaders().set('content-type', 'multipart/form-data');
    return this.httpClient.get(this.meetingUrl + '/DownloadFile',
      { params: params, headers: headers, responseType: 'blob' })
    //return this.httpClient.get(this.meetingUrl + '/DownloadFile' + '?fileName=' + fileName,
    //  { headers, responseType: 'blob' })
  }


  public MergeAllFiles(id: number) {
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let options = {
      headers: httpHeaders
    };
    return this.httpClient.get(this.meetingUrl + `MergeAllFiles?id=` + id, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    });
  }

  public sendMeetingRequest(id: number) {
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let options = {
      headers: httpHeaders
    };
    return this.httpClient.get(this.meetingUrl + `SendMeetingRequest?id=` + id, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    });
  }

  
}
