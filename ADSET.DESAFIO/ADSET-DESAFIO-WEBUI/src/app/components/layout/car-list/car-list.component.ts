import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Car } from '../../../models/car.model';
import { CarService } from '../../../services/car.service';
import {
  RawCarPortalPackage,
  PackageOption,
  PortalGroup
} from '../../../models/car-portal-package.model';
import { Portal, PackageType, PackageTypeApiMap } from '../../../enums/car-portal-package.enums';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.scss']
})
export class CarListComponent implements OnInit {
  @Input() cars: Car[] = [];
  @Input() currentPage = 1;
  @Output() delete = new EventEmitter<number>();
  @Output() edit = new EventEmitter<Car>();
  showPackagesMap: { [carId: number]: boolean } = {};
  pageSize = 10;

  constructor(private carService: CarService) { }

  ngOnInit(): void { }

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
        name: this.displayName(p.package),
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

  deleteCar(id: number): void {
    this.delete.emit(id);
  }

  onEdit(car: Car): void {
    this.edit.emit(car);
  }

  private lookupCode(p: RawCarPortalPackage): string {
    switch (p.package) {
      case PackageType.Diamante: return '030-025';
      case PackageType.Platinum: return '040-010';
      case PackageType.Basico: return '030-025';
      default: return '';
    }
  }

  private displayName(pkg: any): string {
    if (PackageTypeApiMap[pkg as PackageType]) {
      return pkg as string;
    }
    const entry = Object.entries(PackageTypeApiMap).find(([key, value]) => value === pkg || +key === pkg);
    return entry ? entry[0] : String(pkg);
  }

  onPageChange(newPage: number) {
    this.currentPage = newPage;
  }
}
