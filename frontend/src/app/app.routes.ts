import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { CatsComponent } from './features/cats/cats.component';
import { VaccinesComponent } from './features/vaccines/vaccines.component';
import { StockComponent } from './features/stock/stock.component';
import { FinanceComponent } from './features/finance/finance.component';
import { WishlistComponent } from './features/wishlist/wishlist.component';
import { NotificationsComponent } from './features/notifications/notifications.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
  },
  {
    path: 'auth',
    children: [
      {
        path: 'login',
        component: LoginComponent
      },
      {
        path: 'register',
        component: RegisterComponent
      },
      {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: 'dashboard',
    canActivate: [authGuard],
    component: DashboardComponent
  },
  {
    path: 'cats',
    canActivate: [authGuard],
    component: CatsComponent
  },
  {
    path: 'vaccines',
    canActivate: [authGuard],
    component: VaccinesComponent
  },
  {
    path: 'stock',
    canActivate: [authGuard],
    component: StockComponent
  },
  {
    path: 'finance',
    canActivate: [authGuard],
    component: FinanceComponent
  },
  {
    path: 'wishlist',
    canActivate: [authGuard],
    component: WishlistComponent
  },
  {
    path: 'notifications',
    canActivate: [authGuard],
    component: NotificationsComponent
  },
  {
    path: '**',
    redirectTo: '/dashboard'
  }
];
