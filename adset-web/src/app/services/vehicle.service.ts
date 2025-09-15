import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vehicle } from '../models/VehicleModel';
import { environment } from 'src/environment/environment';
import { SearchVehiclesFilter } from '../models/SearchVehiclesFilter';
import { UpdateVehiclePortalPackages } from '../models/PortalPackageSelection';


@Injectable({ providedIn: 'root' })
export class VehicleService {
   private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  search(filter: SearchVehiclesFilter, currentPage: number = 1, pageSize: number = 10): Observable<any> {
    return this.http.post<any>(
      `${this.baseUrl}/vehicles/search?currentPage=${currentPage}&pageSize=${pageSize}`,
      filter
    );
  }

  getById(id: number) {
  return this.http.get<Vehicle>(`${this.baseUrl}/vehicles/${id}`);
  }

  create(vehicle: FormData): Observable<any> {
    return this.http.post(`${this.baseUrl}/vehicles`, vehicle);
  }

  update(id: number, vehicle: FormData): Observable<any> {
    return this.http.put(`${this.baseUrl}/vehicles/${id}`, vehicle);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/vehicles/${id}`);
  }

  getOpcionais(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/optionals`);
  }

   updatePackages(model: UpdateVehiclePortalPackages): Observable<any> {
    return this.http.post(`${this.baseUrl}/VehiclePortalPackages/update`, model);
  }

}
