import { PortalPackage } from '../../models/portal-package';

export interface CreateVehicleRequest {
  brand: string;
  model: string;
  year: number;
  licensePlate: string;
  color: string;
  price: number;
  mileage: number;
  options: { [key: string]: boolean };
  files?: File[];
  portalPackages?: PortalPackage[];
}
