
import { CarOptional } from './car-optional.model';
import { CarPhoto } from './car-photo.model';
import { CarPortalPackage } from './car-portal-package.model';

export interface Car {
  id: number;
  brand: string;
  model: string;
  year: number;
  plate: string;
  km: number;
  color: string;
  price: number;
  optionals: CarOptional[];
  photos: CarPhoto[];
  portalPackages: CarPortalPackage[];
}