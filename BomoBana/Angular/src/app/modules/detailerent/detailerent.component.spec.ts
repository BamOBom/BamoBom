import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailerentComponent } from './detailerent.component';

describe('DetailerentComponent', () => {
  let component: DetailerentComponent;
  let fixture: ComponentFixture<DetailerentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailerentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailerentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
