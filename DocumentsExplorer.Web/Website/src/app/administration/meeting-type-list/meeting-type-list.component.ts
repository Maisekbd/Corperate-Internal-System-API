import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HallBookingService } from '../../services/hallBooking.service';
import { SharedService } from '../../services/shared.service';
import { TranslateService } from '@ngx-translate/core';
import { DictionaryService } from '../../services/dictionary.service';
import { locale, loadMessages, formatMessage } from 'devextreme/localization';
let messagesAr = require("devextreme/localization/messages/ar.json");

@Component({
  selector: 'app-meeting-type-list',
  templateUrl: './meeting-type-list.component.html',
  styleUrls: ['./meeting-type-list.component.css']
})
export class MeetingTypeListComponent implements OnInit {

  /*----- Hall URL-------------*/
  public MeetingTypeDSUrl: string = "//MeetingType//DSGets";
  public MeetingTypeCreateUrl: string = "//MeetingType//Create";
  public MeetingTypeUpdateUrl: string = "//MeetingType//Update";
  public MeetingTypeDeleteUrl: string = "//MeetingType//Delete";
  /*----- End Hall URL-------------*/

  rtlEnabled = true;
  locale: string;
   meetingTypeDS: any;
  formatMessage = formatMessage;

  constructor(private sharedService: SharedService,
    private router: Router,
    private bookingSvc: HallBookingService,
    private readonly translate: TranslateService,
    private readonly dictionaryService: DictionaryService) {
    this.locale = this.translate.defaultLang;
    this.initMessages();
    locale(this.locale);
  }

  ngOnInit() {
    this.meetingTypeDS = this.sharedService.CreateDSFullCRUD(this.bookingSvc.hallBookingUrl, this.MeetingTypeDSUrl, this.MeetingTypeCreateUrl, this.MeetingTypeUpdateUrl, this.MeetingTypeDeleteUrl);
  }


  initMessages() {
    loadMessages(messagesAr);
    loadMessages(this.dictionaryService.getMeetingTypeDictionary());
  }



}
