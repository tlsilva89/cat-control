import { Routes } from '@angular/router';
import { VaccinesComponent } from './vaccines.component';

export const VACCINES_ROUTES: Routes = [
  {
    path: '',
    component: VaccinesComponent
  },
  {
    path: 'new',
    loadComponent: () => import('./components/vaccine-form/vaccine-form.component').then(m => m.VaccineFormComponent)
  },
  {
    path: ':id/edit',
    loadComponent: () => import('./components/vaccine-form/vaccine-form.component').then(m => m.VaccineFormComponent)
  }
];
