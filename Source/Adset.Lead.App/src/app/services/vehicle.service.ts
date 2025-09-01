import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Vehicle, VehicleStats, VehicleFilter } from '../models/vehicle.model';
import { environment } from '../../environments/environment';

interface ApiVehicle {
  id: string;
  brand: string;
  model: string;
  year: number;
  plate: string;
  km?: number;
  color: string;
  price: number;
  features: number[];
  photos: string;
  portal?: string;
  package?: string;
}

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private readonly apiUrl = `${environment.apiUrl}/automobiles`;

  constructor(private http: HttpClient) { }

  getVehicles(): Observable<Vehicle[]> {
    return this.http.get<ApiVehicle[]>(this.apiUrl).pipe(
      map((vehicles: ApiVehicle[]) => vehicles.map((vehicle: ApiVehicle) => this.mapVehicleFromApi(vehicle)))
    );
  }

  getVehicleStats(): Observable<VehicleStats> {
    return this.getVehicles().pipe(
      map((vehicles: Vehicle[]) => {
        const total = vehicles.length;
        const withPhotos = vehicles.filter((v: Vehicle) => v.hasPhotos).length;
        const withoutPhotos = total - withPhotos;

        return {
          total,
          withPhotos,
          withoutPhotos
        };
      })
    );
  }

  searchVehicles(filter: VehicleFilter): Observable<Vehicle[]> {
    let params = new HttpParams();
    
    if (filter.plate && filter.plate.trim()) {
      params = params.set('plate', filter.plate.trim());
    }
    
    if (filter.brand && filter.brand.trim()) {
      params = params.set('brand', filter.brand.trim());
    }
    
    if (filter.model && filter.model.trim()) {
      params = params.set('model', filter.model.trim());
    }
    
    if (filter.yearMin) {
      params = params.set('yearMin', filter.yearMin.toString());
    }
    
    if (filter.yearMax) {
      params = params.set('yearMax', filter.yearMax.toString());
    }
    
    if (filter.color && filter.color.trim() && filter.color !== 'Todos') {
      params = params.set('color', filter.color.trim());
    }
    
    if (filter.price && filter.price !== 'Todos') {
      const priceRange = this.parsePriceRange(filter.price);
      if (priceRange.min !== null) {
        params = params.set('priceMin', priceRange.min.toString());
      }
      if (priceRange.max !== null) {
        params = params.set('priceMax', priceRange.max.toString());
      }
    }
    
    if (filter.photos && filter.photos !== 'Todos') {
      const hasPhotos = filter.photos === 'Com fotos';
      params = params.set('hasPhotos', hasPhotos.toString());
    }
    
    if (filter.features && filter.features !== 'Todos') {
      const featureId = this.mapFeatureNameToId(filter.features);
      if (featureId !== null) {
        params = params.set('feature', featureId.toString());
      }
    }
    
    return this.http.get<ApiVehicle[]>(this.apiUrl, { params }).pipe(
      map((vehicles: ApiVehicle[]) => vehicles.map((vehicle: ApiVehicle) => this.mapVehicleFromApi(vehicle)))
    );
  }
  
  private mapFeatureNameToId(featureName: string): number | null {
    const featureMap: { [key: string]: number } = {
      'Ar Condicionado': 1,
      'Alarme': 2,
      'Airbag': 3,
      'Freio ABS': 4,
      'MP3 Player': 5
    };
    
    return featureMap[featureName] || null;
  }
  
  private parsePriceRange(priceFilter: string): { min: number | null; max: number | null } {
    switch (priceFilter) {
      case '0 - 50,000':
        return { min: 0, max: 50000 };
      case '50,000 - 100,000':
        return { min: 50000, max: 100000 };
      case '100,000+':
        return { min: 100000, max: null };
      default:
        return { min: null, max: null };
    }
  }

  createVehicle(vehicle: Partial<Vehicle>): Observable<string> {
    const apiVehicle = this.mapVehicleToApi(vehicle);
    return this.http.post<any>(this.apiUrl, apiVehicle).pipe(
      map(response => response.id || response.Id || 'new-id')
    );
  }

  updateVehicle(id: string, vehicle: Partial<Vehicle>): Observable<void> {
    const apiVehicle = this.mapVehicleToApi(vehicle);
    return this.http.put<void>(`${this.apiUrl}/${id}`, apiVehicle);
  }

  deleteVehicle(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  // Mapeia dados da API para o formato esperado pelo frontend
  private mapVehicleFromApi(apiVehicle: ApiVehicle): Vehicle {
    // Garantir que photos seja um array de strings
    let photos: string[] = [];
    try {
      const parsed = apiVehicle.photos ? JSON.parse(apiVehicle.photos) : [];
      
      if (Array.isArray(parsed)) {
        // Garantir que todos os elementos sejam strings
        photos = parsed.map(photo => {
          if (typeof photo === 'string') {
            return photo;
          } else if (photo && typeof photo === 'object' && photo.FileName) {
            // Se é um objeto com propriedade FileName, extrair o valor
            return photo.FileName;
          } else if (photo != null) {
            return String(photo);
          } else {
            return '';
          }
        }).filter(photo => photo.length > 0); // Remover strings vazias
      }
    } catch (error) {
      console.error('Failed to parse photos:', error);
      photos = [];
    }

    // Adicionar extensão .jpg aos nomes de arquivo se não tiver
    const photosWithExtension = photos.map(photoName => {
      // Se já tem extensão, manter; senão adicionar .jpg
      return photoName.includes('.') ? photoName : `${photoName}.jpg`;
    });

    // Features agora vem como array de números diretamente da API
    const features = Array.isArray(apiVehicle.features) ? apiVehicle.features : [];

    // Garantir que imageUrl seja sempre uma string válida
    let imageUrl = '';
    if (photos.length > 0 && typeof photos[0] === 'string' && photos[0].trim()) {
      // Usar endpoint da API para servir as imagens
      const fileName = photosWithExtension[0]; // Já tem a extensão .jpg
      imageUrl = `http://localhost:5062/api/images/${fileName}`;
    }

    const mappedVehicle = {
      // API 
      id: apiVehicle.id,
      plate: apiVehicle.plate,
      brand: apiVehicle.brand,
      model: apiVehicle.model,
      year: apiVehicle.year,
      color: apiVehicle.color,
      price: apiVehicle.price,
      km: apiVehicle.km,
      features: features, // Agora é array de números
      photos: photosWithExtension, // Array de nomes de arquivo com extensão
      portal: apiVehicle.portal,
      package: apiVehicle.package,
      hasPhotos: photos.length > 0,
      photosCount: photos.length,
      featuresCount: features.length
    };
    
    return mappedVehicle;
  }

  private mapVehicleToApi(vehicle: Partial<Vehicle>): any {
    // Features agora já é array de números
    const featuresArray = vehicle.features || [];

    // Photos já é um array de strings (GUIDs) - remover extensões se existirem
    const photosArray: string[] = (vehicle.photos || []).map(photo => {
      // Se o photo contém extensão, remove ela para obter apenas o GUID
      const photoWithoutExtension = photo.includes('.') ? photo.substring(0, photo.lastIndexOf('.')) : photo;
      return photoWithoutExtension;
    });

    const mappedVehicle = {
      brand: vehicle.brand,
      model: vehicle.model,
      year: vehicle.year,
      plate: vehicle.plate,
      color: vehicle.color,
      price: vehicle.price,
      km: vehicle.km,
      portal: this.mapPortalName(vehicle.portal || 'WebMotors'),
      package: this.mapPackageName(vehicle.package || 'Basic'),
      optionalFeatures: this.mapFeatures(featuresArray),
      fileNames: photosArray
    };
    
    return mappedVehicle;
  }

  private mapPortalName(portalName: string): number {
    switch (portalName) {
      case 'ICars':
      case 'ICarros':
        return 1; 
      case 'WebMotors':
        return 2;
      default:
        return 2;
    }
  }

  private mapPackageName(packageName: string): number {
    switch (packageName) {
      case 'Bronze':
        return 1;
      case 'Diamond':
        return 2;
      case 'Platinum':
        return 3;
      case 'Basic':
        return 4;
      default:
        return 4;
    }
  }

  private mapFeatures(features: number[]): number[] {
    // Features já vem como array de números, apenas filtra valores válidos
    return features.filter(f => f > 0 && f <= 5);
  }
}
