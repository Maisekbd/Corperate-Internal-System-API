import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferenceAndDecisionComponent } from './reference-and-decision.component';

describe('ReferenceAndDecisionComponent', () => {
  let component: ReferenceAndDecisionComponent;
  let fixture: ComponentFixture<ReferenceAndDecisionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReferenceAndDecisionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferenceAndDecisionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
