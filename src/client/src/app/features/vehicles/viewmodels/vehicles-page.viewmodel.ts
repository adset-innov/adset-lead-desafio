import { HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { SearchVehiclesRequest } from '../../../core/dtos/requests/search-vehicles-request';
import { Vehicle } from '../../../core/models/vehicle';
import { finalize } from 'rxjs/operators';
import { SearchVehiclesResponse } from '../../../core/dtos/responses/search-vehicles-response';
import { VehicleTotalCountResponse } from '../../../core/dtos/responses/vehicle-total-count-response';
import { CreateVehicleRequest } from '../../../core/dtos/requests/create-vehicle-request';
import { UpdateVehicleRequest } from '../../../core/dtos/requests/update-vehicle-request';
import { AddOrUpdateVehiclePortalPackageRequest } from '../../../core/dtos/requests/add-or-update-vehicle-portal-package-request';
import { Portal } from '../../../core/enums/portal';
import { VehicleService } from '../../../core/services/vehicle.service';

@Injectable()
export class VehiclesPageViewModel {
  vehicles$ = new BehaviorSubject<Vehicle[]>([]);
  totalCount$ = new BehaviorSubject<number>(0);
  withPhotosCount$ = new BehaviorSubject<number>(0);
  withoutPhotosCount$ = new BehaviorSubject<number>(0);
  colors$ = new BehaviorSubject<string[]>([]);
  loading$ = new BehaviorSubject<boolean>(false);

  pageSize$ = new BehaviorSubject<number>(10);

  filters: SearchVehiclesRequest = {
    pageNumber: 1,
    pageSize: 10,
  };

  constructor(private vehicleService: VehicleService) {}

  loadVehicles(): void {
    console.log('[VehiclesVM] loadVehicles called with filters:', this.filters);
    this.loading$.next(true);

    this.vehicleService
      .search(this.filters)
      .pipe(
        finalize(() => {
          this.loading$.next(false);
          console.log('[VehiclesVM] loadVehicles finished');
        }),
      )
      .subscribe({
        next: (res: SearchVehiclesResponse) => {
          console.log('[VehiclesVM] vehicles received:', res.vehicles);
          console.log('[VehiclesVM] totalCount received:', res.totalCount);

          this.vehicles$.next(res.vehicles);
          this.totalCount$.next(res.totalCount);
        },
        error: (err: HttpErrorResponse) =>
          console.error(
            `[VehiclesVM] Failed to fetch vehicles [${err.status}]: ${err.message}`,
            err.error,
          ),
      });
  }

  loadCounts(): void {
    console.log('[VehiclesVM] loadCounts called');

    this.vehicleService
      .getTotalCount()
      .subscribe((r: VehicleTotalCountResponse) => {
        console.log('[VehiclesVM] totalCount response:', r.totalCount);
        this.totalCount$.next(r.totalCount);
      });

    this.vehicleService
      .getWithPhotosCount()
      .subscribe((r: VehicleTotalCountResponse) => {
        console.log('[VehiclesVM] withPhotosCount response:', r.totalCount);
        this.withPhotosCount$.next(r.totalCount);
      });

    this.vehicleService
      .getWithoutPhotosCount()
      .subscribe((r: VehicleTotalCountResponse) => {
        console.log('[VehiclesVM] withoutPhotosCount response:', r.totalCount);
        this.withoutPhotosCount$.next(r.totalCount);
      });
  }

  loadColors(): void {
    console.log('[VehiclesVM] loadColors called');

    this.vehicleService.getDistinctColors().subscribe({
      next: (r) => {
        console.log('[VehiclesVM] distinct colors response:', r);
        this.colors$.next(r.colors);
      },
      error: (err) => {
        console.error('[VehiclesVM] Failed to load colors', err);
      },
    });
  }

  applyFilters(filters: Partial<SearchVehiclesRequest>, reset = false): void {
    console.log('[VehiclesVM] applyFilters called with:', filters);

    if (reset) {
      this.filters = {
        pageNumber: 1,
        pageSize: this.filters.pageSize ?? 10,
        ...filters,
      };
    } else {
      this.filters = { ...this.filters, ...filters, pageNumber: 1 };
    }

    this.loadVehicles();
  }

  changePage(page: number): void {
    console.log('[VehiclesVM] changePage called with:', page);

    this.filters.pageNumber = page;
    this.loadVehicles();
  }

  changePageSize(size: number): void {
    console.log('[VehiclesVM] changePageSize called with:', size);

    this.filters.pageSize = size;
    this.filters.pageNumber = 1;
    this.pageSize$.next(size);
    this.loadVehicles();
  }

  applySort(sort: { field: string; direction: 'asc' | 'desc' | null }): void {
    console.log('[VehiclesVM] applySort called with:', sort);

    this.filters = {
      ...this.filters,
      sortField: sort.field,
      sortDirection: sort.direction ?? undefined,
      pageNumber: 1,
    };

    this.loadVehicles();
  }

  createVehicle(req: CreateVehicleRequest): void {
    console.log('[VehiclesVM] createVehicle called with:', req);

    this.vehicleService.create(req).subscribe({
      next: () => {
        console.log('[VehiclesVM] vehicle created successfully');
        this.loadVehicles();
      },
      error: (err: HttpErrorResponse) =>
        console.error(
          `[VehiclesVM] Failed to create vehicle [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  updateVehicle(id: string, req: UpdateVehicleRequest): void {
    console.log('[VehiclesVM] updateVehicle called with:', id, req);

    this.vehicleService.update(id, req).subscribe({
      next: () => {
        console.log('[VehiclesVM] vehicle updated successfully');
        this.loadVehicles();
      },
      error: (err: HttpErrorResponse) =>
        console.error(
          `[VehiclesVM] Failed to update vehicle [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  deleteVehicle(id: string): void {
    console.log('[VehiclesVM] deleteVehicle called with:', id);

    this.vehicleService.delete(id).subscribe({
      next: () => {
        console.log('[VehiclesVM] vehicle deleted successfully');
        this.loadVehicles();
      },
      error: (err: HttpErrorResponse) =>
        console.error(
          `[VehiclesVM] Failed to delete vehicle [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  uploadPhoto(vehicleId: string, file: File): void {
    console.log(
      '[VehiclesVM] uploadPhoto called with vehicleId:',
      vehicleId,
      'file:',
      file,
    );

    this.vehicleService.uploadPhoto(vehicleId, file).subscribe({
      next: () => {
        console.log('[VehiclesVM] photo uploaded successfully');
        this.loadVehicles();
      },
      error: (err: HttpErrorResponse) =>
        console.error(
          `[VehiclesVM] Failed to upload photo [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  removePhoto(vehicleId: string, photoId: string): void {
    console.log(
      '[VehiclesVM] removePhoto called with vehicleId:',
      vehicleId,
      'photoId:',
      photoId,
    );

    this.vehicleService.removePhoto(vehicleId, photoId).subscribe({
      next: () => {
        console.log('[VehiclesVM] photo removed successfully');
        this.loadVehicles();
      },
      error: (err: HttpErrorResponse) =>
        console.error(
          `[VehiclesVM] Failed to remove photo [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  addOrUpdatePortalPackage(
    vehicleId: string,
    req: AddOrUpdateVehiclePortalPackageRequest,
  ): void {
    console.log(
      '[VehiclesVM] addOrUpdatePortalPackage called with:',
      vehicleId,
      req,
    );

    this.vehicleService.addOrUpdatePortalPackage(vehicleId, req).subscribe({
      next: () => {
        console.log('[VehiclesVM] portal package updated successfully');
        this.loadVehicles();
      },
      error: (err: HttpErrorResponse) =>
        console.error(
          `[VehiclesVM] Failed to update portal package [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }

  removePortalPackage(vehicleId: string, portal: Portal): void {
    console.log(
      '[VehiclesVM] removePortalPackage called with:',
      vehicleId,
      portal,
    );

    this.vehicleService.removePortalPackage(vehicleId, portal).subscribe({
      next: () => {
        console.log('[VehiclesVM] portal package removed successfully');
        this.loadVehicles();
      },
      error: (err: HttpErrorResponse) =>
        console.error(
          `[VehiclesVM] Failed to remove portal package [${err.status}]: ${err.message}`,
          err.error,
        ),
    });
  }
}
