import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DecisionsViewComponent } from './decisions-view.component';

describe('DecisionsViewComponent', () => {
  let component: DecisionsViewComponent;
  let fixture: ComponentFixture<DecisionsViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DecisionsViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DecisionsViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
