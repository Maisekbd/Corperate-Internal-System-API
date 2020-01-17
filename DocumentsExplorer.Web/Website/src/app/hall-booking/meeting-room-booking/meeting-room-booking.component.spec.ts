import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingRoomBookingComponent } from './meeting-room-booking.component';

describe('MeetingRoomBookingComponent', () => {
  let component: MeetingRoomBookingComponent;
  let fixture: ComponentFixture<MeetingRoomBookingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MeetingRoomBookingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MeetingRoomBookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
