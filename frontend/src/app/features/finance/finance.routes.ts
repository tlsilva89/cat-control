import { Routes } from '@angular/router';
import { FinanceComponent } from './finance.component';

export const FINANCE_ROUTES: Routes = [
  {
    path: '',
    component: FinanceComponent
  },
  {
    path: 'new',
    loadComponent: () => import('./components/finance-form/finance-form.component').then(m => m.FinanceFormComponent)
  },
  {
    path: ':id/edit',
    loadComponent: () => import('./components/finance-form/finance-form.component').then(m => m.FinanceFormComponent)
  }
];
