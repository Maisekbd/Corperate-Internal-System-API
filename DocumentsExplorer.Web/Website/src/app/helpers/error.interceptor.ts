import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthenticationService } from '../services/authentication.service';
import { Console } from '@angular/core/src/console';
import { Router } from '@angular/router';


/// Middleware to catch error before throw to view
/// it should inject in main Module to activate it


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private authenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                // auto logout if 401 response returned from api
              //this.router.navigate(["login"]);
              this.router.navigate(['/login']);
                //this.authenticationService.logout();
                //location.reload(true);
            }

            const error = err.error.message || err.statusText;
            console.log(error);
            return throwError(error);
        }))
    }
}
