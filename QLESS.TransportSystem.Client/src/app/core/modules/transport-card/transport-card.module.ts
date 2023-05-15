import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransportCardRoutingModule } from './transport-card-routing.module';
import { TransportCardComponent } from './transport-card.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatMenuModule } from '@angular/material/menu';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatInputModule } from '@angular/material/input';
import { TransportCardSectionAComponent } from './transport-card-section-a/transport-card-section-a.component';
import { TransportCardSectionBComponent } from './transport-card-section-b/transport-card-section-b.component';
import { TransportCardSectionCComponent } from './transport-card-section-c/transport-card-section-c.component';
import { TransportCardSectionDComponent } from './transport-card-section-d/transport-card-section-d.component';

@NgModule({
  declarations: [
    TransportCardComponent,
    TransportCardSectionAComponent,
    TransportCardSectionBComponent,
    TransportCardSectionCComponent,
    TransportCardSectionDComponent
  ],
  imports: [
    CommonModule,
    TransportCardRoutingModule,
    ReactiveFormsModule,
    MatPaginatorModule, 
    MatMenuModule,
    NgbModule,
    MatInputModule
  ]
})
export class TransportCardModule { }
