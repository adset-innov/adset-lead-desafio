import { Routes } from '@angular/router';
import { CarrosComponent } from './pages/carro-list/carro-list.component';
import { CarroCadastroComponent } from './pages/carro-cadastro/carro-cadastro.component';
import { CarroEdicaoComponent } from './pages/carro-edicao/carro-edicao.component';
import { WelcomeComponent } from './pages/welcome/welcome.component';

export const routes: Routes = [
  { path: '', component: WelcomeComponent },
  { path: 'carros', component: CarrosComponent },
  { path: 'carros/cadastrar', component: CarroCadastroComponent },
  { path: 'carros/editar/:id', component: CarroEdicaoComponent },
];
