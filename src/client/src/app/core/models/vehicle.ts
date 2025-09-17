import { PortalPackage } from './portal-package';
import { Photo } from './photo';

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
  options: string[];
  photos: Photo[];
  portalPackages: PortalPackage[];
}
