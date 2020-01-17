import { Component, OnInit, HostBinding, ElementRef} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  locale: string;
  @HostBinding('attr.dir') dir;

  constructor(private translate: TranslateService, private el: ElementRef) {
  }

  ngOnInit() {
    this.translate.setDefaultLang('ar');
    this.locale = 'ar';
  }
 
}
