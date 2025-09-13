import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VehiclesPageComponent } from './pages/vehicles-page/vehicles-page.component';

const routes: Routes = [{ path: '', component: VehiclesPageComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class VehiclesRoutingModule {}
