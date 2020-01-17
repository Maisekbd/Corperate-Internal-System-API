import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { HallListComponent } from './hall-list/hall-list.component';
import { MeetingTypeListComponent } from './meeting-type-list/meeting-type-list.component';

import { SharedModule } from '../share/shared.module';

import { AdministrationRoutingModule } from './administration-routing.module';


@NgModule({
  declarations: [HomeComponent, HallListComponent, MeetingTypeListComponent],
  imports: [
    CommonModule,
    SharedModule,
    AdministrationRoutingModule
  ]
})
export class AdministrationModule { }
