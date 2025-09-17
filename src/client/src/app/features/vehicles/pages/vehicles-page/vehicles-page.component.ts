import { Component, OnInit } from '@angular/core';
import { VehiclesPageViewModel } from '../../viewmodels/vehicles-page.viewmodel';
import { MatDialog } from '@angular/material/dialog';
import { CreateVehicleModalComponent } from '../../components/create-vehicle-modal/create-vehicle-modal.component';
import { EditVehicleModalComponent } from '../../components/edit-vehicle-modal/edit-vehicle-modal.component';
import { VehiclePhotosModalComponent } from '../../components/vehicle-photos-modal/vehicle-photos-modal.component';
import { Vehicle } from '../../../../core/models/vehicle';
import { VehicleOptionsModalComponent } from '../../components/vehicle-options-modal/vehicle-options-modal.component';

@Component({
  selector: 'app-vehicles-page',
  templateUrl: './vehicles-page.component.html',
  styleUrls: ['./vehicles-page.component.scss'],
  providers: [VehiclesPageViewModel],
})
export class VehiclesPageComponent implements OnInit {
  constructor(
    public vm: VehiclesPageViewModel,
    private dialog: MatDialog,
  ) {}

  ngOnInit(): void {
    this.vm.loadVehicles();
    this.vm.loadCounts();
    this.vm.loadColors();
  }

  onCreateVehicle(): void {
    const dialogRef = this.dialog.open(CreateVehicleModalComponent, {
      width: '600px',
    });

    dialogRef.componentInstance.vehicleCreated.subscribe((req) => {
      this.vm.createVehicle(req);
      dialogRef.close();
    });
  }

  onSave(): void {
    console.log('Save clicked');
  }

  onExport(type: 'excel' | 'api'): void {
    console.log('Export type:', type);
  }

  onEditVehicle(id: string): void {
    const vehicle = this.vm.vehicles$.getValue().find((v) => v.id === id);
    if (!vehicle) return;

    const dialogRef = this.dialog.open(EditVehicleModalComponent, {
      width: '600px',
      data: vehicle,
    });

    dialogRef.componentInstance.vehicleUpdated.subscribe((req) => {
      this.vm.updateVehicle(id, req);
      dialogRef.close();
    });
  }

  onDeleteVehicle(id: string): void {
    console.log('Delete vehicle:', id);
    this.vm.deleteVehicle(id);
  }

  onViewPhotos(vehicle: Vehicle): void {
    if (!vehicle.photos?.length) return;

    this.dialog.open(VehiclePhotosModalComponent, {
      width: '800px',
      data: { photos: vehicle.photos },
    });
  }

  onViewOptions(vehicle: Vehicle): void {
    this.dialog.open(VehicleOptionsModalComponent, {
      width: '500px',
      data: { options: vehicle.options },
    });
  }
}
