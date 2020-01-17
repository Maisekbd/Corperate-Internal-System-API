import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeesSelectionComponent } from './employees-selection.component';

describe('EmployeesSelectionComponent', () => {
  let component: EmployeesSelectionComponent;
  let fixture: ComponentFixture<EmployeesSelectionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeesSelectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeesSelectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
