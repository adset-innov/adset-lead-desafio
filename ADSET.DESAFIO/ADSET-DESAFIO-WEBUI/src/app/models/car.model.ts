export enum Portal {
  iCarros = 'iCarros',
  WebMotors = 'WebMotors'
}

export enum Package {
  Bronze = 'Bronze',
  Diamante = 'Diamante',
  Platinum = 'Platinum',
  Basico = 'Basico'
}

export interface Car {
  id: number;
  brand: string;
  model: string;
  year: number;
  plate: string;
  km?: number;
  color: string;
  price: number;
  optionals: string[];
  portalPackages: Array<{ portal: Portal; package: Package }>;
  photos: string[];
}
