import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingRequestPopupComponent } from './booking-request-popup.component';

describe('BookingRequestPopupComponent', () => {
  let component: BookingRequestPopupComponent;
  let fixture: ComponentFixture<BookingRequestPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookingRequestPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookingRequestPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
