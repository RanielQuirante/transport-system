import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransportCardSectionCComponent } from './transport-card-section-c.component';

describe('TransportCardSectionCComponent', () => {
  let component: TransportCardSectionCComponent;
  let fixture: ComponentFixture<TransportCardSectionCComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransportCardSectionCComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TransportCardSectionCComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
