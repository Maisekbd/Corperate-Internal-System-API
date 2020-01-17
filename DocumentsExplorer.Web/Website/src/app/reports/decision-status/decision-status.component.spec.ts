import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DecisionStatusComponent } from './decision-status.component';

describe('DecisionStatusComponent', () => {
  let component: DecisionStatusComponent;
  let fixture: ComponentFixture<DecisionStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DecisionStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DecisionStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
