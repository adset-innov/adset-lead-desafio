export interface CarsFilterDTO {
  placa?: string;
  marca?: string;
  modelo?: string;
  anoMin?: number;
  anoMax?: number;
  preco?: number;
  fotos?: string;
  opcionais?: string;
  cor?: string;
  page?: number;
  pageSize?: number;
  somenteComFotos:boolean;
}
