import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";


import { HomeComponent } from './home/home.component';
import { HallListComponent } from "./hall-list/hall-list.component";
import { MeetingTypeListComponent } from "./meeting-type-list/meeting-type-list.component";

const routes: Routes = [
  { path: "home", component: HomeComponent },
  { path: "hall", component: HallListComponent },
  { path: "meetingType", component: MeetingTypeListComponent },
];

@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forChild(routes)]
})
export class AdministrationRoutingModule { }
