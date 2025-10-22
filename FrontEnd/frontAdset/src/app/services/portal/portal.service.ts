import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PortalDto } from '../../interfaces/Portal/PortalDto';

@Injectable({
  providedIn: 'root'
})
export class PortalService {
  private apiUrl = `${environment.apiUrl}`;
  private apiRoute = `${environment.portalRoute}`;

  constructor(private http: HttpClient) { }


  obterPortais(): Observable<PortalDto[]> {
    return this.http.get<PortalDto[]>(`${this.apiUrl}${this.apiRoute}`);
  }
}
