import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { VehiclesRoutingModule } from './vehicles-routing.module';
import { VehiclesPageComponent } from './pages/vehicles-page/vehicles-page.component';
import { MaterialModule } from '../../shared/material.module';
import { SharedModule } from '../../shared/shared.module';
import { VehiclesStatsBarComponent } from './components/vehicles-stats-bar/vehicles-stats-bar.component';

@NgModule({
  declarations: [VehiclesPageComponent, VehiclesStatsBarComponent],
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
