import { Component, OnInit } from '@angular/core';
import { Vehicle } from 'src/app/models/vehicle.model';

@Component({
  selector: 'app-vehicles-list',
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.scss']
})
export class VehicleListComponent {
  showForm = false;
  selectedVehicle: Vehicle | null = null;
  vehicles: Vehicle[] = [
    {
      id: 1,
      make: 'Volkswagen',
      model: 'Golf Variant',
      year: 2018,
      plate: 'AAA-0102',
      km: 25000,
      color: 'Branco',
      price: 103900,
      images: ['assets/car1.jpg']
    }
  ];

  sortField: string | null = null;
  sortDirection: 'asc' | 'desc' | null = null;

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
}
