export interface UpdateVehicleRequest {
  brand: string;
  model: string;
  year: number;
  licensePlate: string;
  color: string;
  price: number;
  mileage: number;
  options: { [key: string]: boolean };
}
