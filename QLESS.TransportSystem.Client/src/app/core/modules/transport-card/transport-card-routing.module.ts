import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TransportCardSectionAComponent } from './transport-card-section-a/transport-card-section-a.component';
import { TransportCardSectionBComponent } from './transport-card-section-b/transport-card-section-b.component';
import { TransportCardSectionCComponent } from './transport-card-section-c/transport-card-section-c.component';
import { TransportCardSectionDComponent } from './transport-card-section-d/transport-card-section-d.component';
import { TransportCardComponent } from './transport-card.component';

const routes: Routes = [
  { path: '', component: TransportCardComponent },
  { path: 'section-a', component: TransportCardSectionAComponent },
  { path: 'section-b', component: TransportCardSectionBComponent },
  { path: 'section-c', component: TransportCardSectionCComponent },
  { path: 'section-d', component: TransportCardSectionDComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransportCardRoutingModule { }
