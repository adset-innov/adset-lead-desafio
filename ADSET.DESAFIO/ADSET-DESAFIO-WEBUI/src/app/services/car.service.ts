import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Car } from '../models/car.model';
import { PaginatedList } from '../models/paginated-list.model';
import { CarFilterDto } from '../dto/car-filter.dto';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class CarService {
  private readonly baseUrl = `${environment.apiUrl}/api/cars`;

  constructor(private http: HttpClient) { }

  list(filter: CarFilterDto, pageNumber: number, pageSize: number): Observable<PaginatedList<Car>> {
    let params = new HttpParams().set('pageNumber', pageNumber).set('pageSize', pageSize);
    Object.entries(filter).forEach(([key, value]) => {
      if (value !== null && value !== undefined && value !== '') {
        params = params.set(key, value.toString());
      }
    });

    return this.http.get<any>(`${this.baseUrl}/get-all`, { params }).pipe(
      map(resp => ({
        pageIndex: resp.pageIndex ?? resp.PageIndex,
        totalPages: resp.totalPages ?? resp.TotalPages,
        items: resp.items ?? resp
      }) as PaginatedList<Car>)
    );
  }

  getById(id: number): Observable<Car> {
    return this.http.get<Car>(`${this.baseUrl}/get-by-id/${id}`);
  }

  create(formData: FormData): Observable<Car> {
    return this.http.post<Car>(`${this.baseUrl}/register`, formData);
  }

  update(id: number, formData: FormData): Observable<Car> {
    return this.http.put<Car>(`${this.baseUrl}/update?id=${id}`, formData);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/delete?id=${id}`);
  }
}
