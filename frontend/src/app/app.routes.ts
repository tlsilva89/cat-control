import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.routes').then(m => m.AUTH_ROUTES)
  },
  {
    path: 'dashboard',
    canActivate: [authGuard],
    loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
  },
  {
    path: 'cats',
    canActivate: [authGuard],
    loadChildren: () => import('./features/cats/cats.routes').then(m => m.CATS_ROUTES)
  },
  {
    path: 'vaccines',
    canActivate: [authGuard],
    loadChildren: () => import('./features/vaccines/vaccines.routes').then(m => m.VACCINES_ROUTES)
  },
  {
    path: 'stock',
    canActivate: [authGuard],
    loadChildren: () => import('./features/stock/stock.routes').then(m => m.STOCK_ROUTES)
  },
  {
    path: 'finance',
    canActivate: [authGuard],
    loadChildren: () => import('./features/finance/finance.routes').then(m => m.FINANCE_ROUTES)
  },
  {
    path: 'wishlist',
    canActivate: [authGuard],
    loadChildren: () => import('./features/wishlist/wishlist.routes').then(m => m.WISHLIST_ROUTES)
  },
  {
    path: 'notifications',
    canActivate: [authGuard],
    loadChildren: () => import('./features/notifications/notifications.routes').then(m => m.NOTIFICATIONS_ROUTES)
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];
