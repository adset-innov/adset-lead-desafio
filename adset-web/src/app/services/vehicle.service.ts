import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vehicle, VehicleFilter, PaginationInfo } from '../models/vehicle.model';
import { environment } from '../../environments/environment';

export interface VehicleResponse {
  vehicles: Vehicle[];
  pagination: PaginationInfo;
}

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private apiUrl = `${environment.apiUrl}/vehicles`;

  constructor(private http: HttpClient) { }

  getVehicles(filter?: VehicleFilter, page: number = 1, limit: number = 100): Observable<VehicleResponse> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('limit', limit.toString());

    if (filter) {
      Object.keys(filter).forEach(key => {
        const value = filter[key as keyof VehicleFilter];
        if (value !== undefined && value !== null && value !== '') {
          params = params.set(key, value.toString());
        }
      });
    }

    return this.http.get<VehicleResponse>(this.apiUrl, { params });
  }

  getVehicleById(id: number): Observable<Vehicle> {
    return this.http.get<Vehicle>(`${this.apiUrl}/${id}`);
  }

  createVehicle(vehicle: Vehicle): Observable<Vehicle> {
    return this.http.post<Vehicle>(this.apiUrl, vehicle);
  }

  updateVehicle(id: number, vehicle: Vehicle): Observable<Vehicle> {
    return this.http.put<Vehicle>(`${this.apiUrl}/${id}`, vehicle);
  }

  deleteVehicle(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  updatePackages(id: number, packages: { icarrosPackage?: string, webmotorsPackage?: string }): Observable<Vehicle> {
    return this.http.patch<Vehicle>(`${this.apiUrl}/${id}/packages`, packages);
  }

  getAvailableColors(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/colors`);
  }

  getBrands(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/brands`);
  }

  getModelsByBrand(brand: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/brands/${brand}/models`);
  }

  getVehicleCounts(): Observable<{ total: number, withPhotos: number, withoutPhotos: number }> {
    return this.http.get<{ total: number, withPhotos: number, withoutPhotos: number }>(`${this.apiUrl}/counts`);
  }
}
