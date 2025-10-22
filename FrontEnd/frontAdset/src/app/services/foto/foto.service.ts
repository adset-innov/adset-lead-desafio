import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FotoDto } from 'src/app/interfaces/Foto/FotoDto';

@Injectable({
  providedIn: 'root'
})
export class FotoService {

  private apiUrl = `${environment.apiUrl}`;
  private apiRoute = `${environment.fotoRoute}`;

  constructor(private http:HttpClient) { }

  public EditarFoto(id: number, foto: File): Observable<FotoDto> {
  const formData = new FormData();
  formData.append('foto', foto, foto.name);
  return this.http.put<FotoDto>( 
    `${this.apiUrl}${this.apiRoute}/${id}`, 
    formData 
  );
}

public CadastrarFotos(id: number, fotos: File[]): Observable<void> {
  const formData = new FormData();

  for (const foto of fotos) {
    formData.append('fotos', foto, foto.name); 
  }

  return this.http.post<void>(
    `${this.apiUrl}${this.apiRoute}/${id}`,
    formData
  );
}



  public DeletarFoto(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}${this.apiRoute}/${id}`);
  }
}
