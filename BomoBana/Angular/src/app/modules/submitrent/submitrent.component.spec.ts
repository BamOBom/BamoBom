import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitrentComponent } from './submitrent.component';

describe('SubmitrentComponent', () => {
  let component: SubmitrentComponent;
  let fixture: ComponentFixture<SubmitrentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubmitrentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmitrentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
