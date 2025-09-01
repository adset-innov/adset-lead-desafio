import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { VehicleService } from '../../services/vehicle.service';
import { Vehicle, VehicleStats, VehicleFilter, FeatureNames } from '../../models/vehicle.model';

@Component({
  selector: 'app-vehicle-listing',
  templateUrl: './vehicle-listing.component.html',
  styleUrls: ['./vehicle-listing.component.css']
})
export class VehicleListingComponent implements OnInit {
  
  filterForm: FormGroup;
  vehicles: Vehicle[] = [];
  vehicleStats: VehicleStats = { total: 0, withPhotos: 0, withoutPhotos: 0 };
  
  currentPage = 1;
  pageSize = 1;
  
  // Modal properties
  isModalOpen = false;
  isEditMode = false;
  selectedVehicle: Vehicle | null = null;
  
  // Controle de mudanças pendentes
  hasPendingChanges = false;
  pendingVehicles: Vehicle[] = [];
  pendingCount = 0;

  brands = ['Todos', 'Volkswagen', 'Ford', 'Chevrolet', 'Toyota', 'Honda'];
  priceOptions = ['Todos', '0 - 50,000', '50,000 - 100,000', '100,000+'];
  photoOptions = ['Todos', 'Com fotos', 'Sem fotos'];
  featuresOptions = [
    'Todos', 
    'Ar Condicionado', 
    'Alarme', 
    'Airbag', 
    'Freio ABS', 
    'MP3 Player'
  ];
  colorOptions = ['Todos', 'Branco', 'Preto', 'Prata', 'Azul', 'Vermelho'];

  constructor(
    private fb: FormBuilder,
    private vehicleService: VehicleService
  ) {
    this.filterForm = this.fb.group({
      plate: [''],
      brand: [''],
      model: [''],
      yearMin: [null],
      yearMax: [null],
      price: ['Todos'],
      photos: ['Todos'],
      features: ['Todos'],
      color: ['Todos']
    });
  }

  ngOnInit(): void {
    this.loadVehicles();
    this.loadStats();
  }

  loadVehicles(): void {
    this.vehicleService.getVehicles().subscribe(vehicles => {
      this.vehicles = vehicles;
      this.vehicles.forEach((vehicle, index) => {
      });
    });
  }

  loadStats(): void {
    this.vehicleService.getVehicleStats().subscribe(stats => {
      this.vehicleStats = stats;
    });
  }

  onSearch(): void {
    const filter: VehicleFilter = this.filterForm.value;
    
    this.vehicleService.searchVehicles(filter).subscribe({
      next: (vehicles) => {
        this.vehicles = vehicles;
      },
      error: (error) => {
        console.error('Error searching vehicles:', error);
      }
    });
  }

  onClearFilters(): void {
    this.filterForm.reset({
      plate: '',
      brand: '',
      model: '',
      yearMin: null,
      yearMax: null,
      price: 'Todos',
      photos: 'Todos',
      features: 'Todos',
      color: 'Todos'
    });
    this.loadVehicles();
  }

  onRegisterVehicle(): void {
    this.selectedVehicle = null;
    this.isEditMode = false;
    this.isModalOpen = true;
  }

  onExportStock(): void {
    // falta excel
  }

  onSave(): void {
    if (this.pendingVehicles.length === 0) {
      return;
    }
    
    const savePromises = this.pendingVehicles.map(vehicle => {
      if (vehicle.id.startsWith('temp-')) {
        // Novo veículo - criar
        return this.vehicleService.createVehicle(vehicle).toPromise().then(newId => {
          vehicle.id = newId || vehicle.id;
          return vehicle;
        });
      } else {
        // Veículo existente - atualizar
        return this.vehicleService.updateVehicle(vehicle.id, vehicle).toPromise().then(() => vehicle);
      }
    });

    Promise.all(savePromises)
      .then((savedVehicles) => {
        this.pendingVehicles = [];
        this.pendingCount = 0;
        this.hasPendingChanges = false;
        this.loadStats();
      })
      .catch((error) => {
        alert('Erro ao salvar veículos. Verifique o console para mais detalhes.');
      });
  }

  onEditVehicle(vehicle: Vehicle): void {
    this.selectedVehicle = vehicle;
    this.isEditMode = true;
    this.isModalOpen = true;
  }

  onDeleteVehicle(vehicle: Vehicle): void {
    const confirmDelete = confirm(`Tem certeza que deseja excluir o veículo ${vehicle.brand} ${vehicle.model} (${vehicle.plate})?`);
    
    if (confirmDelete) {
      this.vehicleService.deleteVehicle(vehicle.id).subscribe({
        next: () => {
          this.loadVehicles();
          this.loadStats();
        },
        error: (error) => {
          console.error('Error deleting vehicle:', error);
          alert('Erro ao excluir o veículo. Tente novamente.');
        }
      });
    }
  }

  // Modal methods
  onVehicleSave(vehicle: Vehicle): void {
    if (this.isEditMode) {
      // Para edição, adicionar à lista de pendentes
      const existingIndex = this.pendingVehicles.findIndex(v => v.id === vehicle.id);
      if (existingIndex !== -1) {
        this.pendingVehicles[existingIndex] = vehicle;
      } else {
        this.pendingVehicles.push(vehicle);
      }
      
      // Atualizar visualmente na lista (temporário)
      const index = this.vehicles.findIndex(v => v.id === vehicle.id);
      if (index !== -1) {
        this.vehicles[index] = vehicle;
      }
    } else {
      // Para criação, gerar ID temporário e adicionar à lista de pendentes
      vehicle.id = 'temp-' + Date.now();
      this.pendingVehicles.push(vehicle);
      this.vehicles.push(vehicle);
    }
    
    this.pendingCount = this.pendingVehicles.length;
    this.hasPendingChanges = this.pendingCount > 0;
    this.isModalOpen = false;
    this.selectedVehicle = null;
  }

  onModalClose(): void {
    this.isModalOpen = false;
    this.selectedVehicle = null;
  }

  isICarrosActive(vehicle: Vehicle): boolean {
    return vehicle.portal === 'ICars' || vehicle.portal === 'ICarros';
  }

  isWebMotorsActive(vehicle: Vehicle): boolean {
    return vehicle.portal === 'WebMotors';
  }

  isPackageSelected(vehicle: Vehicle, packageName: string): boolean {
    if (!vehicle.portal || !vehicle.package) {
      return false;
    }
    
    return vehicle.package === packageName;
  }

  isBronze(vehicle: Vehicle): boolean {
    return this.isICarrosActive(vehicle) && this.isPackageSelected(vehicle, 'Bronze');
  }

  isDiamante(vehicle: Vehicle): boolean {
    return this.isPackageSelected(vehicle, 'Diamond');
  }

  isPlatinum(vehicle: Vehicle): boolean {
    return this.isICarrosActive(vehicle) && this.isPackageSelected(vehicle, 'Platinum');
  }

  isBasic(vehicle: Vehicle): boolean {
    return this.isWebMotorsActive(vehicle) && this.isPackageSelected(vehicle, 'Basic');
  }

  // Método para converter features numéricas para nomes
  getFeatureNames(features: number[]): string[] {
    if (!features || features.length === 0) {
      return [];
    }
    return features.map(featureId => FeatureNames[featureId] || 'Desconhecido');
  }

  // Método para obter features como string concatenada
  getFeaturesAsString(features: number[]): string {
    const names = this.getFeatureNames(features);
    return names.length > 0 ? names.join(', ') : 'Nenhuma';
  }

  // Método para obter URL da primeira imagem do veículo
  getVehicleImageUrl(vehicle: Vehicle): string {
    if (vehicle.photos && vehicle.photos.length > 0) {
      const firstPhoto = vehicle.photos[0];

      // Garantir que firstPhoto seja uma string válida
      let fileName = '';
      if (typeof firstPhoto === 'string') {
        fileName = firstPhoto;
      } else if (firstPhoto != null) {
        // Se for um objeto, tentar extrair uma propriedade que contenha o nome do arquivo
        fileName = String(firstPhoto);
      }
      
      if (fileName && fileName !== '[object Object]') {
        // Usar endpoint da API para servir as imagens
        const imageUrl = `http://localhost:5062/api/images/${fileName}`;
        return imageUrl;
      }
    }
    return '';
  }
}
