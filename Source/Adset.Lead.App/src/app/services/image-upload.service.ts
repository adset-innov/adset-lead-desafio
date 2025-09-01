import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageUploadService {
  private readonly apiUrl = `${environment.apiUrl}/images`;

  constructor(private http: HttpClient) { }

  uploadImage(file: File): Observable<{ url: string, fileName: string, originalName: string, size: number }> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<{ url: string, fileName: string, originalName: string, size: number }>(`${this.apiUrl}/upload`, formData);
  }

  addImageToAutomobile(automobileId: string, fileName: string): Observable<any> {
    return this.http.post(`${environment.apiUrl}/automobiles/${automobileId}/images`, {
      fileName: fileName
    });
  }

  deleteImage(fileName: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${fileName}`);
  }
}
