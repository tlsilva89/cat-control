import { Routes } from '@angular/router';
import { StockComponent } from './stock.component';

export const STOCK_ROUTES: Routes = [
  {
    path: '',
    component: StockComponent
  },
  {
    path: 'new',
    loadComponent: () => import('./components/stock-form/stock-form.component').then(m => m.StockFormComponent)
  },
  {
    path: ':id/edit',
    loadComponent: () => import('./components/stock-form/stock-form.component').then(m => m.StockFormComponent)
  }
];
