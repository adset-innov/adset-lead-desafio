import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DefaultReturn } from 'src/app/shared/models/DefaultReturn';

// Interface fora da classe, antes dela
export interface ResumoVeiculos {
  total: number;
  comFotos: number;
  semFotos: number;
}

@Injectable({
  providedIn: 'root',
})
export class CarsService {
  private apiBaseUrl = 'https://localhost:7296/api/Cars';

  constructor(private http: HttpClient) {}

  getResumoVehicle(): Observable<DefaultReturn<ResumoVeiculos>> {
    const url = `${this.apiBaseUrl}/Resume`;
    return this.http.get<DefaultReturn<ResumoVeiculos>>(url);
  }

  getVehicleById(id:number): Observable<DefaultReturn<ResumoVeiculos>> {
    const url = `${this.apiBaseUrl}/${id}`;
    return this.http.get<DefaultReturn<ResumoVeiculos>>(url);
  }

  DeleteVehicle(vehicle: any): Observable<DefaultReturn> {
    const url = `${this.apiBaseUrl}/${vehicle}`;
    return this.http.delete<DefaultReturn>(url);
  }

  PutVehicle(vehicle: any): Observable<DefaultReturn> {
  const url = `${this.apiBaseUrl}`
  return this.http.put<DefaultReturn>(url, vehicle);
}
  
  getFilteredVehicles(filters: any): Observable<DefaultReturn<any[]>> {
  const queryParams = [];

  for (const key in filters) {
    const value = filters[key];
    if (value && value !== 'Todos') {
      queryParams.push(`${encodeURIComponent(key)}=${encodeURIComponent(value)}`);
    }
  }

  const queryString = queryParams.length ? `?${queryParams.join('&')}` : '';
  return this.http.get<DefaultReturn<any[]>>(`${this.apiBaseUrl}/filtered${queryString}`);
}
}
