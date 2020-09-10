import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailebuyandrentComponent } from './detailebuyandrent.component';

describe('DetailebuyandrentComponent', () => {
  let component: DetailebuyandrentComponent;
  let fixture: ComponentFixture<DetailebuyandrentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailebuyandrentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailebuyandrentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
