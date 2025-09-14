import { Vehicle } from '../../models/vehicle';

export interface SearchVehiclesResponse {
  items: Vehicle[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}
