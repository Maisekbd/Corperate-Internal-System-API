import { Component, OnInit, OnDestroy } from '@angular/core';
import { MeetingsService } from '../../services/meetings.service';
import { Meeting } from '../../model/Meetings.model';
import { AgendaItem } from '../../model/AgendaItem.model';
import { EnumModel } from '../../model/Enum.model';
import { EnumServiceList } from '../../services/enumList.service';
import { MeetingAttendance } from '../../model/MeetingAttendance.model';
import { MemeberTypeEnum } from '../../model/enum/MemeberType.enum';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AgendaDetails } from '../../model/AgendaDetails.model';
import { AuthenticationService } from '../../services/authentication.service';
import { SystemRoleEnum } from '../../model/enum/System.enum';

@Component({
  selector: 'app-read-only',
  templateUrl: './read-only.component.html',
  styleUrls: ['./read-only.component.css']
})
export class ReadOnlyComponent implements OnInit, OnDestroy {
   meeting: Meeting;
   AgendaItem: AgendaItem;
   AgendaList: AgendaItem[];
  Id: number = 0;
  show: boolean = false;
  meetingSubscribtion: Subscription;
  AgendaItemId: number;
  AgendaDetailId: number;
  agendaItem: AgendaItem[] =[];
  agendaDetail: AgendaDetails[];
  attendCouncilType: MeetingAttendance[];
  attendEmployeeType: MeetingAttendance[];
  attendExternalType: MeetingAttendance[];
  destroy$: Subject<boolean> = new Subject<boolean>();
  DecisionEditableMode: string[] = [SystemRoleEnum.DecisionEditor];
  MeetingEditableMode: string[] = [SystemRoleEnum.MeetingEditor];

  constructor(
    private AuthService: AuthenticationService,
    private meetingServ: MeetingsService,
    private route: ActivatedRoute,
    private router: Router) {
    
  }

  ngOnInit() {
    //this.meetingServ.agendaItemSelectedForDecisionId$.next(0);
    //this.meetingServ.agendaDetailSelectedForDecisionId$.next(0);

    if (this.route.snapshot.params["Id"]) {
      this.Id = Number(this.route.snapshot.params["Id"]);
    }

    this.meetingSubscribtion = this.meetingServ.getMeetingById(this.Id).pipe(takeUntil(this.destroy$)).subscribe((meetingData) => {
      //this.meetingServ.selectedMeetingInfo.next(Meeting.deserialize(meetingData));
      this.meeting = Meeting.deserialize(meetingData);
      this.show = true;
      //console.log(this.meeting.AgendaItems[0].AttachementName)
      this.attendCouncilType = this.meeting.MeetingAttendances.filter(c => c.MemberType == MemeberTypeEnum.CouncilMemeber);
      this.attendEmployeeType = this.meeting.MeetingAttendances.filter(c => c.MemberType == MemeberTypeEnum.Employee);
      this.attendExternalType = this.meeting.MeetingAttendances.filter(c => c.MemberType == MemeberTypeEnum.External);
      // this.agendaItem = this.meeting.AgendaItems;
    });
 
  }

  mergeFiles(id: number) {
    this.meetingServ.MergeAllFiles(this.Id).pipe(takeUntil(this.destroy$)).subscribe((meetingData) => {
    });
  }


  sendMeetingRequest(id: number) {
    this.meetingServ.sendMeetingRequest(this.Id).pipe(takeUntil(this.destroy$)).subscribe((meetingData) => {
    });
  }

  onSelectAgendaItem(parentId: number, Id: number, parentText: string, text: string) {
    if (parentId == 0) {
      this.router.navigate(['/home/AddDecision', this.meeting.Id, Id, 2]);
    }
    else {
      this.router.navigate(['/home/AddDecision', this.meeting.Id, parentId, Id, 2])
    }   
  }
  viewAgendaAttachmentClick(fileName: string) {
    this.meetingServ.previewMeetingAttachment(fileName);
  }

  ngOnDestroy() {
    if (this.meetingSubscribtion)
      this.meetingSubscribtion.unsubscribe();

  }
}
