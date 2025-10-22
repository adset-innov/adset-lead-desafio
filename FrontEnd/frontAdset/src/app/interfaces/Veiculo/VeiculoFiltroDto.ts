import { VeiculoDto } from "./VeiculoDto";

export interface VeiculoFiltroDto {
  itens: VeiculoDto[];
  paginaAtual: number;
  tamanhoPagina: number;
  totalRegistros: number;
  totalPaginas: number;
}