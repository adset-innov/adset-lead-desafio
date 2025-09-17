import { Component, OnInit } from '@angular/core';
import { VehiclesPageViewModel } from '../../viewmodels/vehicles-page.viewmodel';
import { MatDialog } from '@angular/material/dialog';
import { CreateVehicleModalComponent } from '../../components/create-vehicle-modal/create-vehicle-modal.component';

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

    dialogRef.componentInstance.save.subscribe((req) => {
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
    console.log('Edit vehicle:', id);
    // this.vm.updateVehicle(id, {...})
  }

  onDeleteVehicle(id: string): void {
    console.log('Delete vehicle:', id);
    this.vm.deleteVehicle(id);
  }
}
