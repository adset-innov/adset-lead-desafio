export interface CarFilterDto {
  plate?: string;
  brand?: string;
  model?: string;
  yearMin?: number;
  yearMax?: number;
  priceMin?: number;
  priceMax?: number;
  hasPhotos?: boolean;
  optionals?: string;
  color?: string;
  pageNumber?: number;
}