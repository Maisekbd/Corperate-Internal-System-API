import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../services/shared.service';
import { Router } from '@angular/router';
import { DictionaryService } from '../../services/dictionary.service';
import { locale, loadMessages, formatMessage } from 'devextreme/localization';
import { TranslateService } from '@ngx-translate/core';
import { EnumModel } from '../../Model/Enum.model';
import { HallBookingService } from '../../services/hallBooking.service';

let messagesAr = require("devextreme/localization/messages/ar.json");
@Component({
  selector: 'app-hall-list',
  templateUrl: './hall-list.component.html',
  styleUrls: ['./hall-list.component.css']
})
export class HallListComponent implements OnInit {

  /*----- Hall URL-------------*/
  public HallDSUrl: string = "//Hall//DSGets";
  public HallCreateUrl: string = "//Hall//Create";
  public HallUpdateUrl: string = "//Hall//Update";
  public HallDeleteUrl: string = "//Hall//Delete";
  /*----- End Hall URL-------------*/

  rtlEnabled = true;
  locale: string;
  hallDS: any;
  formatMessage = formatMessage;
  locationDS: EnumModel[] = [];

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
    this.locationDS = this.bookingSvc.getLocations();
    this.hallDS = this.sharedService.CreateDSFullCRUD(this.bookingSvc.hallBookingUrl, this.HallDSUrl, this.HallCreateUrl, this.HallUpdateUrl, this.HallDeleteUrl);
  }


  initMessages() {
    loadMessages(messagesAr);
    loadMessages(this.dictionaryService.getHallDictionary());
  }


  onValueChanged(e, cellInfo) {
    if (cellInfo.setValue)
      cellInfo.setValue(e.value);
  }

}
