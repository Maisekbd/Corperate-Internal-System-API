import { Component, OnInit, HostBinding, ElementRef, OnDestroy, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { SharedService } from '../../services/shared.service';

import { AuthenticationService } from '../../services/authentication.service';
import { Employee } from '../../model/Employee.model';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable, Subscription, of, timer } from 'rxjs';
import { DxAutocompleteModule } from 'devextreme-angular';
import { DecisionService } from '../../services/decision.service';
import { SystemRoleEnum } from '../../model/enum/System.enum';
import { NotificationService } from '../../services/notification.service';
import { Notification } from '../../model/Notification.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  showHeader$: Observable<boolean>;
  IsOperationManagmentAdmin: boolean;
  private ShowHeader = new BehaviorSubject<boolean>(false);
  DecisionEditableMode: string[] = [SystemRoleEnum.DecisionEditor];
  MeetingEditableMode: string[] = [SystemRoleEnum.MeetingEditor];
  HallBookingMode: string[] = [SystemRoleEnum.BookingAdmin];
  notificationsdata = new BehaviorSubject<Notification[]>([]);
  notifications$: Observable<Notification[]>;
  get showHeader() {
    return this.ShowHeader.asObservable();
  }
  //@Input() EmployeeData: Employee;
  employeeData: Employee;
  employeeObs: Observable<Employee>;
  notificationsSubscription: Subscription;
  timerSubscription: Subscription;
  private route: ActivatedRoute;
  /**Menu**/
  navbarOpen = false;

  toggleNavbar() {
    this.navbarOpen = !this.navbarOpen;
  }
  /**End of Menu**/
  fullInfo = "";
  surnames: string[];
  lastName = "";
  searchTxt = "";

  searchBtn: any;

  constructor(
    private translate: TranslateService,
    private el: ElementRef,
    public sharedService: SharedService,
    private authService: AuthenticationService,
    private decisionService: DecisionService,
    private notificationService: NotificationService,
    private router: Router) {
    this.searchBtn = {
      text: "",
      stylingMode: "text",
      width: 32,
      elementAttr: {
        class: "icon-searchTxt font"
      },
      onClick: (e) => {
        this.updateDecisionInfo();
      }
    };
  }



  private refreshData(): void {
    this.notificationsSubscription = this.notificationService.getLatestNotifications().subscribe(notifications => {
      this.notificationsdata.next(notifications);
      this.notifications$ = this.notificationsdata.asObservable();
      this.subscribeToData();
    });
  }

  private subscribeToData(): void {
    this.timerSubscription = timer(30 * 60 * 1000).subscribe(() => this.refreshData());
  }

  updateDecisionInfo() {
    var result = "";
    this.decisionService.searchboxTxt.next(this.searchTxt);
    this.router.navigate(['/home/decisions']);
    //result += ((this.lastName || "") + " " + (this.lastName || "")).trim();
    //this.fullInfo = result;
  }

  ngOnInit() {
    this.useLanguage(this.sharedService.getCurrentLanguage());
    this.authService.EmployeeData.subscribe((emp) => {
      this.employeeData = emp;
      if (emp && emp.Id != 0)
        this.refreshData();
    });

  }
  useLanguage(language: string) {
    let prevLang = this.sharedService.getCurrentLanguage();
    this.sharedService.useLanguage(language);
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    };
    if (language == this.sharedService.appArLang) {
      this.el.nativeElement.closest('html').className = "rtl";
      this.sharedService.isRtlEmitter.next(true);
    }
    else {
      this.el.nativeElement.closest('html').classList.remove("rtl");
      this.sharedService.isRtlEmitter.next(false);
    }
    if (prevLang != this.sharedService.getCurrentLanguage())
      parent.document.location.reload();
  }

  signOut() {
    this.authService.logout();
  }
}
