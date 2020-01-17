import { Injectable, Inject, HostBinding, ElementRef } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';
import { APP_CONFIG, AppConfig } from '../app-config.module';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, Subscription } from "rxjs";
import { map, takeUntil } from 'rxjs/operators';
import { Appointment } from '../model/Appointment';
import { EnumServiceList } from './enumList.service';
import { Hall } from '../model/Hall.model';
import { MeetingType } from '../model/MeetingType.model';
import { BookingRequestPopupComponent } from '../hall-booking/booking-request-popup/booking-request-popup.component';
import { BookingRequest } from '../model/HallBooking/BookingRequest.model';

@Injectable({ providedIn: 'root' })
export class HallBookingService {
  hallBookingUrl = `${this.config.hallBookingApiEndpoint}`;
  showBookingRequestPopupEmitter = new BehaviorSubject<number>(-1);
  refreshViewEmitter = new BehaviorSubject<boolean>(false);
  currentRequest: BookingRequest = BookingRequest.getDefault();


  constructor(@Inject(APP_CONFIG) private config: AppConfig,
    private enumService: EnumServiceList,
    private httpClient: HttpClient) { }

  getHalls(location: number, startDate: Date, endDate: Date) {
    //Date.prototype.toJSON = function () {
    //  const hoursDiff = this.getHours() - this.getTimezoneOffset() / 60;
    //  this.setHours(hoursDiff);
    //  return this.toISOString();
    //};
    let start = startDate.toUTCString();
    let end = endDate.toUTCString();
    return this.httpClient.get<Hall[]>(this.hallBookingUrl + `/Hall/Gets?location=` + location + `&startDate=` + start+ `&endDate=` + end, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (hallsData) => {

        const hallList: Hall[] = [];
        for (let hall of hallsData) {
          hall = Hall.deserialize(hall);
          hallList.push(hall);
        }
        return hallList;

      }
    ));
  }

  public getMeetingTypes() {
    return this.httpClient.get<MeetingType[]>(this.hallBookingUrl + `/MeetingType/Gets`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (meetingTypesData) => {

        const meetingTypeList: MeetingType[] = [];
        for (let meetingType of meetingTypesData) {
          meetingType = MeetingType.deserialize(meetingType);
          meetingTypeList.push(meetingType);
        }
        return meetingTypeList;

      }
    ));
  }

  public getMeetingRequests() {
    return this.httpClient.get<BookingRequest[]>(this.hallBookingUrl + `/BookingRequest/Gets`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (meetingsData) => {

        const bookingRequestList: BookingRequest[] = [];
        for (let data of meetingsData) {
          var date = new Date(data.StartDate)
          var userTimezoneOffset = date.getTimezoneOffset() * 60000;
          data.StartDate = new Date(date.getTime() - userTimezoneOffset);
          data.EndDate = new Date(new Date(data.EndDate).getTime() - userTimezoneOffset);
          bookingRequestList.push(data);
        }
        return bookingRequestList;

      }
    ));
  }

  getLocations() {
    return this.enumService.LocationEnumLst;
  }

  /**
  * Return Booking Request
  */
  public getBookingRequest(bookingId: number) {
    return this.httpClient.get<BookingRequest>(this.hallBookingUrl + `/BookingRequest/GetById?id=` + bookingId, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map((bookingRequestData) => {
      let bookingRequest: BookingRequest;
      bookingRequest = BookingRequest.deserialize(bookingRequestData);
      return bookingRequest;
    })
    );
  }

  /**/

  /**
//* Return Booking Request
//*/
//  public isAuthurizedforEditing(bookingId: number) {
//    return this.httpClient.get(this.hallBookingUrl + `/BookingRequest/IsAuthurizedforEditing?id=` + bookingId, {
//      headers: new HttpHeaders({ "Content-Type": "application/json" })
//    }).pipe(map((isAuthurized) => {
//      return isAuthurized;
//    })
//    );
//  }

///**/

  /**
   * Create new bookingRequest
   * @param newRequest
   */
  postBookingRequest(newRequest: BookingRequest) {

    //Date.prototype.toJSON = function () {
    //  const hoursDiff = this.getHours() - this.getTimezoneOffset() / 60;
    //  this.setHours(hoursDiff);
    //  return this.toISOString();
    //};

    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let options = {
      headers: httpHeaders
    };
    return this.httpClient.post<BookingRequest>(this.hallBookingUrl + `/BookingRequest/PostBookingRequest`, JSON.stringify(newRequest), options);
  }
  /**/

  /**
 * update bookingRequest
 * @param updatedRequest
 */
  putBookingRequest(request: BookingRequest) {

    Date.prototype.toJSON = function () {
      const hoursDiff = this.getHours() - this.getTimezoneOffset() / 60;
      this.setHours(hoursDiff);
      return this.toISOString();
    };

    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let options = {
      headers: httpHeaders
    };
    return this.httpClient.put<BookingRequest>(this.hallBookingUrl + `/BookingRequest/PutBookingRequest`, JSON.stringify(request), options);
  }
  /**/


  deleteBookingRequest(id: number) {
    let params = new HttpParams()
      .append('id', id.toString());
    let httpHeaders = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    let options = {
      headers: httpHeaders,
      params: params
    };
    return this.httpClient.delete(this.hallBookingUrl + `/BookingRequest/Delete`, options);
  }

}
