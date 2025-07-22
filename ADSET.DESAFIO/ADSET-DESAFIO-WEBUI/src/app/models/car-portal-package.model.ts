import { Portal, PackageType } from '../../app/enums/car-portal-package.enums';

export interface CarPortalPackage {
  carId: number;
  portal: Portal;
  package: PackageType;
}

export interface RawCarPortalPackage {
  carId: number;
  portal: Portal;
  package: PackageType;
}

export interface PackageOption {
  name: string;
  code: string;
  selected: boolean;
}

// Grupo de pacotes por portal
export interface PortalGroup {
  portalName: Portal;
  options: PackageOption[];
}