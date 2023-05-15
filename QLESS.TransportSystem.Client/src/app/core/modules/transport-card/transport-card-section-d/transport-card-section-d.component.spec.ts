import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransportCardSectionDComponent } from './transport-card-section-d.component';

describe('TransportCardSectionDComponent', () => {
  let component: TransportCardSectionDComponent;
  let fixture: ComponentFixture<TransportCardSectionDComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransportCardSectionDComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TransportCardSectionDComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
