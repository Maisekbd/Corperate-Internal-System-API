import { Injectable, Inject } from '@angular/core';
import * as AppConst from "../helpers/string.helper";
import { AppConfig, APP_CONFIG } from '../app-config.module';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';


const txtRequiredMessage = "الرجاء إدخال {0}";
const txtSelectOneMessage = "الرجاء إختيار {0} واحد على الأقل";
const txtMoreThanMessage = "الرجاء إدخال قيمة أكبر من {0} لـ {1}";

@Injectable({ providedIn: 'root' })
export class ValidationService {
   

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private translate: TranslateService) {
    this.GetMessagesList();
    this.translate.setDefaultLang(localStorage.getItem('lang'));
    this.translate.use(localStorage.getItem('lang'))
    this.translate.onLangChange.subscribe((event: LangChangeEvent) => {
      this.GetMessagesList();
    });
  }

  GetMessagesList() {
    //this.translate.get(['filters.securityLevel_Confidential', 'filters.priority_Urgent', 'filters.unread']).subscribe((res: string) => {
    //  //this.filterbtns = [
    //  //  {
    //  //    icon: "key",
    //  //    name: FilterEnum.SecurityLevel_Confidential.toString(),
    //  //    hint: res['filters.securityLevel_Confidential']
    //  //  },
    //  //  {
    //  //    icon: "warning",
    //  //    name: FilterEnum.Priority_Urgent.toString(),
    //  //    hint: res['filters.priority_Urgent']
    //  //  },
    //  //  {
    //  //    icon: "message",
    //  //    name: FilterEnum.Unread.toString(),
    //  //    hint: res['filters.unread']
    //  //  },

    //  ];
    //})
  }


  generatRequiredValidationMessage(field: string): string {
    return AppConst.StringFormat(txtRequiredMessage, field);
  }

  generatSelectOneValidationMessage(field: string): string {
    return AppConst.StringFormat(txtSelectOneMessage, field);
  }

  generatMoreThanValidationMessage(minValue: number, field: string): string {
    return AppConst.StringFormat(txtMoreThanMessage, minValue + '', field);
  }
}
