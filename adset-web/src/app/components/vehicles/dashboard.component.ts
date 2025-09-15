import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { VehicleListComponent } from "../vehicles-list/vehicles-list.component";
import { Vehicle } from '../../models/VehicleModel';
import { UpdateVehiclePortalPackages } from '../../models/PortalPackageSelection';
import { SearchVehiclesFilter } from '../../models/SearchVehiclesFilter';
import { VehicleService } from '../../services/vehicle.service';

@Component({
  selector: 'app-vehicles',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
    showForm = false;
    selectedVehicle: Vehicle | null = null;
    selectedPackages!: UpdateVehiclePortalPackages;
    dropdownOpen = false;
    filtrosVisiveis = true;
    vehicles: Vehicle[] = [];
    sortField: string = '';
    sortDirection: 'asc' | 'desc' = 'asc';
    filterForm!: FormGroup;
    currentFilter!: SearchVehiclesFilter;
    totalVehicles = 0;
    vehiclesWithPhotos = 0;
    vehiclesWithoutPhotos = 0;
    years: number[] = [];
    start = 2000;
    end = 2024;
    currentPage = 1;
    pageSize = 20;

    opcionaisList: any[] = [];
    selectedOpcionais: number[] = [];
    priceOptions = [
      { label: '10 mil a 50 mil', value: '10-50' },
      { label: '50 mil a 90 mil', value: '50-90' },
      { label: '+90 mil', value: '90+' }
    ];

    colors: string[] = [
      'Preto',
      'Branco',
      'Prata',
      'Cinza',
      'Vermelho',
      'Azul',
      'Verde',
      'Amarelo',
      'Marrom',
      'Bege',
    ];

  constructor(private fb: FormBuilder, private vehicleService: VehicleService) {}

  ngOnInit(): void {
    this.years = Array.from({ length: this.end - this.start + 1 }, (_, i) => this.start + i);
     this.filterForm = this.fb.group({
      plate: [''],
      make: [''],
      model: [''],
      yearMin: [null],
      yearMax: [null],
      km: [null],
      price: [''],
      images: [null],
      optionais: [''],
      colors: [[]]
    })
    this.currentFilter = {};

     this.vehicleService.getOpcionais().subscribe(data => {
     this.opcionaisList = data;
     this.onSearch();
  });
  }

   get selectedOpcionaisLabels(): string[] {
    return this.opcionaisList
      .filter(opc => this.selectedOpcionais.includes(opc.id))
      .map(opc => opc.nome);
  }

  toggleDropdown() {
    this.dropdownOpen = !this.dropdownOpen;
  }

 onOpcionalChange(event: any, opc: any) {
  if (event.target.checked) {
    this.selectedOpcionais.push(opc.id);
  } else {
    this.selectedOpcionais = this.selectedOpcionais.filter(id => id !== opc.id);
  }

  this.filterForm.patchValue({ optionals: this.selectedOpcionais });
}

  onEditVehicle(vehicle: Vehicle) {
    this.selectedVehicle = vehicle;
    this.showForm = true;
}

  onSearch(): void {
  const formValue = this.filterForm.value;
  const filter: SearchVehiclesFilter = {
    plate: formValue.plate?.trim() || null,
    make: formValue.make?.trim() || null,
    model: formValue.model?.trim() || null,
    yearMin: formValue.yearMin || null,
    yearMax: formValue.yearMax || null,
    km: formValue.km || null,
    price: formValue.price?.trim() || null,
    images: formValue.images ?? null,
    colors: (formValue.colors || []).filter((c: string) => c && c.trim() !== ''),
    optionals: this.selectedOpcionais
  };

  this.vehicleService.search(filter, 1, 20).subscribe(res => {
    this.vehicles = res.items;
      this.vehicles = res.items;
      this.totalVehicles = this.vehicles.length;
      this.vehiclesWithPhotos = this.vehicles.filter(v => v.imageUrls && v.imageUrls.length > 0).length;
      this.vehiclesWithoutPhotos = this.vehicles.filter(v => !v.imageUrls || v.imageUrls.length === 0).length;
  });
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

  onSaved(vehicle: Vehicle) {
    this.showForm = false;
    this.onSearch();
  }

  closeForm() {
    this.showForm = false;
  }

  onPackagesSelected(event: UpdateVehiclePortalPackages) {
    this.selectedPackages = event;
   }

  savePackages() {
    if (!this.selectedPackages || this.selectedPackages.portalSelections.length === 0) {
      alert('Selecione um pacote antes de salvar.');
      return;
    }

    console.log(this.selectedPackages);
    this.vehicleService.updatePackages(this.selectedPackages).subscribe({
      next: () => alert('Pacotes salvos com sucesso!'),
      error: (err) => console.error('Erro ao salvar pacotes:', err)
    });
  }
}
