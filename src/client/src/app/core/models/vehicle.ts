import { PortalPackage } from './portal-package';
import { Photo } from './photo';
import { VehicleOption } from './vehicle-option';

export interface Vehicle {
  id: string;
  createdOn: Date;
  updatedOn: Date;
  brand: string;
  model: string;
  year: number;
  licensePlate: string;
  mileage: number;
  color: string;
  price: number;
  options: VehicleOption[];
  photos: Photo[];
  portalPackages: PortalPackage[];
}
