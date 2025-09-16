import { Portal } from '../../enums/portal';
import { Package } from '../../enums/package';

export interface SearchVehiclesRequest {
  plate?: string;
  brand?: string;
  model?: string;
  yearMin?: number;
  yearMax?: number;
  priceMin?: number;
  priceMax?: number;
  hasPhotos?: boolean;
  color?: string;
  portal?: Portal;
  package?: Package;
  pageNumber?: number;
  pageSize?: number;
  sortField?: string;
  sortDirection?: 'asc' | 'desc';
}
