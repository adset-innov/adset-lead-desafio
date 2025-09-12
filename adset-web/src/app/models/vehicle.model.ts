export interface Vehicle {
  id: number;
  plate: string;
  model: string;
  make: string;
  year: number;
  color: string;
  price: number;
  km?: number;
  images?: string[];
  optionals?: number[];
  portalPackages?: {
    icarros: string;
    webmotors: string;
  };
}

