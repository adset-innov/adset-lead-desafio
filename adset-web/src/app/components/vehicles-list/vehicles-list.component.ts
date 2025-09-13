import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { PortalPackageSelection } from 'src/app/models/PortalPackageSelection';
import { SearchVehiclesFilter } from 'src/app/models/SearchVehiclesFilter';
import { Vehicle } from 'src/app/models/VehicleModel';
import { VehicleService } from 'src/app/services/vehicle.service';
import { environment } from 'src/environment/environment';

@Component({
  selector: 'app-vehicles-list',
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.scss']
})
export class VehicleListComponent {
  showForm = false;
  selectedVehicle: Vehicle | null = null;
  environment = environment;
  @Input() filter!: SearchVehiclesFilter;
  @Input() vehicles: Vehicle[] = [];
  @Output() editVehicle = new EventEmitter<Vehicle>();
  packages: PortalPackageSelection[] = [
    { portal: 'iCarros', packageName: 'Diamante Feirão', selected: false },
    { portal: 'WebMotors', packageName: 'Básico', selected: true }
  ];
  totalVehicles = 0;
  vehiclesWithPhotos = 0;
  vehiclesWithoutPhotos = 0;

  sortField: string | null = null;
  sortDirection: 'asc' | 'desc' | null = null;
  currentPage = 1;
  pageSize = 20;


  constructor(private vehicleService: VehicleService) {}

   ngOnInit(): void {
    this.search();
  }

    ngOnChanges(changes: SimpleChanges) {
    if (changes['filter'] && this.filter) {
      this.search();
    }
  }

  search() {
    this.vehicleService.search(this.filter || {}, 1, 20).subscribe(res => {
    });
  }

  sortBy(field: string) {
    if (this.sortField === field) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortField = field;
      this.sortDirection = 'asc';
    }
  }

  isActive(field: string, direction: 'asc' | 'desc'): boolean {
    return this.sortField === field && this.sortDirection === direction;
  }

  openNewForm() {
    this.selectedVehicle = null;
    this.showForm = true;
  }

  onEdit(vehicle: Vehicle) {
  this.editVehicle.emit(vehicle);
  }

  deleteVehicle(id: number) {
    if (confirm("Tem certeza que deseja excluir este veículo?")) {
      this.vehicleService.delete(id).subscribe({
        next: () => {
          this.vehicles = this.vehicles.filter(v => v.id !== id);
        },
        error: (err) => {
          console.error("Erro ao excluir veículo", err);
        }
      });
    }
  }

  pageChanged(page: number) {
  this.currentPage = page;
  this.search();
}
}
