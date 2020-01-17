import { Injectable, Inject, HostBinding, ElementRef } from '@angular/core';
import { APP_CONFIG, AppConfig } from '../app-config.module';
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, Subscription, BehaviorSubject } from "rxjs";
import { map } from 'rxjs/operators';
import { Notification } from '../model/Notification.model';

@Injectable({ providedIn: 'root' })

export class NotificationService {
  serviceUrl = `${this.config.apiEndpoint}`;
  notificationUrl: string = `${this.config.apiEndpoint}/Notification/`;




  constructor(@Inject(APP_CONFIG) private config: AppConfig,
    private httpClient: HttpClient) { }

  public getLatestNotifications() {
    return this.httpClient.get<Notification[]>(this.notificationUrl + `GetLatestNotifications`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map((notificationData) => {
      const notifications: Notification[] = [];
      for (let dec of notificationData) {
        dec = Notification.deserialize(dec);
        notifications.push(dec);
      }
      return notifications;
    })
    )
  }

}
