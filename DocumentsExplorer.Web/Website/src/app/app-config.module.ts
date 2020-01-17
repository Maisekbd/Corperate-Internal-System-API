import { NgModule, InjectionToken } from '@angular/core';
import { environment } from '../environments/environment';

export let APP_CONFIG = new InjectionToken<AppConfig>('app.config');

export class AppConfig {
  apiEndpoint: string;
  hrApiEndpoint: string;
  hallBookingApiEndpoint: string;
}

export const APP_DI_CONFIG: AppConfig = {
  apiEndpoint: environment.apiEndpoint,
  hrApiEndpoint: environment.hrApiEndpoint,
  hallBookingApiEndpoint: environment.hallBookingApiEndpoint
};

@NgModule({
  providers: [{
    provide: APP_CONFIG,
    useValue: APP_DI_CONFIG
  }]
})
export class AppConfigModule { }
