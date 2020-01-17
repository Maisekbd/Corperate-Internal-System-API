import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectAgendaItemComponent } from './select-agenda-item.component';

describe('SelectAgendaItemComponent', () => {
  let component: SelectAgendaItemComponent;
  let fixture: ComponentFixture<SelectAgendaItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectAgendaItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectAgendaItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
