import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransportCardSectionAComponent } from './transport-card-section-a.component';

describe('TransportCardSectionAComponent', () => {
  let component: TransportCardSectionAComponent;
  let fixture: ComponentFixture<TransportCardSectionAComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransportCardSectionAComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TransportCardSectionAComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
