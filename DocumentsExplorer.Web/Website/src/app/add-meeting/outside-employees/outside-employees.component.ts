import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormControl, FormArray, FormGroup, Validators } from '@angular/forms';
import { ValidationService } from '../../services/validation.service';

import { MeetingAttendance } from '../../model/MeetingAttendance.model';

@Component({
  selector: 'app-outside-employees',
  templateUrl: './outside-employees.component.html',
  styleUrls: ['./outside-employees.component.css']
})
export class OutsideEmployeesComponent implements OnInit {

  public _parentForm: FormGroup;
  displayMsg: boolean;

  constructor(private fb: FormBuilder,
    private validationService: ValidationService) { }

  @Input() set parentForm(parentForm: FormGroup) {
    if (parentForm) {
      this._parentForm = parentForm;
    }
  }

  get parentForm(): FormGroup {
    return this._parentForm;
  }

  ngOnInit() {
    this.displayMsg = true
    //this.addNewoutEmployee();
  }
  onDeletingOutput(indexPassed: number) {   
    (<FormArray>this.parentForm.get('MeetingAttendances')).removeAt(indexPassed);       
    //this.modalService.dismissAll();
  }


  private initOutEmp() {
    return this.fb.group({
      Id: new FormControl(0),
      Name: new FormControl(),
      Email: new FormControl(),
      JobDescription: new FormControl(),
      CompanyName: new FormControl()      
    })
  }

  private addNewOutEmployee(emp: MeetingAttendance) {
    this.displayMsg = false;
    const outEmpFormArraycontrol = <FormArray>this.parentForm.controls['MeetingAttendances'];
    outEmpFormArraycontrol.push(this.initOutEmp());
  }



  addNewoutEmployee() {
    this.addNewOutEmployee(MeetingAttendance.getMeetingAttendanceDefault());
  }
  confirmDeleting(content) {
    //if (!this._parentDisable) {
    //  const modalRef = this.modalService.open(content, { windowClass: 'dark-modal', centered: true });

    //}
  }

}
