import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingTypeListComponent } from './meeting-type-list.component';

describe('MeetingTypeListComponent', () => {
  let component: MeetingTypeListComponent;
  let fixture: ComponentFixture<MeetingTypeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MeetingTypeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MeetingTypeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
