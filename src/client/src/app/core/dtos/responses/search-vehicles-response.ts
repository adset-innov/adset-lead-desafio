import { Vehicle } from '../../models/vehicle';

export interface SearchVehiclesResponse {
  vehicles: Vehicle[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}
