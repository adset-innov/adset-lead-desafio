import { Component, EventEmitter, Input, Output } from '@angular/core';
import {  UpdateVehiclePortalPackages } from 'src/app/models/PortalPackageSelection';
import { VehicleService } from 'src/app/services/vehicle.service';


@Component({
  selector: 'app-vehicle-packages',
  templateUrl: './vehicle-packages.component.html'
})
export class VehiclePackagesComponent {
  @Input() vehicleId!: number;
  @Output() packagesSelected = new EventEmitter<UpdateVehiclePortalPackages>();
  icarrosPackages = [
    { name: 'Diamante Feirão', code: '010 - 008' },
    { name: 'Diamante', code: '030 - 025' },
    { name: 'Platinum', code: '040 - 010' }
  ];

  webmotorsPackages = [
    { name: 'Básico', code: '010 - 008' },
    { name: 'Diamante', code: '030 - 025' },
    { name: 'Platinum', code: '040 - 010' }
  ];

  icarrosSelected: string | null = null;
  webmotorsSelected: string | null = null;


  constructor(private vehicleService: VehicleService) {}

  selectPackage(portal: string, packageName: string) {
    if (portal === 'iCarros') {
      this.icarrosSelected = packageName;
    }
    if (portal === 'WebMotors') {
      this.webmotorsSelected = packageName;
    }
    this.emitSelection();
  }

   emitSelection() {
    const selections: any[] = [];

    if (this.icarrosSelected) {
      selections.push({ portal: 'iCarros', packageName: this.icarrosSelected, selected: true });
    }
    if (this.webmotorsSelected) {
      selections.push({ portal: 'WebMotors', packageName: this.webmotorsSelected, selected: true });
    }

    const model: UpdateVehiclePortalPackages = {
      vehicleId: this.vehicleId,
      portalSelections: selections
    };

    this.packagesSelected.emit(model);
    return model;
  }

    save() {
    const model: UpdateVehiclePortalPackages = {
      vehicleId: this.vehicleId,
      portalSelections: []
    };

    if (this.icarrosSelected) {
      model.portalSelections.push({
          portal: 'iCarros',
          packageName: this.icarrosSelected,
          selected: true
        });
    }

    if (this.webmotorsSelected) {
       model.portalSelections.push({
        portal: 'WebMotors',
        packageName: this.webmotorsSelected,
        selected: true
      });
    }

    this.vehicleService.updatePackages(model).subscribe({
      next: () => alert('Pacotes salvos com sucesso!'),
      error: (err) => console.error('Erro ao salvar pacotes', err)
    });
  }
}
