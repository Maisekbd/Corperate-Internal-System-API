import { Component, OnInit, Input, OnDestroy, ViewChild } from '@angular/core';
import { Injectable, Inject, HostBinding, ElementRef } from '@angular/core';
import { FormGroup, FormArray, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Decision } from '../../model/decision.model';
import { AgendaItem } from '../../model/AgendaItem.model';
import { Meeting } from '../../model/Meetings.model';
import { MeetingsService } from '../../services/meetings.service';
import { MatStepper, MatDialog } from '@angular/material';
import { LookupsService } from '../../services/lookups.service';
import { ReferenceType } from '../../model/ReferenceType.model';
import { ReferenceItem } from '../../model/ReferenceItem.model';
import { Observable, Subject } from 'rxjs';
import { DxAutocompleteModule } from 'devextreme-angular';
import { MainCategory } from '../../model/MainCategory.model'
import { SubCategory } from '../../model/SubCategory.model';
import { Company } from '../../model/Company.model';
import { Department } from '../../model/Department.model';
import { AuthenticationService } from '../../services/authentication.service';
import { EnumModel } from '../../model/Enum.model';
import { EnumServiceList } from '../../services/enumList.service';
import { DecisionType } from '../../model/DecisionType.model';
import { DecisionService } from '../../services/decision.service';
import { SharedService } from '../../services/shared.service';
import { takeUntil } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';
import { DecisionExecution } from '../../model/DecisionExecution.model';
import { ConfirmationDialogComponent } from '../../share/confirmation-dialog/confirmation-dialog.component'
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';


@Component({
  selector: 'app-reference-and-decision',
  templateUrl: './reference-and-decision.component.html',
  styleUrls: ['./reference-and-decision.component.css']
})
export class ReferenceAndDecisionComponent implements OnInit, OnDestroy {
  destroy$: Subject<boolean> = new Subject<boolean>();
  uploadURL: string = this.sharedService.serviceUrl + "/Attachment/UploadAttachment";
  uploadWithParameterURL: string = this.sharedService.serviceUrl + "/Attachment/UploadAttachmentWithParameter";
  headers: any;
  decisionfilePattern: any = "\b[1-9]{1,3}_[0-9]{4}\b";
  public meeting: Meeting;
  private AgendaItem: AgendaItem;
  private AgendaList: AgendaItem[];
  private referenceType$: Observable<ReferenceType[]>;
  public mainCatList$: Observable<MainCategory[]>;
  public subCatList$: Observable<SubCategory[]>;
  public decisionTypes$: Observable<DecisionType[]>;
  public isExecutable: boolean = false;
  refernceDecisionList: string[] = [];
  private actions: EnumModel[];
  decisionsDataSource: any;
  @Input() stepperId: MatStepper = null;
  public companies$: Observable<Company[]>;
  public departments$: Observable<Department[]>;
  formDecision: FormGroup;
  decisionText: string = "";
  config: any; richTxtLang: string;
  rtlEnabled: boolean = true;

  editMood: boolean = false;
  decision: Decision = null;
  AgendaItemId: number = 0;
  AgendaDetailId: number = 0;
  AgendaItemtxt: string;
  AgendaDetailtxt: string;
  agendaItemSub: any;
  detailItemSub: any;
  lastName = ""; searchBtn: any; fullInfo = "";
  mId: number = 0;
  RowIndex: number = 0;
  @Input() set meetingParent(meetingParent: Meeting) {
    if (meetingParent) {
      this.meeting = meetingParent;
      if (this.route.snapshot.params["agendaItem"]) {
        this.AgendaItemId = this.route.snapshot.params["agendaItem"]
        // console.log(this.meeting.AgendaItems[this.route.snapshot.params["agendaItem"]].AgendaText);
        if (this.formDecision)
          this.formDecision.patchValue({ AgendaItemId: this.AgendaItemId });
        //this.AgendaItemId = id;
        if (this.AgendaItemId != 0) {
          let agendaItem = this.meeting.AgendaItems.filter(f => f.Id == this.AgendaItemId).shift();
          console.log(agendaItem.AgendaText);
          this.AgendaItemtxt = agendaItem.AgendaNumber + " : " + agendaItem.AgendaText;
          if (this.route.snapshot.params["agendaDetail"]) {
            this.AgendaDetailId = this.route.snapshot.params["agendaDetail"];
            if (this.formDecision)
              this.formDecision.patchValue({ AgendaDetailId: this.AgendaDetailId });
            if (this.AgendaDetailId != 0) {
              let agendaItemDetail = this.meeting.AgendaItems.filter(f => f.Id == this.AgendaItemId).shift().AgendaDetails.filter(d => d.Id == this.AgendaDetailId).shift();
              this.AgendaDetailtxt = agendaItem.AgendaNumber + "." + agendaItemDetail.TreeNumber + " : " + agendaItemDetail.Description;
            }
          }
        }
      }
      if (this.meeting)
        this.formDecision.patchValue({ ConferenceYear: new Date(this.meeting.MeetingDate).getFullYear() });
      if (this.meeting.CouncilType != null) {
        this.mainCatList$ = this.lookupServ.getMainCategories(this.meeting.CouncilTypeId);
        this.decisionTypes$ = this.lookupServ.getDecisionTypes();
      }


    }
  }
  get meetingParent(): Meeting {
    return this.meeting;
  }

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private meetingServ: MeetingsService,
    private fb: FormBuilder,
    private sharedService: SharedService,
    private lookupServ: LookupsService,
    private authServ: AuthenticationService,
    private decisionServ: DecisionService,
    private enumServlst: EnumServiceList,
    private translate: TranslateService,
    private dialog: MatDialog)
  {
    this.richTxtLang = this.sharedService.getCurrentLanguage();
    if (this.richTxtLang == this.sharedService.appArLang) {
      this.richTxtLang = "ar-AR";
      this.rtlEnabled = true;
    }
    else {
      this.rtlEnabled = false;
    }
    this.config = {
      height: '200px',
      uploadImagePath: '/api/upload',
      toolbar: [
        ['misc', ['undo', 'redo']],
        ['font', ['bold', 'italic', 'underline', 'strikethrough', 'superscript', 'subscript', 'clear']],
        ['fontsize', ['fontname']],
        ['para', ['style0', 'ul', 'ol', 'paragraph', 'height']],
        ['insert', ['table', 'link', 'hr']]
      ], lang: this.richTxtLang,
      fontNames: ['Arial', 'Arial Black', 'Comic Sans MS', 'Courier New', 'Cairo', 'CairoSemiBold', "Segoe UI", 'Roboto', "Helvetica Neue", 'Arial', "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol",],

      callbacks: {
        onChange: function (contents, $editable) {
          // this.decisionText = contents;
        }
      }

    };


    this.searchBtn = {
      text: "",
      stylingMode: "text",
      width: 32,
      elementAttr: {
        class: "icon-searchTxt font"
      },
      onClick: (e) => {

      }
    };
  }

  ngOnInit() {
    
    this.decisionsDataSource = this.sharedService.CreateddlDataSourse(this.sharedService.DecisionsDSUrl);
    this.referenceType$ = this.lookupServ.getReferences();
    this.decisionTypes$ = this.lookupServ.getDecisionTypes();

    this.formDecision = this.fb.group({
      Id: new FormControl(0),
      ConferenceYear: new FormControl(),
      DecisionNumber: new FormControl(),
      ConferenceIndex: new FormControl(),
      CouncilType: new FormControl(),
      ConferenceNumber: new FormControl(),
      ConferenceLocation: new FormControl(),
      ConferenceDate: new FormControl(),
      AgendaItem: new FormControl(),
      AgendaItemId: new FormControl(),
      AgendaDetailId: new FormControl(),
      ReferenceItems: this.fb.array([]),
      DecisionText: new FormControl(),
      RequiredDecisionText: new FormControl(),
      MainCategoryId: new FormControl(),
      SubCategoryId: new FormControl(),
      DecisionTypeId: new FormControl(),
      SelectedCompaniesIds: new FormControl(),
      Subject: new FormControl(),
      html: new FormControl(),
      KeyWords: new FormControl(),
      DecisionPath: new FormControl([]),
      DecisionExecutions: this.fb.array([]),
      ExecutionDate: new FormControl(),
      IsExecutable: new FormControl(this.isExecutable)
    });

    if (this.route.snapshot.params["Id"]) {
      this.decision = Decision.getDecisionDefault();
      this.formDecision.patchValue({ ConferenceYear: this.decision.ConferenceYear ? this.decision.ConferenceYear : new Date().getFullYear() });
      this.decision.Id = Number(this.route.snapshot.params["Id"]);
      this.decisionServ.getDecisionById(Number(this.route.snapshot.params["Id"]))
        .pipe(
          takeUntil(this.destroy$))
        .subscribe((decisionData) => {

          this.decisionServ.SelectedDecision$.next(Decision.deserialize(decisionData));
          this.decision = decisionData;
          this.meetingServ.getMeetingById(this.decision.AgendaItem.MeetingId).pipe(
            takeUntil(this.destroy$))
            .subscribe((meetingData) => {
              this.meeting = Meeting.deserialize(meetingData);
              this.subCatList$ = this.lookupServ.getSubCategories(this.decision.MainCategoryId);
              this.refreshView();
              this.editMood = true;
            });
          
        });
     

    }

    this.agendaItemSub = this.meetingServ.agendaItemSelectedForDecisionId$.subscribe(id => {
      if (this.formDecision)
        this.formDecision.patchValue({ AgendaItemId: id });
      this.AgendaItemId = id;
      if (this.AgendaItemId != 0) {
        let agendaItem = this.meeting.AgendaItems.filter(f => f.Id == this.AgendaItemId).shift();
        this.AgendaItemtxt = agendaItem.AgendaNumber + " : " + agendaItem.AgendaText;
        this.detailItemSub = this.meetingServ.agendaDetailSelectedForDecisionId$.subscribe(decisionId => {
          this.AgendaDetailId = decisionId;
          if (this.formDecision)
            this.formDecision.patchValue({ AgendaDetailId: this.AgendaDetailId });
          if (this.AgendaDetailId != 0) {
            let agendaItemDetail = this.meeting.AgendaItems.filter(f => f.Id == this.AgendaItemId).shift().AgendaDetails.filter(d => d.Id == this.AgendaDetailId).shift();
            this.AgendaDetailtxt = agendaItem.AgendaNumber + "." + agendaItemDetail.TreeNumber + " : " + agendaItemDetail.Description;
          }
        });
      }
    });


    this.headers = { 'Authorization': 'Bearer ' + JSON.parse(localStorage.getItem('currentUser')) };
    this.lookupServ.getReferences().pipe(takeUntil(this.destroy$)).subscribe(
      (optData) => {
        this.refernceDecisionList = optData.filter(c => c.IsReferenceDecision).map(c => c.Id.toString());
      })

    if (this.decision && this.decision.Id == 0)
      this.addNewExecutor(DecisionExecution.getDefault());
    this.companies$ = this.lookupServ.getCompanies();
    this.departments$ = this.authServ.getDepartments();
    this.actions = this.enumServlst.ActionTypeEnum;
    this.checkIfExecutableWithRequired = this.checkIfExecutableWithRequired.bind(this);
  }

  refreshView() {

    this.meetingServ.agendaItemSelectedForDecisionId$.next(this.decision.AgendaItemId);
    if (this.decision.AgendaDetailId && this.decision.AgendaDetailId != 0)
      this.meetingServ.agendaDetailSelectedForDecisionId$.next(this.decision.AgendaDetailId)
    this.mainCatList$ = this.lookupServ.getMainCategories(this.meeting.CouncilTypeId);
    this.formDecision.patchValue({
      Id: this.decision.Id,
      ConferenceYear: this.decision.ConferenceYear,
      DecisionNumber: this.decision.DecisionNumber,
      Subject: this.decision.Subject,
      MainCategoryId: this.decision.MainCategoryId,
      SubCategoryId: this.decision.SubCategoryId,
      DecisionTypeId: this.decision.DecisionTypeId,
      DecisionText: this.decision.DecisionText,
      SelectedCompaniesIds: this.decision.SelectedCompaniesIds,
      KeyWords: this.decision.KeyWords,
      DecisionPath: this.decision.DecisionPath,
      ExecutionDate: this.decision.ExecutionDate,
    });
    this.isExecutable = this.decision.IsExecutable;
    this.formDecision.controls['IsExecutable'].setValue(this.isExecutable);

    this.decision.ReferenceItems.map(c => {
      this.addNewReference(c);
    });

    this.decision.DecisionExecutions.map(c => {
      this.addNewExecutor(c);
    });
  }

  private initReferenceItem(ref: ReferenceItem) {
    return this.fb.group({
      RowIndex: new FormControl(this.RowIndex),
      Id: new FormControl(ref.Id),
      Name: new FormControl(ref.Name),
      RefereceItemNo: new FormControl(ref.RefereceItemNo),
      Path: new FormControl(ref.Path),
      Description: new FormControl(ref.Description),
      ReferenceTypeId: new FormControl(ref.ReferenceTypeId.toString()),
      ReferenceDecisionId: new FormControl(ref.ReferenceDecisionId ? ref.ReferenceDecisionId : null)
    })
  }

  private addNewReference(ref: ReferenceItem) {
    this.RowIndex = this.RowIndex + 1;
    ref.RefereceItemNo = this.RowIndex;
    const RefArraycontrol = <FormArray>this.formDecision.controls['ReferenceItems'];
    RefArraycontrol.push(this.initReferenceItem(ref));
  }

  addInitReference() {
    this.addNewReference(ReferenceItem.getDefault());
  }

  onDeleteReference(ActionIndex: number) {
    (<FormArray>this.formDecision.get('ReferenceItems')).removeAt(ActionIndex);
  }
  toggleVisibility(e) {
    this.isExecutable = e.target.checked;
    this.addInitExecutor();
    this.formDecision.controls['IsExecutable'].setValue(this.isExecutable);
  }

  private initExecutor(executor: DecisionExecution) {
    return this.fb.group({
      Id: new FormControl(executor.Id),
      DepartmentId: new FormControl(executor.DepartmentId),
      ActionType: new FormControl(executor.ActionType)
    })
  }

  private addNewExecutor(executor: DecisionExecution) {
    const ExecutorsArraycontrol = <FormArray>this.formDecision.controls['DecisionExecutions'];
    ExecutorsArraycontrol.push(this.initExecutor(executor));
  }

  private addInitExecutor() {
    this.addNewExecutor(DecisionExecution.getDefault());
  }


  onDeleteAction(ActionIndex: number) {
    (<FormArray>this.formDecision.get('DecisionExecutions')).removeAt(ActionIndex);
  }

  onMainCatValueChanged(e) {
    let value = e.value;
    this.subCatList$ = this.lookupServ.getSubCategories(value);
  }


  updateDecisionInfo() {
    var result = "";
    result += ((this.lastName || "") + " " + (this.lastName || "")).trim();
    this.fullInfo = result;
  }



  OnBackStep() {
    this.router.navigate(['/home/AddDecision', this.meeting.Id, 1]);

  }

  checkIfExecutableWithRequired(e) {
    if (this.isExecutable && e.value) {
      return true;
    }
  }


  onSubmit(e, valGroup) {
    if (!this.formDecision.controls['DecisionText'].value) {
      this.formDecision.controls['RequiredDecisionText'].setValue(null);
    }
    else {
      this.formDecision.controls['RequiredDecisionText'].setValue("1");
    }
    if (!this.formDecision.controls['IsExecutable'].value) {
      let executers = (<FormArray>this.formDecision.get('DecisionExecutions'));
      (<FormArray>this.formDecision.get('DecisionExecutions')).removeAt(0);
    }
    if (valGroup.instance.validate().isValid) {
      const confirmMsg = this.translate.instant('messages.decisionConfirmAdding');
      const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
        width: '350px',
        data: confirmMsg
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          if (this.decision == null || this.decision.Id == 0) {
            this.decisionServ.postDecision(this.formDecision.getRawValue()).subscribe(
              OPObject => {
                //notify({ message: 'لقد تم حفظ العملية بنجاح ', position: 'top', width: 300, rtlEnabled: true, show: { type: 'fade', duration: 400, from: 0, to: 1 } }, 'success', 3000)
                //console.log(this.formMeeting.getRawValue());
                //this.id = OPObject.Id;
                this.router.navigate(['/home/decisions']);
              });
          }
          else {
            this.decisionServ.putDecision(this.formDecision.getRawValue()).subscribe(
              OPObject => {
                //notify({ message: 'لقد تم حفظ العملية بنجاح ', position: 'top', width: 300, rtlEnabled: true, show: { type: 'fade', duration: 400, from: 0, to: 1 } }, 'success', 3000)
                //console.log(this.formMeeting.getRawValue());
                //this.id = OPObject.Id;
                this.router.navigate(['/home/decisions']);
              });
          }
        }
      })
    }
  }

  onUploadedFile(e) {
    this.formDecision.controls['DecisionNumber'].setValue(Number(e.request.responseText.replace(/['"]+/g, '').split("_", 1)));
    this.formDecision.controls["DecisionPath"].setValue(e.request.responseText);
  }

  onUploadedReferenceItemFile(e) {

    let agendaFormArraycontrol = <FormArray>this.formDecision.controls['ReferenceItems'];
    let rowIndex = e.component.option("name");
    for (let formGroupItem of agendaFormArraycontrol.controls) {
      let agendaItem = (<FormGroup>formGroupItem);
      if ((<FormGroup>formGroupItem).controls["RowIndex"].value == e.component.option("name")) {
        (<FormGroup>formGroupItem).controls["Path"].setValue(e.request.responseText);
      }
    }
  }

  addReferenceItemParameter(e) {
    this.uploadWithParameterURL = this.sharedService.updateQueryStringParameter(this.uploadWithParameterURL, "attachmentType", 2);
    this.uploadWithParameterURL = this.sharedService.updateQueryStringParameter(this.uploadWithParameterURL, "itemNumber", this.generateTempRefItemNumber());
    e.component.option("uploadUrl", this.uploadWithParameterURL);
  }

  private generateTempRefItemNumber() {
    var currentdate = new Date();
    return currentdate.getFullYear() + "_" + currentdate.getMonth() + "_" + currentdate.getDay() + "_" + currentdate.getHours() + "_" + currentdate.getMinutes() + "_" + currentdate.getSeconds();
  }

  ngOnDestroy() {
    if (this.agendaItemSub)
      this.agendaItemSub.unsubscribe();
    if (this.detailItemSub)
      this.detailItemSub.unsubscribe();
  }


}
