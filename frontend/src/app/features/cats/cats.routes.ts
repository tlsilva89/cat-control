import { Routes } from '@angular/router';
import { CatsComponent } from './cats.component';

export const CATS_ROUTES: Routes = [
  {
    path: '',
    component: CatsComponent
  },
  {
    path: 'new',
    loadComponent: () => import('./components/cat-form/cat-form.component').then(m => m.CatFormComponent)
  },
  {
    path: ':id',
    loadComponent: () => import('./components/cat-profile/cat-profile.component').then(m => m.CatProfileComponent)
  },
  {
    path: ':id/edit',
    loadComponent: () => import('./components/cat-form/cat-form.component').then(m => m.CatFormComponent)
  }
];
