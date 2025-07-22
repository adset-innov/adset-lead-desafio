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

  list(filter: CarFilterDto, pageNumber: number, pageSize: number)
    : Observable<PaginatedList<Car>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);

    Object.entries(filter).forEach(([key, value]) => {
      if (value !== null && value !== undefined && value !== '') {
        params = params.set(key, value.toString());
      }
    });

    return this.http.get<any>(`${this.baseUrl}/get-all`, { params }).pipe(
      map(resp => {
        const pageIndex = resp.pageIndex ?? resp.PageIndex;
        const totalPagesRaw = resp.totalPages ?? resp.TotalPages;
        const totalPages = Math.max(totalPagesRaw, 1);
        const rawItems = resp.items ?? resp.Items ?? resp;
        const items: Car[] = rawItems.map((car: any) => ({
          ...car,
          photos: (car.photos as any[]).map(p => ({
            id: p.id,
            carId: p.carId,
            order: p.order,         
            displayUrl: Array.isArray(p.photoData)
              ? `data:image/jpeg;base64,${btoa(
                String.fromCharCode(...(p.photoData as number[]))
              )}`: `data:image/jpeg;base64,${p.photoData}`
          }))
        }));

        return {
          pageIndex,
          totalPages,
          items
        } as PaginatedList<Car>;
      })
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
