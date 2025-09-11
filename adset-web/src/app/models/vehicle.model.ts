export interface Vehicle {
  id?: number;
  brand: string;
  model: string;
  yearMin: number;
  yearMax: number;
  plate: string;
  km?: number;
  color: string;
  price: number;
  photos?: string[];
  optionals?: string[];
  icarrosPackage?: PackageType;
  webmotorsPackage?: PackageType;
  createdAt?: Date;
  updatedAt?: Date;
}

export interface VehicleFilter {
  plate?: string;
  brand?: string;
  model?: string;
  yearMin?: number;
  yearMax?: number;
  price?: PriceRange;
  photos?: PhotoFilter;
  color?: string;
  optionals?: string;
}

export interface PaginationInfo {
  currentPage: number;
  totalPages: number;
  totalItems: number;
  itemsPerPage: number;
}

export type PackageType = 'bronze' | 'diamond' | 'platinum' | 'basic';

export type PriceRange = '10-50' | '50-90' | '90+' | 'all';

export type PhotoFilter = 'with-photos' | 'without-photos' | 'all';

export interface Optional {
  id: string;
  name: string;
  selected: boolean;
}

export const AVAILABLE_OPTIONALS: Optional[] = [
  { id: 'air-conditioning', name: 'Air Conditioning', selected: false },
  { id: 'alarm', name: 'Alarm', selected: false },
  { id: 'airbag', name: 'Airbag', selected: false },
  { id: 'abs-brakes', name: 'ABS Brakes', selected: false },
  { id: 'power-steering', name: 'Power Steering', selected: false },
  { id: 'electric-windows', name: 'Electric Windows', selected: false },
  { id: 'central-locking', name: 'Central Locking', selected: false },
  { id: 'sound-system', name: 'Sound System', selected: false }
];

export const PACKAGE_OPTIONS = [
  { value: 'bronze', label: 'Bronze' },
  { value: 'diamond', label: 'Diamond' },
  { value: 'platinum', label: 'Platinum' },
  { value: 'basic', label: 'Basic' }
];

export const YEAR_OPTIONS = Array.from({ length: 25 }, (_, i) => 2000 + i);

export const PRICE_OPTIONS = [
  { value: 'all', label: 'All' },
  { value: '10-50', label: '10k to 50k' },
  { value: '50-90', label: '50k to 90k' },
  { value: '90+', label: '90k+' }
];

export const PHOTO_OPTIONS = [
  { value: 'all', label: 'All' },
  { value: 'with-photos', label: 'With Photos' },
  { value: 'without-photos', label: 'Without Photos' }
];
