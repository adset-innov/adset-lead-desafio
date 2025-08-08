import { Routes } from '@angular/router';
import { WelcomeComponent } from './pages/welcome/welcome.component';
import { CarrosComponent } from './pages/carro-list/carro-list.component';

export const routes: Routes = [
  { path: '', component: WelcomeComponent },
  { path: 'carros', component: CarrosComponent }
];
