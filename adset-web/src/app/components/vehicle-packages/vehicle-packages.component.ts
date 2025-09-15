import { Component, EventEmitter, Input, Output } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';
import { UpdateVehiclePortalPackages } from '../../models/PortalPackageSelection';



@Component({
  selector: 'app-vehicle-packages',
  templateUrl: './vehicle-packages.component.html'
})
export class VehiclePackagesComponent {
  @Input() vehicleId!: number;
  @Output() packagesSelected = new EventEmitter<UpdateVehiclePortalPackages>();
@Input() portalPackages: { icarros?: string; webmotors?: string } | undefined;
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

  ngOnChanges() {
    if (this.portalPackages) {
      if (this.portalPackages.icarros) {
        this.icarrosSelected = this.portalPackages.icarros;
      }
      if (this.portalPackages.webmotors) {
        this.webmotorsSelected = this.portalPackages.webmotors;
      }
    }
  }

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
      selections.push({ portalName: 'iCarros', packageName: this.icarrosSelected, selected: true });
    }
    if (this.webmotorsSelected) {
      selections.push({ portalName: 'WebMotors', packageName: this.webmotorsSelected, selected: true });
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
          portalName: 'iCarros',
          packageName: this.icarrosSelected,
          selected: true
        });
    }

    if (this.webmotorsSelected) {
       model.portalSelections.push({
        portalName: 'WebMotors',
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
