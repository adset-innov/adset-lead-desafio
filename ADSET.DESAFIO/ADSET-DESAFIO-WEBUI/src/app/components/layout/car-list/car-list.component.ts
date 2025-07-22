import { Component, OnInit } from '@angular/core';
import { Car } from '../../../models/car.model';
import {
  RawCarPortalPackage,
  PackageOption,
  PortalGroup
} from '../../../models/car-portal-package.model';
import { Portal, PackageType } from '../../../enums/car-portal-package.enums';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.scss']
})
export class CarListComponent implements OnInit {
  cars: Car[] = [];
  showPackagesMap: { [carId: number]: boolean } = {};
  currentPage = 1;
  totalPagesFromApi = 10;

  ngOnInit(): void {
    this.cars = [
      {
        id: 1,
        brand: 'Volkswagen',
        model: 'Golf',
        year: 2020,
        plate: 'AAA-0102',
        km: 25000,
        color: 'Branco',
        price: 103900.00,
        optionals: [
          { carId: 1, optionalId: 1 },
          { carId: 1, optionalId: 2 }
        ],
        photos: [
          { id: 1, carId: 1, url: 'assets/sample-car.jpg', order: 0 }
        ],
        portalPackages: [
          { carId: 1, portal: Portal.iCarros, package: PackageType.Diamante },
          { carId: 1, portal: Portal.iCarros, package: PackageType.Platinum },
          { carId: 1, portal: Portal.WebMotors, package: PackageType.Basico }
        ] as RawCarPortalPackage[]
      }
    ];
  }

  togglePackages(carId: number): void {
    this.showPackagesMap[carId] = !this.showPackagesMap[carId];
  }

  groupByPortal(pkgs: RawCarPortalPackage[]): PortalGroup[] {
    const map = new Map<Portal, PackageOption[]>();

    for (const p of pkgs) {
      if (!map.has(p.portal)) {
        map.set(p.portal, []);
      }

      map.get(p.portal)!.push({
        name: p.package,
        code: this.lookupCode(p),
        selected: false
      });
    }

    return Array.from(map.entries()).map(([portalName, options]) => ({
      portalName,
      options
    }));
  }

  groupCount(pkgs: RawCarPortalPackage[]): number {
    return new Set(pkgs.map(p => p.portal)).size;
  }

  onPackageToggle(
    car: Car,
    portalName: Portal,
    toggled: PackageOption
  ) {
    const group = this.groupByPortal(car.portalPackages).find(g => g.portalName === portalName)!;
    group.options.forEach(o => (o.selected = false));
    toggled.selected = true;
  }

  private lookupCode(p: RawCarPortalPackage): string {
    switch (p.package) {
      case PackageType.Diamante: return '030-025';
      case PackageType.Platinum: return '040-010';
      case PackageType.Basico: return '030-025';
      default: return '';
    }
  }

  onPageChange(newPage: number) {
    this.currentPage = newPage;
  }

}
