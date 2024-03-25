import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { 
        path: 'carros', 
        loadChildren: () => import('./carros/carros.module'). then(m=> m.CarrosModule), 
      },
    { path: '**', component: PageNotFoundComponent },
 ];