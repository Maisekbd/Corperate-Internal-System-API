import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, FormControl, Validators } from '@angular/forms';
import { ValidationService } from '../../services/validation.service';
import { Meeting } from '../../model/Meetings.model';
import { AuthenticationService } from '../../services/authentication.service';
import { SharedService } from '../../services/shared.service';
import { Employee } from '../../model/Employee.model';
import { MeetingsService } from '../../services/meetings.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-employees-selection',
  templateUrl: './employees-selection.component.html',
  styleUrls: ['./employees-selection.component.css']
})
export class EmployeesSelectionComponent implements OnInit, OnDestroy {
  public _parentForm: FormGroup;
  validationRules;
  employeesList: any;
  private _selectedItems: Employee[] = [];

  constructor(
    private fb: FormBuilder,
    private validationMsg: ValidationService,
    private meetingsService : MeetingsService,
    private hrService: AuthenticationService,
    private sharedService: SharedService) {
    this.employeesList = this.sharedService.CreateHRDataSourse("/HR/GetDSEmployees");
  }

  /*ParentForm*/
  @Input() set parentForm(parentForm: FormGroup) {
    if (parentForm) {
      this._parentForm = parentForm;

    }
  }
  get parentForm(): FormGroup {
    
    return this._parentForm;
  }

  @Input() set selectedItems(selectedItems: Employee[]) {
    if (selectedItems) {
      this._selectedItems = selectedItems;

    }
  }
  get selectedItems(): Employee[] {

    return this._selectedItems;
  }

  ngOnInit() { }



  private initEmployeeList(employee: Employee) {
    return this.fb.group({
      Id: new FormControl(employee.Id),
      Name: new FormControl(employee.Name),
      DepartmentId: new FormControl(employee.DepartmentId),
      DepartmentName: new FormControl(employee.DepartmentName)
    })
  }

  private addNewEmployee(employee: Employee) {
    const employeeFormArraycontrol = <FormArray>this.parentForm.controls['SelectedEmployees'];
    employeeFormArraycontrol.push(this.initEmployeeList(employee));

    //if (outPutFormArraycontrol.length != 0)
    //  this.parentForm.controls['txtOpOutputsValidation'].setValue("1");
  }



  addInitEmployee() {
    this.addNewEmployee(new Employee(0, "", 0,null,""));
  }

  
  onSelectionChanged(e) {
    if (this.selectedItems.length > 0) {
      e.addedItems.map(c => {
        this.addNewEmployee(new Employee(c.Id, c.Name, c.DepartmentId, c.Department, c.Department.Name));
        
      })
    }
    e.removedItems.map(c => {
      const Empcontrol = <FormArray>this.parentForm.controls['SelectedEmployees'];
      Empcontrol.controls.map((e, index) => {
        if (<number>(e as FormGroup).controls['Id'].value == c.Id)
          Empcontrol.removeAt(index);
      })
    })
    
  }

  onRemoveEmp(i:number) {
    const Empcontrol = <FormArray>this.parentForm.controls['SelectedEmployees'];
    Empcontrol.removeAt(i);
  }

  ngOnDestroy() {
    
  }
}
