import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadOnlyComponent } from './read-only.component';

describe('ReadOnlyComponent', () => {
  let component: ReadOnlyComponent;
  let fixture: ComponentFixture<ReadOnlyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReadOnlyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReadOnlyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
