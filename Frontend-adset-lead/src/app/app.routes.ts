import { Routes } from '@angular/router';
import { WelcomeComponent } from './pages/welcome/welcome.component';
import { CarrosComponent } from './pages/carro-list/carro-list.component';
import { CarroCadastroComponent } from './pages/carro-cadastro/carro-cadastro.component';

export const routes: Routes = [
  { path: '', component: WelcomeComponent },
  { path: 'carros', component: CarrosComponent },
  { path: 'carros/cadastrar', component: CarroCadastroComponent }
];
