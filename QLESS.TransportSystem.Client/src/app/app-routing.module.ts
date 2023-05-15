import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { TransportCardComponent } from './core/modules/transport-card/transport-card.component';

const routes: Routes = [
  { 
    path: '',
    pathMatch: 'full',
    component: TransportCardComponent
  },
  {
    path: 'transport-card', 
    loadChildren: () => import('./core/modules/transport-card/transport-card.module').then(m => m.TransportCardModule)
  }]
  

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
