import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { map, tap } from 'rxjs/operators';
import { APP_CONFIG, AppConfig } from '../app-config.module';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Employee } from '../model/Employee.model';
import { EventEmiterService } from './eventEmiter.service';
import { Department } from '../model/Department.model';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';


@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  EmployeeData = new BehaviorSubject<Employee>(new Employee(0, "", 0, new Department(0, ""), ""));
  errorMessage: string = "";
  get UserRoles(): string[] {
    let user = localStorage.getItem('currentUser');
    return this.jwtHelper.decodeToken(JSON.stringify(user)).role;
  };
  constructor(
    private router: Router,
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private _eventEmiter: EventEmiterService,
    @Inject(APP_CONFIG) private config: AppConfig) {
    this.GetEmployeeData();
  }

  login(credentials: string) {
    var result;
    let userIdvar: string = "";
    //TO-DO Uncomment till transfer dlls ti aminah
    return this.http.post(`${this.config.apiEndpoint}/Account/Login`, credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    })
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
          userIdvar = this.jwtHelper.decodeToken(JSON.stringify(user)).nameid;
          //this.UserRoles = this.jwtHelper.decodeToken(JSON.stringify(user)).role;
          this.RequestEmployeeData(userIdvar).subscribe(
            (employeeData) => {
              // store Employee data in local storage to keep Employee Inforamtion
              localStorage.setItem('Employee', JSON.stringify(employeeData));
              this.EmployeeData.next(JSON.parse(JSON.stringify(employeeData)));
              this._eventEmiter.emitChange(true);
            },
            (error: any) => this.errorMessage = <any>error
          );

          return false;
        }
        return true;
      }));

  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    localStorage.removeItem('Employee');

  }



  ///Get EmployeeData
  RequestEmployeeData(userId: string) {

    return this.http.get(`${this.config.hrApiEndpoint}/HR/GetEmployeeData?userId=` + userId)
      .pipe(tap()
        //catchError(this.handleError)
      );

  }


  GetEmployeeData() {
    let employee = JSON.parse(localStorage.getItem("Employee"));
    //this.EmployeeData = JSON.parse(employee);
    if (employee)
      this.EmployeeData.next(employee);
    //return this.EmployeeData;
    return employee;
  }

  GetUserData() {
    let user = localStorage.getItem('currentUser');
    return this.jwtHelper.decodeToken(JSON.stringify(user));
  }

  /*************Departments****************/
  public getDepartments() {
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    //console.log(currentUser);
    //, "Cache-Control": "no-cache", "Pragma": "no-cache", "Authorization": `Bearer ${currentUser}`
    return this.http.get<Department[]>(`${this.config.hrApiEndpoint}/HR/GetDepartments`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (departmentsData) => {
        const departments: Department[] = [];
        for (let dept of departmentsData) {
          dept = Department.deserialize(dept);
          departments.push(dept);
        }

        return departments;
      }
    ));

    //return this.departments;
  }
  public getEmployees() {
    return this.http.get<Employee[]>(`${this.config.hrApiEndpoint}/HR/GetEmployees`, {
      headers: new HttpHeaders({ "Content-Type": "application/json" })
    }).pipe(map(
      (employeesData) => {
        const employees: Employee[] = [];
        //console.log(employeesData);
        for (let emp of employeesData) {
          emp = Employee.deserialize(emp);
          employees.push(emp);
        }

        return employees;
      }
    ));

    //return this.departments;
  }

  public IsAuthorized(roles: string[]): boolean {
    let authorized: boolean = false;
    roles.map(
      c => {
        if (this.UserRoles.filter(role => role == c).length > 0) {
          authorized = true;
          return authorized;
        }
      }
    )
    return authorized;
  }

  public IsAuthorizedByDepartmet(roles: string[], targetDeptId: number): boolean {
    let authorized: boolean = false;
    roles.map(
      c => {
        if (this.UserRoles.filter(role => role == c).length > 0) {

          if (this.GetEmployeeData().DepartmentId == targetDeptId) {
            authorized = true;
            return authorized;
          }
        }
      }
    )
    return authorized;
  }

  
}
