import { Routes } from '@angular/router';
import { WishlistComponent } from './wishlist.component';

export const WISHLIST_ROUTES: Routes = [
  {
    path: '',
    component: WishlistComponent
  },
  {
    path: 'new',
    loadComponent: () => import('./components/wishlist-form/wishlist-form.component').then(m => m.WishlistFormComponent)
  },
  {
    path: ':id/edit',
    loadComponent: () => import('./components/wishlist-form/wishlist-form.component').then(m => m.WishlistFormComponent)
  }
];
