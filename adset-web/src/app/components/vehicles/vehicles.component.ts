import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Vehicle } from 'src/app/models/vehicle.model';
import { VehicleService } from 'src/app/services/vehicle.service';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.scss']
})
export class VehiclesComponent implements OnInit {
   showForm = false;
   selectedVehicle: Vehicle | null = null;
   filtrosVisiveis = true;
   vehicles = [
    { id: 1, marca: 'VW', modelo: 'Golf Variant', ano: 2020, preco: 103900, fotos: 8, placa: 'AAA-0102', km: 25000, cor: 'Branco', image: 'https://via.placeholder.com/200x150.png?text=Carro' },
    { id: 2, marca: 'Fiat', modelo: 'Argo', ano: 2022, preco: 75000, fotos: 5, placa: 'BBB-0203', km: 12000, cor: 'Preto', image: 'https://via.placeholder.com/200x150.png?text=Carro' }
  ];
  sortField: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';

  constructor(private fb: FormBuilder, private svc: VehicleService) {}

  ngOnInit(): void {
  }

  toggleFiltros() {
    this.filtrosVisiveis = !this.filtrosVisiveis;
  }

  sortBy(field: string) {
  if (this.sortField === field) {
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
  } else {
    this.sortField = field;
    this.sortDirection = 'asc';
  }

  this.vehicles.sort((a, b) => {
    const valueA = (a as any)[field];
    const valueB = (b as any)[field];

    if (valueA < valueB) return this.sortDirection === 'asc' ? -1 : 1;
    if (valueA > valueB) return this.sortDirection === 'asc' ? 1 : -1;
    return 0;
  });
}
  openNewForm() {
    this.selectedVehicle = null;
    this.showForm = true;
  }

  edit(vehicleId: number) {
  this.svc.getById(vehicleId).subscribe(vehicle => {
    this.selectedVehicle = vehicle;
    this.showForm = true;
  });
}
  onSaved(vehicle: Vehicle) {
    this.showForm = false;
  }

  closeForm() {
    this.showForm = false;
  }

}
