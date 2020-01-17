import { Injectable, Inject } from '@angular/core';




export class DecisionView {
  Subject: string;
  DecisionNumber: string;
  DecisionText: string;
  CouncilType: string;
  MainCategory: string;
  SubCategory: string;
  Country: string;
  View: string;
  Download: string;
  Edit: string;
  Delete: string;
  Year: string;
  Status: string;
}

export class DecisionDictionary {
  [key: string]: DecisionView;
}

let decisionDictionary: DecisionDictionary = {
  "en": {
    "Subject": "Subject",
    "DecisionNumber": "Decision Number",
    "DecisionText": "Decision Text",
    "CouncilType": "Council Type",
    "MainCategory": "Main Category",
    "SubCategory": "Sub Category",
    "Country": "Country",
    "View": "View",
    "Download": "Download",
    "Edit": "Edit",
    "Delete": "Delete",
    "Year": "Year",
    "Status": "Status"
  },
  "ar": {
    "Subject": "الموضوع",
    "DecisionNumber": "رقم القرار",
    "DecisionText": "نص القرار",
    "CouncilType": "المجلس",
    "MainCategory": "التصنيف الرئيسي",
    "SubCategory": "التصنيف الفرعي",
    "Country": "البلد",
    "View": "إستعراض",
    "Download": "تحميل",
    "Edit": "تحرير",
    "Delete": "حذف",
    "Year": "السنه",
    "Status": "حاله القرار"
  }
};

export class MeetingView {
  MeetingNumber: string;
  MeetingIndexNumber: string;
  MeetingDate: string;
  MeetingTime: string;
  CouncilType: string;
  Location: string;
  Round: string;
  Edit: string;
  Delete: string;
}

export class MeetingDictionary {
  [key: string]: MeetingView;
}

let meetingDictionary: MeetingDictionary = {
  "en": {
    "MeetingNumber": "Meeting Number",
    "MeetingIndexNumber": "Meeting Index",
    "MeetingDate": "Date",
    "MeetingTime": "Time",
    "CouncilType": "Council Type",
    "Round": "Round",
    "Location": "Location",
    "Edit": "Edit",
    "Delete": "Delete",
  },
  "ar": {
    "MeetingNumber": "رقم الإجتماع",
    "MeetingIndexNumber": "الرقم التسلسلي",
    "MeetingDate": "التاريخ",
    "MeetingTime": "التوقيت",
    "CouncilType": "المجلس",
    "Location": "المكان",
    "Round": "الدورة",
    "Edit": "تحرير",
    "Delete": "حذف",
  }
};

export class HallView {
  Name: string;
  Location: string;
}

export class HallDictionary {
  [key: string]: HallView;
}

let hallDictionary: HallDictionary = {
  "en": {
    "Name": "Name",
    "Location": "Location",
  },
  "ar": {
    "Name": "اسم القاعه",
    "Location": "الموقع",
  }
};

export class MeetingTypeView {
  Name: string;
}

export class MeetingTypeDictionary {
  [key: string]: MeetingTypeView;
}

let meetingTypeDictionary: MeetingTypeDictionary = {
  "en": {
    "Name": "Name",
  },
  "ar": {
    "Name": "الاسم",
  }
};

@Injectable({ providedIn: 'root' })
export class DictionaryService {

  getDecisionDictionary() {
    return decisionDictionary;
  }

  getMeetingDictionary() {
    return meetingDictionary;
  }

  getHallDictionary() {
    return hallDictionary;
  }

  getMeetingTypeDictionary() {
    return meetingTypeDictionary;
  }
}
