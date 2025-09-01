// Enum para mapear features
export enum OptionalFeatures {
  None = 0,
  AirConditioning = 1,
  Alarm = 2,
  Airbag = 3,
  AbsBrake = 4,
  Mp3Player = 5
}

export const FeatureNames: { [key: number]: string } = {
  [OptionalFeatures.AirConditioning]: 'Ar Condicionado',
  [OptionalFeatures.Alarm]: 'Alarme',
  [OptionalFeatures.Airbag]: 'Airbag',
  [OptionalFeatures.AbsBrake]: 'Freio ABS',
  [OptionalFeatures.Mp3Player]: 'MP3 Player'
};

export interface Vehicle {
  id: string;
  plate: string;
  brand: string;
  model: string;
  year: number;
  color: string;
  price: number;
  km?: number;
  features: number[];
  photos: string[]; // Array de GUIDs das fotos
  portal?: string;
  package?: string;
  

  hasPhotos?: boolean; 
  photosCount?: number; 
  featuresCount?: number; 
}

export interface VehicleFilter {
  plate: string;
  brand: string;
  model: string;
  yearMin: number | null;
  yearMax: number | null;
  price: string;
  photos: string;
  features: string;
  color: string;
}

export interface VehicleStats {
  total: number;
  withPhotos: number;
  withoutPhotos: number;
}
