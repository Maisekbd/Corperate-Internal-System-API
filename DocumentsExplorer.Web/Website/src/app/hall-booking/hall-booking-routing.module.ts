import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";


import { MeetingRoomBookingComponent } from './meeting-room-booking/meeting-room-booking.component';


const routes: Routes = [
  { path: "meetingroom", component: MeetingRoomBookingComponent },
];

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forChild(routes)]
})
export class HallBookingRoutingModule { }
