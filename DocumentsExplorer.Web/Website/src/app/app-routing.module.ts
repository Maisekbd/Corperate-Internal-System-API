import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';

import { AuthGuard } from './services/auth-guard.service';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { LoginLayoutComponent } from './layouts/login-layout/login-layout.component';
import { SystemRoleEnum } from './model/enum/System.enum';
import { UnAuthorizedComponent } from './share/unauthroized/unauthroized.component';


export const routes: Routes = [
  
  {
    path: '',
    component: LoginLayoutComponent,
    children: [
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'unauth', component: UnAuthorizedComponent, data: { title: 'unauth' } },
    ]
  },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: 'home', loadChildren: './share/home/home.module#HomeModule', canActivate: [AuthGuard] },
      { path: 'booking', loadChildren: './hall-booking/hall-booking.module#HallBookingModule', canActivate: [AuthGuard] },
      { path: 'admin', loadChildren: './administration/administration.module#AdministrationModule', canActivate: [AuthGuard] },
    ]
  },
  { path: '**', redirectTo: '' },
];


@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
