import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { VehiclesRoutingModule } from './vehicles-routing.module';
import { VehiclesPageComponent } from './pages/vehicles-page/vehicles-page.component';

@NgModule({
  declarations: [VehiclesPageComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    VehiclesRoutingModule,
  ],
})
export class VehiclesModule {}
