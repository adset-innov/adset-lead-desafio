import { Component, OnInit } from '@angular/core';
import { VehiclesPageViewModel } from '../../viewmodels/vehicles-page.viewmodel';

@Component({
  selector: 'app-vehicles-page',
  templateUrl: './vehicles-page.component.html',
  styleUrls: ['./vehicles-page.component.scss'],
  providers: [VehiclesPageViewModel],
})
export class VehiclesPageComponent implements OnInit {
  constructor(public vm: VehiclesPageViewModel) {}

  ngOnInit(): void {
    this.vm.loadVehicles();
    this.vm.loadCounts();
    this.vm.loadColors();
  }

  onCreateVehicle(): void {
    console.log('Create vehicle clicked');
  }

  onSave(): void {
    console.log('Save clicked');
  }

  onExport(type: 'excel' | 'api'): void {
    console.log('Export type:', type);
  }

  onEditVehicle(id: string): void {
    console.log('Edit vehicle:', id);
  }
}
