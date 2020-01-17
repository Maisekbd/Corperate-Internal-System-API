import { Injectable, Inject, HostBinding, ElementRef } from '@angular/core';
import { APP_CONFIG, AppConfig } from '../app-config.module';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, Subscription, BehaviorSubject } from "rxjs";
import { map } from 'rxjs/operators';
import { Decision } from '../model/decision.model';
import { DecisionExecution } from '../model/DecisionExecution.model';

@Injectable({ providedIn: 'root' })

export class DecisionService {
  serviceUrl = `${this.config.apiEndpoint}`;
  decisionUrl: string = `${this.config.apiEndpoint}/Decision/`;

  SelectedDecision$ = new BehaviorSubject<Decision>(Decision.getDecisionDefault());
  selectedDecision: Decision = Decision.getDecisionDefault();


  searchboxTxt = new BehaviorSubject<string>("");



  constructor(@Inject(APP_CONFIG) private config: AppConfig,
    private httpClient: HttpClient) { }

  public download(fileName: string) {


    this.getFile(fileName).subscribe((result: any) => {
      if (result.type != 'text/plain') {
        var blob = new Blob([result]);
        let saveAs = require('file-saver');
        // for remove last character
        let file = fileName.slice(0, -1) + '';
        saveAs(blob, file);
      }
      else {
        alert('File not found in Blob!');
      }
    }
    );
  }

  postDecision(decisionObject: Decision) {
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let options = {
      headers: httpHeaders
    };
    return this.httpClient.post<Decision>(this.decisionUrl + `PostDecision`, decisionObject, options);
  }

  putDecision(decisionObject: Decision) {
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let options = {
      headers: httpHeaders
    };
    return this.httpClient.put<Decision>(this.decisionUrl + `PutDecision`, decisionObject, options);
  }

  public preview(fileName: string) {
    this.getFile(fileName).subscribe(res => {
      const fileURL = URL.createObjectURL(res);
      window.open(fileURL, '_blank');
    });
  }

  public executeDecision(executionDataId: number, decisionExeAttName: string, executionComment: string) {
    let descExecution = new DecisionExecution(executionDataId, 0, "", 0, 0, executionComment, null, 0, null, null, false, decisionExeAttName);
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let options = {
      headers: httpHeaders
    };
    return this.httpClient.post(this.decisionUrl + `ExecuteDecision`, descExecution, options);
  }

  public getFile(fileName: string) {
    const headers = new HttpHeaders().set('content-type', 'multipart/form-data');
    return this.httpClient.get(this.serviceUrl + '/Decision/DownloadFile' + '?fileName=' + fileName,
      { headers, responseType: 'blob' })
  }

  public getDecisionById(id: number) {
    return this.httpClient.get<Decision>(this.decisionUrl + `GetById?id=` + id, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map((meetingData) => {
      this.selectedDecision = Decision.deserialize(meetingData);
      //this.selectedMeetingInfo.next(Meeting.deserialize(meetingData));
      return this.selectedDecision;
    })
    );
  }
  /*******Get Executed Decisions********/
  public getExecutedDecisions(meetingId: number) {
    return this.httpClient.get<Decision[]>(this.decisionUrl + `GetExecutedDecisions?meetingId=` + meetingId, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map((decisionData) => {
      const decisions: Decision[] = [];
      for (let dec of decisionData) {
        dec = Decision.deserialize(dec);
        decisions.push(dec);
      }
      return decisions;
    })
    );
  }
  /***Get the latest three decision for dashboard***/


  public getLatestDecision() {
    return this.httpClient.get<Decision[]>(this.decisionUrl + `GetLatestDecisions`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map((decisionData) => {
      const decisions: Decision[] = [];
      for (let dec of decisionData) {
        dec = Decision.deserialize(dec);
        decisions.push(dec);
      }
      return decisions;
    })
    )
  }

  public getDelayDecision() {
    return this.httpClient.get<Decision[]>(this.decisionUrl + `GetDelayDecisions`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map((decisionData) => {
      const decisions: Decision[] = [];
      for (let dec of decisionData) {
        dec = Decision.deserialize(dec);
        decisions.push(dec);
      }
      return decisions;
    })
    )
  }

}







