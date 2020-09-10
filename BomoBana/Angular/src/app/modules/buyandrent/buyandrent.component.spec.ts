import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuyandrentComponent } from './buyandrent.component';

describe('BuyandrentComponent', () => {
  let component: BuyandrentComponent;
  let fixture: ComponentFixture<BuyandrentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuyandrentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuyandrentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
