import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { VehiclesRoutingModule } from './vehicles-routing.module';
import { VehiclesPageComponent } from './pages/vehicles-page/vehicles-page.component';
import { MaterialModule } from '../../shared/material.module';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  declarations: [VehiclesPageComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    VehiclesRoutingModule,
    MaterialModule,
    SharedModule,
  ],
})
export class VehiclesModule {}
