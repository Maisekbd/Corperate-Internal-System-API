import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, FormArray, Validators, ValidatorFn } from '@angular/forms';
import { AgendaItem } from "../../model/AgendaItem.model";
import { AgendaDetails } from "../../model/AgendaDetails.model";
import { SharedService } from '../../services/shared.service';
import { locale } from 'devextreme/localization';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { MeetingsService } from '../../services/meetings.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-agenda',
  templateUrl: './add-agenda.component.html',
  styleUrls: ['./add-agenda.component.css']
})
export class AddAgendaComponent implements OnInit {

  public _parentForm: FormGroup;
  uploadURL: string = this.sharedService.serviceUrl + "/Attachment/UploadAttachment";
  currentDate: Date = new Date();
  isFirstOpen: boolean = true;
  closeOthers: boolean = true;
  agendaItemUoloadedFiles: any[] = [];
  detailedUploadedFile: any[] = [];
  agenda: AgendaItem[];
  headers: any;
  RowIndex: number = 0;
  validationRules;
  meetingSubscribtion: Subscription;
  editMood: boolean = false;

  constructor(
    private fb: FormBuilder,
    private sharedService: SharedService,
    private meetingService: MeetingsService) {
    locale(this.sharedService.getCurrentLanguage());
    this.headers = { 'Authorization': 'Bearer ' + JSON.parse(localStorage.getItem('currentUser')) };
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

  ngOnInit() {
    this.setValidationRules();
    this.meetingSubscribtion = this.meetingService.selectedMeetingInfo.subscribe((meeting) => {
      if (meeting.Id) {
        this.editMood = true;
        this.onDeleteAgenda(0);
        this.meetingService.selectedMeeting.AgendaItems.forEach((c, index) => {
          this.addNewAgenda(c);
          c.AgendaDetails.forEach(det => {
            this.addNewDetail(det, index);
          })
        })
      }
      else {
        this.addInitAgenda();
      }
    });
    //if (this.meetingService.selectedMeeting.Id > 0) {
    //  this.meetingService.selectedMeeting.AgendaItems.forEach((c, index) => {
    //    this.addNewAgenda(c);
    //    c.AgendaDetails.forEach(det => {
    //      this.addNewDetail(det, index);
    //    }
    //    )
    //  }
    //  )
    //}
  }

  setValidationRules() {

    //this.translate.get(['meetings.location']).subscribe((res: string) => {
    this.validationRules = {
      //location: [{ type: 'required', message: this.validationService.generatRequiredValidationMessage(res['meetings.location']) }],
      //operationGoals: [{ type: 'required', message: this.validationService.generatRequiredValidationMessage('اهداف العملية') }],
      //operationEvidencesRequired: [{ type: 'required', message: this.validationService.generatSelectOneValidationMessage('دليل للعملية') }],
      //operationCustomersRequired: [{ type: 'required', message: this.validationService.generatSelectOneValidationMessage('متعامل') }],
      //txtHidden: [{ type: 'required', message: 'يرجي اختيار الموظفون.' }],
      //txtAccountEmpValidation: [{ type: 'required', message: 'يرجى إختيار أحد الموظفين كمسؤول عن العملية.' }],
      txtAgendaItemValidation: [{ type: 'required', message: 'يرجى إختيار أحد الموظفين كمسؤول عن العملية.' }],
    };
    //});
  }

  validationCallback(e) {
    return e.value == null;
  }

  private initAgendaList(agenda: AgendaItem) {
    return this.fb.group({
      RowIndex: new FormControl(this.RowIndex),
      Id: new FormControl(agenda.Id),
      MeetingId: new FormControl(agenda.MeetingId),
      AgendaNumber: new FormControl(agenda.AgendaNumber),
      AgendaText: new FormControl(agenda.AgendaText),
      AttachementName: new FormControl(agenda.AttachementName),
      AgendaDetails: this.fb.array([], this.checkDublicateAgendaDetailsNO())
    })
  }


  checkDublicateAgendaDetailsNO() {
    const validator: ValidatorFn = (formArray: FormArray) => {
      const totalSelected = formArray.controls.map(control => (<FormGroup>control).controls["TreeNumber"].value);
      //const names = totalSelected.map(value => value.username)
      if (totalSelected.length > 0) {
        const hasDuplicate = totalSelected.some((name, index) => totalSelected.indexOf(name, index + 1) != -1);
        return hasDuplicate ? { duplicate: true } : null;
      }
    }
    return validator;
  }

  private addNewAgenda(agenda: AgendaItem) {
    this.RowIndex = this.RowIndex + 1;
    agenda.RowIndex = this.RowIndex;
    const agendaFormArraycontrol = <FormArray>this.parentForm.controls['AgendaItems'];
    agendaFormArraycontrol.push(this.initAgendaList(agenda));
    //this.agenda.push((<FormArray>this.parentForm.controls['AgendaItems']).value());
    //this.agenda = [{ , this.parentForm.controls['AgendaItems'].get('AgendaNumber').value}];
    //alert(this.fb.control('AgendaName').value);
    //if (outPutFormArraycontrol.length != 0)
    //  this.parentForm.controls['txtOpOutputsValidation'].setValue("1");
  }

  addInitAgenda() {
    let agendaItem = AgendaItem.getAgendaDefault();
    if (this.meetingService.selectedMeeting)
      agendaItem.MeetingId = this.meetingService.selectedMeeting.Id;
    this.addNewAgenda(agendaItem);
  }

  //private generateAgendaNumber() {
  //  let agendaFormArraycontrol = <FormArray>this.parentForm.controls['AgendaItems'];
  //  if (agendaFormArraycontrol.length > 0)
  //    return (Number((<FormGroup>agendaFormArraycontrol.controls[length-1]).controls["AgendaNumber"].value) + 1).toString();
  //  else
  //    return "1";
  //}

  onDeleteAgenda(agendaIndex: number) {
    if ((<FormArray>this.parentForm.controls['AgendaItems']).length > 0) {
    const actionFormArraycontrol = (<FormArray>this.parentForm.controls['AgendaItems']).at(agendaIndex).get('AgendaDetails') as FormArray;
    if (actionFormArraycontrol) {
      this.clearFormArray(actionFormArraycontrol);
      (<FormArray>this.parentForm.get('AgendaItems')).removeAt(agendaIndex);
      }
    }
  }

  clearFormArray = (formArray: FormArray) => {
    while (formArray.length !== 0) {
      formArray.removeAt(0)
    }
  }


  private initAgendaDetailIndexList(detail: AgendaDetails) {
    return this.fb.group({
      Id: new FormControl(detail.Id),
      AgendaItemId: new FormControl(detail.AgendaItemId),
      DetailRowIndex: new FormControl(this.RowIndex),
      TreeNumber: new FormControl(detail.TreeNumber),
      AttachementName: new FormControl(detail.AttachementName),
      Description: new FormControl(detail.Description)
    })
  }

  private addNewDetail(detail: AgendaDetails, agendaIndex: number) {
    this.RowIndex = this.RowIndex + 1;
    const detailFormArraycontrol = (<FormArray>this.parentForm.controls['AgendaItems']).at(agendaIndex).get('AgendaDetails') as FormArray;
    detailFormArraycontrol.push(this.initAgendaDetailIndexList(detail));
    //if (outPutFormArraycontrol.length != 0)
    //  this.parentForm.controls['txtOpOutputsValidation'].setValue("1");
  }

  addInitDetails(agendaIndex: number) {
    this.addNewDetail(AgendaDetails.getAgendaDetailsDefault(), agendaIndex);
  }
  onDeleteDetails(agendaIndex: number, agendaDetailIndex: number) {
    const detailFormArraycontrol = (<FormArray>this.parentForm.controls['AgendaItems']).at(agendaIndex).get('AgendaDetails') as FormArray;
    detailFormArraycontrol.removeAt(agendaDetailIndex);
  }


  addAgendaParameter(e) {
    this.uploadURL = this.updateQueryStringParameter(this.uploadURL, "agendaNumber", e.component.option("name"));
    this.uploadURL = this.updateQueryStringParameter(this.uploadURL, "meetingNo", this.parentForm.controls['GeneratedMeetingNumber'].value);
    e.component.option("uploadUrl", this.uploadURL);
  }

  onUploadedFile(e) {

    let agendaFormArraycontrol = <FormArray>this.parentForm.controls['AgendaItems'];
    let rowIndex = e.component.option("name");
    for (let formGroupItem of agendaFormArraycontrol.controls) {
      let agendaItem = (<FormGroup>formGroupItem);
      if ((<FormGroup>formGroupItem).controls["RowIndex"].value == e.component.option("name")) {
        (<FormGroup>formGroupItem).controls["AttachementName"].setValue(e.request.responseText);
      }
    }
  }
  //e.component.option("name")

  onUploadedDetailFile(e) {

    let agendaFormArraycontrol = <FormArray>this.parentForm.controls['AgendaItems'];
    let rowIndex = e.component.option("name");
    for (let formGroupItem of agendaFormArraycontrol.controls) {
      let agendaItemDetails = <FormArray>((<FormGroup>formGroupItem).controls["AgendaDetails"]);
      for (let agendaDetail of agendaItemDetails.controls) {
        if ((<FormGroup>agendaDetail).controls["DetailRowIndex"].value == e.component.option("name")) {
          (<FormGroup>agendaDetail).controls["AttachementName"].setValue(e.request.responseText);
        }
      }
      //e.component.option("name")
    }
  }

  updateQueryStringParameter(uri, key, value) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
      return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
      return uri + separator + key + "=" + value;
    }
  }

  viewAgendaAttachmentClick(fileName: string) {
    this.meetingService.previewMeetingAttachment(fileName);
  }
  //expandAll() {
  //  this.closeOthers = false;
  //  this.isFirstOpen = true;
  //}
}
