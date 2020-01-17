import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MeetingRoomBookingComponent } from './meeting-room-booking/meeting-room-booking.component';
import { HallBookingRoutingModule } from './hall-booking-routing.module';
import { SharedModule } from '../share/shared.module';
import { DateAdapter } from '@angular/material';
import { BookingRequestPopupComponent } from './booking-request-popup/booking-request-popup.component';


@NgModule({
  declarations: [MeetingRoomBookingComponent, BookingRequestPopupComponent],
  imports: [
    CommonModule,
    SharedModule,
    HallBookingRoutingModule,
   
  ]
})
export class HallBookingModule { }
