import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DecisionReadOnlyComponent } from './decision-read-only.component';

describe('DecisionReadOnlyComponent', () => {
  let component: DecisionReadOnlyComponent;
  let fixture: ComponentFixture<DecisionReadOnlyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DecisionReadOnlyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DecisionReadOnlyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
