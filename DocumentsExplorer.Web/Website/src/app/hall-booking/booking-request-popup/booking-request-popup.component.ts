import { Component, OnInit, ViewChild } from '@angular/core';
import { SharedService } from '../../services/shared.service';
import { HallBookingService } from '../../services/hallBooking.service';
import { Subject, Subscription, Observable } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { EnumModel } from '../../Model/Enum.model';
import { Hall } from '../../model/HallBooking/Hall.model';
import { MeetingType } from '../../model/HallBooking/MeetingType.model';
import { BookingRequest } from '../../model/HallBooking/BookingRequest.model';
import { takeUntil, finalize } from 'rxjs/operators';
import notify from 'devextreme/ui/notify';
import { DxValidationGroupComponent } from 'devextreme-angular';
import { LocationEnum } from '../../model/enum/Location.enum';

@Component({
  selector: 'app-booking-request-popup',
  templateUrl: './booking-request-popup.component.html',
  styleUrls: ['./booking-request-popup.component.css']
})
export class BookingRequestPopupComponent implements OnInit {
  private showPopupsubscribtion: Subscription;
  rtlEnabled: boolean = this.sharedService.rtlEnabled;
  directoriesDS: any;
  destroy$: Subject<boolean> = new Subject<boolean>();
  currentRequest: BookingRequest = BookingRequest.getDefault();
  isPopupVisible: boolean = false;
  minDateValue: Date = new Date();
  minEndDateValue: Date = new Date();
  maxDateValue: Date = new Date();
  popuptitle: string = "";
  showDubaiHall: boolean = false;
  meetingType$: Observable<MeetingType[]>;
  halls$: Observable<Hall[]>;
  dubaiHalls$: Observable<Hall[]>;
  sudanHalls$: Observable<Hall[]>;
  locationDS: EnumModel[] = [];
  @ViewChild('popupValidationGroup') popupValidationGroup: DxValidationGroupComponent;
  constructor(private sharedService: SharedService,
    private bookingSvc: HallBookingService,
    private translate: TranslateService) {
  }

  ngOnInit() {

    this.translate.setDefaultLang(this.sharedService.getCurrentLanguage());
    this.translate.use(this.sharedService.getCurrentLanguage())
    this.locationDS = this.bookingSvc.getLocations();
    this.meetingType$ = this.bookingSvc.getMeetingTypes();


    this.showPopupsubscribtion = this.bookingSvc.showBookingRequestPopupEmitter.subscribe((actionTypeChanged) => {
      this.isPopupVisible = actionTypeChanged > -1;
      if (actionTypeChanged >= 0) {
        this.currentRequest = this.bookingSvc.currentRequest;
        this.minEndDateValue = this.currentRequest.StartDate;
        this.viewPopup(actionTypeChanged);
      }
    });
  }

  disabledDates(data) {
    let day = data.date.getDay();
    let isWeekend = (day === 5 || day === 6);
    return data.view === "month" && isWeekend;
  };

  setMaxDate(e) {
    if (e.value) {
      this.maxDateValue = new Date(e.value.getFullYear(), e.value.getMonth(), e.value.getDate(), 24, 0, 0);
      this.currentRequest.EndDate = new Date(this.currentRequest.StartDate.getTime() + (30 * 60 * 1000));
      this.minEndDateValue = e.value;
    }
    this.halls$ = this.bookingSvc.getHalls(this.currentRequest.Location, e.value, this.bookingSvc.currentRequest.EndDate);
  }

  viewPopup(actionId: number) {
    this.maxDateValue = new Date(this.bookingSvc.currentRequest.EndDate.getFullYear(), this.bookingSvc.currentRequest.EndDate.getMonth(), this.bookingSvc.currentRequest.EndDate.getDate(), 23, 59, 0);
    this.halls$ = this.bookingSvc.getHalls(this.bookingSvc.currentRequest.Location, this.bookingSvc.currentRequest.StartDate, this.bookingSvc.currentRequest.EndDate);

  }

  onLocationChanged($event: any) {
    this.dubaiHalls$ = null;
    this.sudanHalls$ = null;
    this.halls$ = null;
    this.currentRequest.HallId = 0;
    this.currentRequest.SudanHallId = 0;
    if ($event.value == LocationEnum.Shared) {
      this.showDubaiHall = true;
      this.dubaiHalls$ = this.bookingSvc.getHalls(0, this.bookingSvc.currentRequest.StartDate, this.bookingSvc.currentRequest.EndDate);
      this.sudanHalls$ = this.bookingSvc.getHalls(1, this.bookingSvc.currentRequest.StartDate, this.bookingSvc.currentRequest.EndDate);
    }
    else {
      this.showDubaiHall = false;
      this.halls$ = this.bookingSvc.getHalls($event.value, this.bookingSvc.currentRequest.StartDate, this.bookingSvc.currentRequest.EndDate);

    }

  }

  onEndDateChanged($event: any) {
    this.halls$ = this.bookingSvc.getHalls(this.currentRequest.Location, this.bookingSvc.currentRequest.StartDate, $event.value);
  }


  OnSaveRequet() {
    if (this.popupValidationGroup.instance.validate().isValid) {
      console.log(this.currentRequest);
      var type: string;
      var text: string;
      if (this.currentRequest.Id == 0) {
        this.bookingSvc.postBookingRequest(this.currentRequest).pipe(
          takeUntil(this.destroy$),
          finalize(() => {
            //this.archiveService.showDirPopupEmitter.next(0);
            notify(text, type, 300);
            this.isPopupVisible = false;
            this.bookingSvc.refreshViewEmitter.next(true);

          }))
          .subscribe(
            (data) => {
              type = "success"; text = "تم الاضافة" + "حجز القاعه" + "بنجاح"
            },
            (error) => {
              //var handeledError = this.handleError(error);
              //type = handeledError.type;
              //text = handeledError.text;
            });
      }
      else {
        this.bookingSvc.putBookingRequest(this.currentRequest).pipe(
          takeUntil(this.destroy$),
          finalize(() => {
            notify(text, type, 300);
            this.isPopupVisible = false;
            this.bookingSvc.refreshViewEmitter.next(true);
          }))
          .subscribe(
            (data) => {
              //this.refreshDirectoryTreeList();
              //this.refreshFolderContent();
              type = "success"; text = "تم الاضافة" + "حجز القاعه" + "بنجاح"

            },
            (error) => {
              //var handeledError = this.handleError(error);
              //type = handeledError.type;
              //text = handeledError.text;
            });
      }
    }
  }

  //onRename(currentId: number, newName: string) {
  //  var type: string;
  //  var text: string;
  //  this.archiveService.rename(currentId, newName).pipe(
  //    takeUntil(this.renameDestroy$), finalize(() => {
  //      this.archiveService.showDirPopupEmitter.next(0);
  //      notify(text, type, 300);
  //    }))
  //    .subscribe((data) => {
  //      //this.refreshDirectoryTreeList();
  //      //this.refreshFolderContent();
  //      type = "success"; text = "تم تغيير اسم المجلد" + newName + "بنجاح"
  //    },
  //      (error) => {
  //        var handeledError = this.handleError(error);
  //        type = handeledError.type;
  //        text = handeledError.text;
  //      })
  //}

  //OnMove(currentId: number) {
  //  var type: string;
  //  var text: string;
  //  this.archiveService.move(currentId, this.newParentId).pipe(
  //    takeUntil(this.moveDestroy$), finalize(() => {
  //      this.archiveService.showDirPopupEmitter.next(0);
  //      notify(text, type, 300);
  //    }))
  //    .subscribe((data) => { type = "success"; text = "تم نقل المجلد إلى" + this.newParentId + "بنجاح" },
  //      (error) => { type = "error"; text = "لم يتم نقل المجلد إلى" + this.newParentId + "حاول لاحقاً" })
  //}

  //selectItemToMove(e) {
  //  this.newParentId = e.selectedRowKeys[0];
  //}

  //handleError(error: any) {
  //  if (error.error == "1")
  //    return { type: "error", text: "dublicate folder name" }
  //  else return { type: "error", text: "لم يتم تغيير اسم المجلد" + "حاول لاحقاً" }
  //}

  ngOnDestroy() {
    this.showPopupsubscribtion.unsubscribe();
  }

}
