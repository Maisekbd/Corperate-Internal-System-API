import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMinutesMeetingComponent } from './add-minutes-meeting.component';

describe('AddMinutesMeetingComponent', () => {
  let component: AddMinutesMeetingComponent;
  let fixture: ComponentFixture<AddMinutesMeetingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddMinutesMeetingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMinutesMeetingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
