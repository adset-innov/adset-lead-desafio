import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { VehicleOption } from '../models/vehicle-option';
import { CreateVehicleOptionResponse } from '../dtos/responses/create-vehicle-option-response';

@Injectable({ providedIn: 'root' })
export class VehicleOptionsService {
  private readonly baseUrl = `${environment.apiUrl}/vehicleoptions`;

  constructor(private http: HttpClient) {}

  search(query: string): Observable<VehicleOption[]> {
    return this.http.get<VehicleOption[]>(`${this.baseUrl}/search`, {
      params: { query },
    });
  }

  create(name: string): Observable<CreateVehicleOptionResponse> {
    return this.http.post<CreateVehicleOptionResponse>(this.baseUrl, { name });
  }
}
