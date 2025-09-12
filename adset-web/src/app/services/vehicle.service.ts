import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vehicle } from '../models/vehicle.model';
import { environment } from 'src/environment/environment';


@Injectable({ providedIn: 'root' })
export class VehicleService {
   private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Vehicle[]> {
    return this.http.get<Vehicle[]>(`${this.baseUrl}/vehicles`);
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

}
