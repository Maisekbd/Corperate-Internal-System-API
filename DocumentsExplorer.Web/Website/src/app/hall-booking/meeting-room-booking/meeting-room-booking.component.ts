import { Component, OnInit, ViewChild, TemplateRef, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { HallBookingService } from '../../services/hallBooking.service';
import { SharedService } from '../../services/shared.service';
import { locale } from 'devextreme/localization';
import {
  DxSchedulerComponent
} from 'devextreme-angular';
import { EnumServiceList } from '../../services/enumList.service';
import { Hall } from '../../model/Hall.model';
import { EnumModel } from '../../Model/Enum.model';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { Subscription, Subject, Observable } from 'rxjs';
import { BookingRequest } from '../../model/HallBooking/BookingRequest.model';
import { takeUntil, finalize } from 'rxjs/operators';
import { AuthenticationService } from '../../services/authentication.service';
import { SystemRoleEnum } from '../../model/enum/System.enum';
import { confirm } from 'devextreme/ui/dialog';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import DataSource from "devextreme/data/data_source";
@Component({
  selector: 'app-meeting-room-booking',
  templateUrl: './meeting-room-booking.component.html',
  styleUrls: ['./meeting-room-booking.component.css'],
})
export class MeetingRoomBookingComponent implements OnInit, OnDestroy {
  @ViewChild(DxSchedulerComponent) scheduler: DxSchedulerComponent;/*, { static: false }*/
  private refreshViewsubscribtion: Subscription;
  destroy$: Subject<boolean> = new Subject<boolean>();
  currentDate: Date = new Date();
  weeksViewStartDate: Date = new Date();
  //currentDate: Date = new Date();
  daysViewStartDate: Date = new Date();
  appointmentDS: any;
  appointments$: Observable<BookingRequest[]>;
  rtlEnabled: boolean = true;
  HallData: Hall[];
  locationDS: EnumModel[] = [];
  adaptivityEnabled: boolean = false;
  EditableMode: string[] = [SystemRoleEnum.BookingAdmin];
  deleteconfirstr: string = "";
  deleteConfirmTitle: string = "";
  currentLocation: any =Intl.DateTimeFormat().resolvedOptions().timeZone;
  constructor(
    //private modal: NgbModal,
    private bookingSrv: HallBookingService,
    private sharedService: SharedService,
    private authSvc: AuthenticationService,
    private translate: TranslateService,
    private enumService: EnumServiceList) {
    locale(this.sharedService.getCurrentLanguage());
    this.rtlEnabled = sharedService.rtlEnabled;
    this.currentLocation = "Asia/Tehran";
  }

  ngOnInit() {
    this.getMessages();
   
    console.log(Intl.DateTimeFormat().resolvedOptions().timeZone);
    this.translate.setDefaultLang(this.sharedService.getCurrentLanguage());
    this.translate.use(this.sharedService.getCurrentLanguage())
    this.translate.onLangChange.subscribe((event: LangChangeEvent) => {
      this.getMessages();
    });
    console.log(window.innerWidth);
    this.adaptivityEnabled = window.innerWidth <= 1034;
    this.locationDS = this.bookingSrv.getLocations();
    this.refreshScheduler();
    this.refreshViewsubscribtion = this.bookingSrv.refreshViewEmitter.subscribe((refreshChanged) => {
      if (refreshChanged) {
        this.refreshScheduler();
        this.bookingSrv.currentRequest = BookingRequest.getDefault();
        this.bookingSrv.refreshViewEmitter.next(false);
      }
    });
  }

  getMessages() {
    this.translate.get(['hallBookingMessages.deleteConfirmStr', 'hallBookingMessages.deleteConfirmTitle']).subscribe((res: string) => {
      this.deleteconfirstr = res['hallBookingMessages.deleteConfirmStr'];
      this.deleteConfirmTitle = res['hallBookingMessages.deleteConfirmTitle'];
    });
  }

  refreshScheduler() {
    let requests: BookingRequest[] = [];
    this.appointments$ = this.bookingSrv.getMeetingRequests();
    this.appointmentDS = this.sharedService.CreateDS(this.sharedService.hallBookingUrl, "/BookingRequest/DSGets");
    //this.appointmentDS = new DataSource(
    //  {
    //    store:
    //      { load: function () { this.sharedService.CreateDS(this.sharedService.hallBookingUrl, "/BookingRequest/DSGets"}})
    //  });
   //this.appointmentDS.load().
    //  .then(
    //  (list) => {
    //    for (let data of list) {
    //      var date = new Date(data.StartDate)
    //      var userTimezoneOffset = date.getTimezoneOffset() * 60000;
    //      data.StartDate = new Date(date.getTime() - userTimezoneOffset);
    //      data.EndDate = new Date(new Date(data.EndDate).getTime() - userTimezoneOffset);
    //      requests.push(data);
    //    }
    //    return requests;
    //  })

    //this.appointmentDS. = requests;
  }



  deleteBookingRequest(e, Id) {
    let result = confirm("<i>" + this.deleteconfirstr + "</i>", this.deleteConfirmTitle);
    result.then((dialogResult) => {
      if (dialogResult) {
        this.bookingSrv.deleteBookingRequest(Id).pipe(
          takeUntil(this.destroy$),
          finalize(() => {
            this.scheduler.instance.hideAppointmentTooltip();
            this.bookingSrv.refreshViewEmitter.next(true);
          }))
          .subscribe(
            (data) => {
              //  type = "success"; text = "تم الاضافة" + "حجز القاعه" + "بنجاح"
            },
            (error) => {
              //var handeledError = this.handleError(error);
              //type = handeledError.type;
              //text = handeledError.text;
            });
      }
    });
  
     
    e.event.stopPropagation();
  }

  
  EditBookingRequest(e, Id) {
    this.scheduler.instance.hideAppointmentTooltip();
    this.bookingSrv.getBookingRequest(Id)
      .subscribe((data) => {
        this.bookingSrv.currentRequest = data;
        this.bookingSrv.showBookingRequestPopupEmitter.next(Id);
      }
    )
    e.event.stopPropagation();
  }

  onAppointmentFormOpening($event: any) {
    const form = $event.form;
    form.itemOption('allDay', { visible: false });
    form.itemOption('recurrenceRule', { visible: false });
  }

 
  isEditable: boolean = false;
  appointmentDblClick($event: any) {
    $event.cancel = true;
    console.log($event.appointmentData);
  }

  //appointmentClick($event: any) {
  //  this.isEditable = this.isCreatedbyUser($event.appointmentData.Id);
  //}

  onAppointmentRendered($event: any) {

    //var date = new Date($event.appointmentData.StartDate)
    //var userTimezoneOffset = date.getTimezoneOffset() * 60000;
    //console.log( new Date(date.getTime() - userTimezoneOffset));

    //$event.appointmentData.StartDate = new Date(date.getTime() - userTimezoneOffset);
    //$event.appointmentData.EndDate = new Date(new Date($event.appointmentData.EndDate).getTime() - userTimezoneOffset);
    if ($event.appointmentData.IsMine)
      $event.appointmentElement.style.backgroundColor = "#7ac142";
  }


  cellClick($event: any) {
    $event.cancel = true;
    this.bookingSrv.currentRequest.StartDate = $event.cellData.startDate;
    this.bookingSrv.currentRequest.EndDate = $event.cellData.endDate;
    this.bookingSrv.showBookingRequestPopupEmitter.next(0);
    console.log($event.appointmentData);
  }

  ngOnDestroy() {
    if (this.refreshViewsubscribtion)
      this.refreshViewsubscribtion.unsubscribe();
  }


}
