import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitbuyandrentComponent } from './submitbuyandrent.component';

describe('SubmitbuyandrentComponent', () => {
  let component: SubmitbuyandrentComponent;
  let fixture: ComponentFixture<SubmitbuyandrentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubmitbuyandrentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubmitbuyandrentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
