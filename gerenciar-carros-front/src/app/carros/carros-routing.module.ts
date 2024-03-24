import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CadastrarComponent } from './cadastrar/cadastrar.component';

const routes: Routes = [
  { path: '', redirectTo: 'cadastrar', pathMatch: 'full' }, 
  { path: 'cadastrar', component: CadastrarComponent },
  { path: 'cadastrar/:id', component: CadastrarComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CarrosRoutingModule { }
