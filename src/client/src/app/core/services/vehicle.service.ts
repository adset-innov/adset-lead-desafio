import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Vehicle } from '../models/vehicle';
import { Photo } from '../models/photo';
import { Portal } from '../enums/portal';
import { UpdateVehicleRequest } from '../dtos/requests/update-vehicle-request';
import { SearchVehiclesRequest } from '../dtos/requests/search-vehicles-request';
import { AddOrUpdateVehiclePortalPackageRequest } from '../dtos/requests/add-or-update-vehicle-portal-package-request';
import { CreateVehicleRequest } from '../dtos/requests/create-vehicle-request';
import { SearchVehiclesResponse } from '../dtos/responses/search-vehicles-response';
import { VehicleTotalCountResponse } from '../dtos/responses/vehicle-total-count-response';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  private readonly baseUrl = `${environment.apiUrl}/vehicle`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Vehicle[]> {
    return this.http.get<Vehicle[]>(this.baseUrl);
  }

  getById(id: string): Observable<Vehicle> {
    return this.http.get<Vehicle>(`${this.baseUrl}/${id}`);
  }

  create(req: CreateVehicleRequest): Observable<Vehicle> {
    const formData = new FormData();
    formData.append('brand', req.brand);
    formData.append('model', req.model);
    formData.append('year', req.year.toString());
    formData.append('licensePlate', req.licensePlate);
    formData.append('color', req.color);
    formData.append('price', req.price.toString());
    formData.append('mileage', req.mileage.toString());
    formData.append('options', JSON.stringify(req.options));

    if (req.files) {
      req.files.forEach((file) => {
        formData.append('files', file, file.name);
      });
    }

    if (req.portalPackages) {
      formData.append('portalPackages', JSON.stringify(req.portalPackages));
    }

    return this.http.post<Vehicle>(this.baseUrl, formData);
  }

  update(id: string, req: UpdateVehicleRequest): Observable<Vehicle> {
    return this.http.put<Vehicle>(`${this.baseUrl}/${id}`, req);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  search(filters: SearchVehiclesRequest): Observable<SearchVehiclesResponse> {
    let params = new HttpParams();
    Object.keys(filters).forEach((key) => {
      const value = (filters as any)[key];
      if (value !== undefined && value !== null) {
        params = params.set(key, value);
      }
    });

    return this.http.get<SearchVehiclesResponse>(`${this.baseUrl}/search`, {
      params,
    });
  }

  getTotalCount(): Observable<VehicleTotalCountResponse> {
    return this.http.get<VehicleTotalCountResponse>(
      `${this.baseUrl}/count/total`,
    );
  }

  getWithPhotosCount(): Observable<VehicleTotalCountResponse> {
    return this.http.get<VehicleTotalCountResponse>(
      `${this.baseUrl}/count/with-photos`,
    );
  }

  getWithoutPhotosCount(): Observable<VehicleTotalCountResponse> {
    return this.http.get<VehicleTotalCountResponse>(
      `${this.baseUrl}/count/without-photos`,
    );
  }

  getDistinctColors(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/colors`);
  }

  uploadPhoto(id: string, file: File): Observable<Photo> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<Photo>(`${this.baseUrl}/${id}/photos`, formData);
  }

  removePhoto(id: string, photoId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}/photos/${photoId}`);
  }

  addOrUpdatePortalPackage(
    id: string,
    req: AddOrUpdateVehiclePortalPackageRequest,
  ): Observable<Vehicle> {
    return this.http.post<Vehicle>(`${this.baseUrl}/${id}/portal-package`, req);
  }

  removePortalPackage(id: string, portal: Portal): Observable<Vehicle> {
    return this.http.delete<Vehicle>(
      `${this.baseUrl}/${id}/portal-package/${portal}`,
    );
  }
}
