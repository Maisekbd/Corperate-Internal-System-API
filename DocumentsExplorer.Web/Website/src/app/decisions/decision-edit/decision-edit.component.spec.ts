import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DecisionEditComponent } from './decision-edit.component';

describe('DecisionEditComponent', () => {
  let component: DecisionEditComponent;
  let fixture: ComponentFixture<DecisionEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DecisionEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DecisionEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
