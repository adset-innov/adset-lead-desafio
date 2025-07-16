import { VehicleResultDTO } from './VehicleResultDTO';

export interface FilteredVehiclesResponseDTO {
  data: VehicleResultDTO[];
  total: number;
  page: number;
  pageSize: number;
}