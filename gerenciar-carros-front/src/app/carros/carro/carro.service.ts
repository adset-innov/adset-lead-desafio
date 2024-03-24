import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';

export interface IUserService {
  getCarro(id: string): Observable<any>
  updateCarro(id: string, carro: any): Observable<any>
  createCarro(carro: any): Observable<any>
  excluirCarro(id: string): Observable<any>
  getAllCarros(): Observable<any>
}


@Injectable({
  providedIn: 'root'
})
export class CarroService {
  private readonly httpClient = inject(HttpClient)
  constructor() { }


  createCarro(carro: any){
    return this.httpClient.post<any>(`${environment.baseUrl}/api/carro`, carro)
  }

  updateCarro(id:string, carro: any){
    return this.httpClient.put<any>(`${environment.baseUrl}/api/carro/${id}`, carro)
  }

  deleteCarro(id:string){
    return this.httpClient.delete<any>(`${environment.baseUrl}/api/carro/${id}`)
  }

  deleteImagem(id:string){
    return this.httpClient.delete<any>(`${environment.baseUrl}/api/carro/imagem/${id}`)
  }

  deleteOpcional(id:string){
    return this.httpClient.delete<any>(`${environment.baseUrl}/api/carro/opcional/${id}`)
  }

  uploadImagens(files: FormData, idCarro: string)
  {
    return this.httpClient.post<any>(`${environment.baseUrl}/api/carro/upload/${idCarro}`, files)
  }

  getTotais(tipo: string){
    return this.httpClient.get<any>(`${environment.baseUrl}/api/carro/totais/${tipo}`)
  }

  getAnos(){
    return this.httpClient.get<any>(`${environment.baseUrl}/api/carro/anos`)
  }

  getById(id: string){
    return this.httpClient.get<any>(`${environment.baseUrl}/api/carro/${id}`)
  }

  getCarros(paginacao: any){
    return this.httpClient.post<any>(`${environment.baseUrl}/api/carro/paginacao`, paginacao)
  }

  vincularPacote(pacote: any){
    return this.httpClient.post<any>(`${environment.baseUrl}/api/carro/vincularPacote`, pacote)
  }

  deletePacote(idCarro:string, tipoPacote: number){
    return this.httpClient.delete<any>(`${environment.baseUrl}/api/carro/pacote/${idCarro}/${tipoPacote}`)
  }
}
