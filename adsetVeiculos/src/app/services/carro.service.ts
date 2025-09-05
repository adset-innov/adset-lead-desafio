import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface VeiculoRequest {
  marca?: string;
  modelo?: string;
  anoMin?: number;
  anoMax?: number;
  precoMin?: number;
  precoMax?: number;
  hasPhotos?: boolean;
  opcionais?: string;
  cor?: string;
  placa?: string;
  fotos?: FotoRequest[];
  pacoteIcarros?: number;
  pacoteWebmotors?: number;
  page: number;
  pageSize: number;
}

export interface VeiculoResponse {
  id: number;
  ano?: number;
  cor?: string;
  fotos: FotoResponse[];
  listaOpcionais?: string;
  marca?: string;
  modelo?: string;
  placa?: string;
  pacotes: PacoteResponse[];
  preco?: number;
  quilometragem?: number;
  totalCarrosCadastrados?: number;
  totalCarrosFiltrados?: number;
  totalCarrosComFotos?: number;
  totalCarrosSemFotos?: number;
  cores?: string[];
  page: number;
  totalPaginas: number;
}

export interface PacoteResponse {
  id: number;
  idVeiculo: number;
  tipoPortal: number;
  tipoPacote: number;
}

export interface FotoResponse {
  id: number;
  url?: string;
  idVeiculo?: number;
}

export interface FotoRequest {
  url?: string;
}

@Injectable({
  providedIn: 'root',
})
export class CarroService {
  private apiUrl = 'http://localhost:5206/v1/Veiculo';

  constructor(private http: HttpClient) {}

  getCarros(filtro: VeiculoRequest): Observable<any> {
    let params = new HttpParams();

    if (filtro.marca) params = params.set('Marca', filtro.marca);
    if (filtro.modelo) params = params.set('Modelo', filtro.modelo);
    if (filtro.anoMin != null)
      params = params.set('AnoMin', filtro.anoMin.toString());
    if (filtro.anoMax != null)
      params = params.set('AnoMax', filtro.anoMax.toString());
    if (filtro.precoMin != null)
      params = params.set('PrecoMin', filtro.precoMin.toString());
    if (filtro.precoMax != null)
      params = params.set('PrecoMax', filtro.precoMax.toString());
    if (filtro.hasPhotos != null)
      params = params.set('HasPhotos', filtro.hasPhotos.toString());
    if (filtro.opcionais) params = params.set('Opcionais', filtro.opcionais);
    if (filtro.cor) params = params.set('Cor', filtro.cor);
    if (filtro.placa) params = params.set('Placa', filtro.placa);
    if (filtro.pacoteIcarros)
      params = params.set('PacoteIcarros', filtro.pacoteIcarros);
    if (filtro.pacoteWebmotors)
      params = params.set('PacoteWebmotors', filtro.pacoteWebmotors);

    params = params.set('Page', filtro.page.toString());
    params = params.set('PageSize', filtro.pageSize.toString());

    return this.http.get<any>(this.apiUrl, { params });
  }

  cadastrarCarro(carro: any): Observable<any> {
    return this.http.post(`${this.apiUrl}`, carro);
  }

  deletarCarro(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getCarroPorId(id: number) {
    return this.http.get<VeiculoResponse>(`${this.apiUrl}/${id}`);
  }

  atualizarCarro(id: number, dados: any) {
    dados.id = id;
    return this.http.put(`${this.apiUrl}`, dados);
  }
}
