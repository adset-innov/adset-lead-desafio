import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Car } from '../models/car.model';
import { CarFilterDto } from '../dto/car-filter.dto';

@Injectable({ providedIn: 'root' })
export class CarService {
  private readonly baseUrl = '/api/cars';

  constructor(private http: HttpClient) { }

  list(filter: CarFilterDto): Observable<Car[]> {
    let params = new HttpParams();
    Object.entries(filter).forEach(([key, value]) => {
      if (value !== null && value !== undefined && value !== '') {
        params = params.set(key, value.toString());
      }
    });
    return this.http.get<Car[]>(this.baseUrl, { params });
  }

  getById(id: number): Observable<Car> {
    return this.http.get<Car>(`${this.baseUrl}/${id}`);
  }

  create(formData: FormData): Observable<Car> {
    return this.http.post<Car>(`${this.baseUrl}/register`, formData);
  }

  update(id: number, formData: FormData): Observable<Car> {
    return this.http.put<Car>(`${this.baseUrl}/${id}`, formData);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}