import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from './authentication.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private jwtHelper: JwtHelperService, private router: Router, private authService: AuthenticationService) {
    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        //let canActivate: boolean = false;
        var token = localStorage.getItem("currentUser");

        if (token && !this.jwtHelper.isTokenExpired(token)) {
            //console.log(this.jwtHelper.decodeToken(token));
            if (this.authService.GetUserData()) {
                // check if route is restricted by role
                if (route.data.roles) {
                    let canActivate = false;
                    route.data.roles.map(c => {
                        if (this.authService.UserRoles.indexOf(c) != -1) {
                            canActivate = true;
                        }
                    })
                    if (canActivate)
                        return true;
                    else {
                        this.router.navigate(['unauth']);
                        return false;
                    }
                }

                // authorised so return true
                return true;
            }
            this.router.navigate(["unauth"]);
            return false;
        }




        this.router.navigate(["login"]);
        return false;
    }
}
