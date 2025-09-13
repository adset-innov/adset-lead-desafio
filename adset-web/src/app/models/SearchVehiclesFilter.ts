export interface SearchVehiclesFilter {
  plate?: string;
  model?: string;
  make?: string;
  yearMin?: number;
  yearMax?: number;
  km?: number;
  colors?: string[];
  price?: string;
  images?: boolean;
  optionals?: number[];
}
