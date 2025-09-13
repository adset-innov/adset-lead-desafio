export interface VehicleImage {
  id: number;
  imageUrls: string;
}

export interface Vehicle {
  id: number;
  plate: string;
  model: string;
  make: string;
  year: number;
  color: string;
  price: number;
  km?: number;
  imageUrls: string[];
  optionals?: number[];
  portalPackages?: {
    icarros: string;
    webmotors: string;
  };
}

