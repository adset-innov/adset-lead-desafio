import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { Vehicle } from '../../../core/models/vehicle';
import { Portal } from '../../../core/enums/portal';
import { SearchVehiclesRequest } from '../../../core/dtos/requests/search-vehicles-request';
import { CreateVehicleRequest } from '../../../core/dtos/requests/create-vehicle-request';
import { UpdateVehicleRequest } from '../../../core/dtos/requests/update-vehicle-request';
import { AddOrUpdateVehiclePortalPackageRequest } from '../../../core/dtos/requests/add-or-update-vehicle-portal-package-request';
import { SearchVehiclesResponse } from '../../../core/dtos/responses/search-vehicles-response';
import { VehicleTotalCountResponse } from '../../../core/dtos/responses/vehicle-total-count-response';
import { VehicleService } from '../../../core/services/vehicle.service';

@Injectable()
export class VehiclesPageViewModel {
  vehicles$ = new BehaviorSubject<Vehicle[]>([]);
  totalCount$ = new BehaviorSubject<number>(0);
  withPhotosCount$ = new BehaviorSubject<number>(0);
  withoutPhotosCount$ = new BehaviorSubject<number>(0);
  colors$ = new BehaviorSubject<string[]>([]);
  loading$ = new BehaviorSubject<boolean>(false);

  private filters: SearchVehiclesRequest = {
    pageNumber: 1,
    pageSize: 10,
  };

  constructor(private vehicleService: VehicleService) {}

  loadVehicles(): void {
    this.loading$.next(true);
    this.vehicleService
      .search(this.filters)
      .pipe(finalize(() => this.loading$.next(false)))
      .subscribe({
        next: (res: SearchVehiclesResponse) => {
          this.vehicles$.next(res.items);
          this.totalCount$.next(res.totalCount);
        },
        error: (err: HttpErrorResponse) =>
          console.error(
            `Failed to fetch vehicles [${err.status}]: ${err.message}`,
            err.error,
          ),
      });
  }

  loadCounts(): void {
    this.vehicleService
      .getTotalCount()
      .subscribe((r: VehicleTotalCountResponse) =>
        this.totalCount$.next(r.totalCount),
      );

    this.vehicleService
      .getWithPhotosCount()
      .subscribe((r: VehicleTotalCountResponse) =>
        this.withPhotosCount$.next(r.totalCount),
      );

    this.vehicleService
      .getWithoutPhotosCount()
      .subscribe((r: VehicleTotalCountResponse) =>
        this.withoutPhotosCount$.next(r.totalCount),
      );
  }

  loadColors(): void {
    this.vehicleService
      .getDistinctColors()
      .subscribe((r: string[]) => this.colors$.next(r));
  }

  applyFilters(filters: Partial<SearchVehiclesRequest>): void {
    this.filters = { ...this.filters, ...filters, pageNumber: 1 };
    this.loadVehicles();
  }

  changePage(page: number): void {
    this.filters.pageNumber = page;
    this.loadVehicles();
  }

  createVehicle(req: CreateVehicleRequest): void {
    this.vehicleService.create(req).subscribe({
      next: () => this.loadVehicles(),
      error: (err: HttpErrorResponse) =>
        console.error(
          `Failed to create vehicle [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  updateVehicle(id: string, req: UpdateVehicleRequest): void {
    this.vehicleService.update(id, req).subscribe({
      next: () => this.loadVehicles(),
      error: (err: HttpErrorResponse) =>
        console.error(
          `Failed to update vehicle [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  deleteVehicle(id: string): void {
    this.vehicleService.delete(id).subscribe({
      next: () => this.loadVehicles(),
      error: (err: HttpErrorResponse) =>
        console.error(
          `Failed to delete vehicle [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  uploadPhoto(vehicleId: string, file: File): void {
    this.vehicleService.uploadPhoto(vehicleId, file).subscribe({
      next: () => this.loadVehicles(),
      error: (err: HttpErrorResponse) =>
        console.error(
          `Failed to upload photo [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  removePhoto(vehicleId: string, photoId: string): void {
    this.vehicleService.removePhoto(vehicleId, photoId).subscribe({
      next: () => this.loadVehicles(),
      error: (err: HttpErrorResponse) =>
        console.error(
          `Failed to remove photo [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  addOrUpdatePortalPackage(
    vehicleId: string,
    req: AddOrUpdateVehiclePortalPackageRequest,
  ): void {
    this.vehicleService.addOrUpdatePortalPackage(vehicleId, req).subscribe({
      next: () => this.loadVehicles(),
      error: (err: HttpErrorResponse) =>
        console.error(
          `Failed to update portal package [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  removePortalPackage(vehicleId: string, portal: Portal): void {
    this.vehicleService.removePortalPackage(vehicleId, portal).subscribe({
      next: () => this.loadVehicles(),
      error: (err: HttpErrorResponse) =>
        console.error(
          `Failed to remove portal package [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }
}
