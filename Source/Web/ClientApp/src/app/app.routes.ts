import { Routes } from '@angular/router';

import { Home } from './features/home/pages/home/home';
import { Login } from './features/auth/pages/login/login';
import { Register } from './features/auth/pages/register/register';
import { authGuard } from './core/guards/auth.guard';
import { Private } from './features/private/pages/private/private';

export const routes: Routes = [
  {
    path: '',
    component: Home
  },
  {
    path: 'login',
    component: Login
  },
  {
    path: 'register',
    component: Register
  },
  {
    path: 'private',
    component: Private,
    canActivate: [authGuard]
  },
  {
    path: '**',
    redirectTo: ''
  }
];
