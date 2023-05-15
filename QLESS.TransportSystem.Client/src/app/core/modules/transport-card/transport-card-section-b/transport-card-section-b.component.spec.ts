import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransportCardSectionBComponent } from './transport-card-section-b.component';

describe('TransportCardSectionBComponent', () => {
  let component: TransportCardSectionBComponent;
  let fixture: ComponentFixture<TransportCardSectionBComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransportCardSectionBComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TransportCardSectionBComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
