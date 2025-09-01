import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Vehicle } from '../../models/vehicle.model';

@Component({
  selector: 'app-vehicle-modal',
  templateUrl: './vehicle-modal.component.html',
  styleUrls: ['./vehicle-modal.component.css']
})
export class VehicleModalComponent implements OnInit {
  @Input() isOpen = false;
  @Input() vehicle: Vehicle | null = null;
  @Input() isEditMode = false;
  
  @Output() close = new EventEmitter<void>();
  @Output() save = new EventEmitter<Vehicle>();
  
  vehicleForm: FormGroup;
  years: number[] = [];
  
  // Opções Check-box, select modal
  selectedPortal: string = '';
  iCarrosPackages = ['Bronze', 'Diamond', 'Platinum'];
  webMotorsPackages = ['Basic'];

  // Opcionais disponíveis
  availableFeatures = [
    { id: 1, name: 'Ar Condicionado', selected: false },
    { id: 2, name: 'Alarme', selected: false },
    { id: 3, name: 'Airbag', selected: false },
    { id: 4, name: 'Freio ABS', selected: false },
    { id: 5, name: 'MP3 Player', selected: false }
  ];

  constructor(private fb: FormBuilder) {
    this.vehicleForm = this.createForm();
    this.generateYears();
  }

  ngOnInit(): void {
    if (this.vehicle && this.isEditMode) {
      // Definir o portal selecionado ANTES de popular o formulário
      this.selectedPortal = this.vehicle.portal || '';
      this.populateForm();
      // No modo edição, ajustar validações para portal e package
      this.adjustValidationsForEditMode();
    } else {
      // No modo cadastro, portal e package são obrigatórios
      this.vehicleForm.get('portal')?.setValidators([Validators.required]);
      this.vehicleForm.get('package')?.setValidators([Validators.required]);
      this.vehicleForm.get('portal')?.updateValueAndValidity();
      this.vehicleForm.get('package')?.updateValueAndValidity();
    }
    
    // Debug: Verificar status do formulário no modo edição
    if (this.isEditMode) {
      this.vehicleForm.statusChanges.subscribe(status => {
        console.log('Form status:', status);
        if (status === 'INVALID') {
          this.logFormErrors();
        }
      });
    }
  }

  private createForm(): FormGroup {
    return this.fb.group({
      plate: ['', [
        Validators.required, 
        Validators.pattern(/^[A-Z]{3}-?\d{4}$/)
      ]],
      brand: ['', [
        Validators.required,
        Validators.minLength(2)
      ]],
      model: ['', [
        Validators.required,
        Validators.minLength(2)
      ]],
      year: [null, [
        Validators.required,
        Validators.min(2000),
        Validators.max(new Date().getFullYear() + 1)
      ]],
      km: [null, [
        Validators.min(0),
        Validators.max(500000)
      ]],
      color: ['', [
        Validators.required,
        Validators.minLength(3)
      ]],
      price: [null, [
        Validators.required,
        Validators.min(0.01)
      ]],
      imageUrl: ['', [
        Validators.pattern(/^$|^https?:\/\/.+/)
      ]],
      portal: [''],
      package: ['']
    });
  }

  private generateYears(): void {
    const currentYear = new Date().getFullYear();
    for (let year = currentYear + 1; year >= 2000; year--) {
      this.years.push(year);
    }
  }

  private populateForm(): void {
    if (this.vehicle) {
      this.vehicleForm.patchValue({
        plate: this.vehicle.plate,
        brand: this.vehicle.brand,
        model: this.vehicle.model,
        year: this.vehicle.year,
        km: this.vehicle.km,
        color: this.vehicle.color,
        price: this.vehicle.price,
        imageUrl: this.vehicle.imageUrl || '',
        portal: this.vehicle.portal || '',
        package: this.vehicle.package || ''
      });

      // Marcar os opcionais selecionados
      if (this.vehicle.features && this.vehicle.features.length > 0) {
        this.availableFeatures.forEach(feature => {
          feature.selected = this.vehicle!.features.includes(feature.id);
        });
      }
      
      // Forçar atualização da validação após popular os dados
      this.vehicleForm.updateValueAndValidity();
      
      // Debug: Verificar se o formulário está válido após popular
      setTimeout(() => {
        console.log('Form valid after populate:', this.vehicleForm.valid);
        if (!this.vehicleForm.valid) {
          this.logFormErrors();
        }
      }, 100);
    }
  }

  onClose(): void {
    this.vehicleForm.reset();
    // Limpar seleções dos checkboxes
    this.availableFeatures.forEach(feature => {
      feature.selected = false;
    });
    this.close.emit();
  }

  onSave(): void {
    // Validação específica para cada modo
    const isValidForSave = this.isEditMode ? this.isValidForEdit() : this.vehicleForm.valid;
    
    if (isValidForSave) {
      const formValue = this.vehicleForm.value;
      
      // Obter features selecionadas
      const selectedFeatures = this.availableFeatures
        .filter(feature => feature.selected)
        .map(feature => feature.id);
      
      // Preparar dados para salvar
      const vehicleData: Vehicle = {
        id: this.isEditMode && this.vehicle?.id ? this.vehicle.id : '', // Será definido no componente pai
        
        // API properties
        plate: formValue.plate?.toUpperCase() || '',
        brand: formValue.brand || '',
        model: formValue.model || '',
        year: formValue.year || new Date().getFullYear(),
        color: formValue.color || '',
        price: parseFloat(formValue.price) || 0,
        km: formValue.km || undefined,
        features: selectedFeatures.length > 0 ? selectedFeatures : (this.vehicle?.features || [1]), // Manter features originais se não alteradas
        photos: JSON.stringify([]),
        portal: formValue.portal || this.vehicle?.portal || 'WebMotors',
        package: formValue.package || this.vehicle?.package || 'Basic',
        
        // Calculated properties
        imageUrl: formValue.imageUrl || null,
        photosCount: this.vehicle?.photosCount || 0,
        hasPhotos: this.vehicle?.hasPhotos || false,
        featuresCount: selectedFeatures.length > 0 ? selectedFeatures.length : (this.vehicle?.featuresCount || 0)
      };

      this.save.emit(vehicleData);
      this.onClose();
    } else {
      this.markFormGroupTouched();
    }
  }

  // Validação específica para modo edição
  private isValidForEdit(): boolean {
    const essentialFields = ['plate', 'brand', 'model', 'year', 'color', 'price'];
    return essentialFields.every(fieldName => {
      const control = this.vehicleForm.get(fieldName);
      return control?.valid;
    });
  }

  onPortalChange(portal: string): void {
    if (this.selectedPortal === portal) {
      this.selectedPortal = '';
      this.vehicleForm.patchValue({
        portal: '',
        package: ''
      });
    } else {
      this.selectedPortal = portal;
      this.vehicleForm.patchValue({
        portal: portal,
        package: ''
      });
    }
    
    this.vehicleForm.get('portal')?.updateValueAndValidity();
    this.vehicleForm.get('package')?.updateValueAndValidity();
  }

  getAvailablePackages(): string[] {
    switch (this.selectedPortal) {
      case 'ICars':
      case 'ICarros':
        return this.iCarrosPackages;
      case 'WebMotors':
        return this.webMotorsPackages;
      default:
        return [];
    }
  }

  getPackageLabel(packageValue: string): string {
    const packageLabels: { [key: string]: string } = {
      'Bronze': 'Bronze',
      'Diamond': 'Diamante',
      'Platinum': 'Platina',
      'Basic': 'Básico'
    };
    
    return packageLabels[packageValue] || packageValue;
  }

  isPortalSelected(portal: string): boolean {
    return this.selectedPortal === portal;
  }

  onFeatureChange(featureId: number, checked: boolean): void {
    const feature = this.availableFeatures.find(f => f.id === featureId);
    if (feature) {
      feature.selected = checked;
    }
  }

  getSelectedFeatures(): string[] {
    return this.availableFeatures
      .filter(feature => feature.selected)
      .map(feature => feature.name);
  }

  private markFormGroupTouched(): void {
    Object.keys(this.vehicleForm.controls).forEach(key => {
      const control = this.vehicleForm.get(key);
      control?.markAsTouched();
      
      if (control?.errors) {
        console.log(`Campo ${key} tem erros:`, control.errors);
      }
    });
  }

  // Método para debug de erros do formulário
  private logFormErrors(): void {
    Object.keys(this.vehicleForm.controls).forEach(key => {
      const control = this.vehicleForm.get(key);
      if (control?.invalid) {
        console.log(`Campo ${key} está inválido:`, control.errors);
      }
    });
  }

  // Ajustar validações para o modo edição
  private adjustValidationsForEditMode(): void {
    // No modo edição, portal e package não são obrigatórios se já existem
    if (this.vehicle?.portal && this.vehicle?.package) {
      this.vehicleForm.get('portal')?.clearValidators();
      this.vehicleForm.get('package')?.clearValidators();
      this.vehicleForm.get('portal')?.updateValueAndValidity();
      this.vehicleForm.get('package')?.updateValueAndValidity();
    }
  }

  // Verifica se o formulário deve estar desabilitado
  isFormDisabled(): boolean {
    if (this.isEditMode) {
      // No modo edição, verificar apenas campos essenciais
      const essentialFields = ['plate', 'brand', 'model', 'year', 'color', 'price'];
      return essentialFields.some(fieldName => {
        const control = this.vehicleForm.get(fieldName);
        return control?.invalid;
      });
    } else {
      // No modo cadastro, usar validação completa
      return this.vehicleForm.invalid;
    }
  }

  // Métodos auxiliares para validação
  getFieldError(fieldName: string): string {
    const field = this.vehicleForm.get(fieldName);
    
    if (field?.errors && field.touched) {
      if (field.errors['required']) {
        return `${this.getFieldLabel(fieldName)} é obrigatório`;
      }
      if (field.errors['pattern']) {
        return `${this.getFieldLabel(fieldName)} tem formato inválido`;
      }
      if (field.errors['min']) {
        return `${this.getFieldLabel(fieldName)} deve ser maior que ${field.errors['min'].min}`;
      }
      if (field.errors['max']) {
        return `${this.getFieldLabel(fieldName)} deve ser menor que ${field.errors['max'].max}`;
      }
      if (field.errors['minlength']) {
        return `${this.getFieldLabel(fieldName)} deve ter pelo menos ${field.errors['minlength'].requiredLength} caracteres`;
      }
    }
    
    return '';
  }

  private getFieldLabel(fieldName: string): string {
    const labels: { [key: string]: string } = {
      placa: 'Placa',
      marca: 'Marca',
      modelo: 'Modelo',
      ano: 'Ano',
      cor: 'Cor',
      preco: 'Preço',
      opcionais: 'Opcionais',
      imagemUrl: 'URL da Imagem'
    };
    
    return labels[fieldName] || fieldName;
  }

  // Método para obter lista de features do veículo
  getVehicleFeaturesList(): string[] {
    if (!this.vehicle || !this.vehicle.features) {
      return [];
    }

    const featuresArray: string[] = [];
    
    try {
      // Features agora é array de números diretamente
      const featuresData = this.vehicle.features;
      
      if (Array.isArray(featuresData)) {
        // Mapeia IDs numéricos para nomes de features
        featuresData.forEach((featureId: number) => {
          const featureName = this.getFeatureName(featureId);
          if (featureName) {
            featuresArray.push(featureName);
          }
        });
      }
    } catch (error) {
      console.log('Error processing features:', error);
    }
    
    // Remove duplicatas e retorna
    return [...new Set(featuresArray)];
  }

  private getFeatureName(featureValue: number): string | null {
    const featureMap: { [key: number]: string } = {
      1: 'Ar Condicionado',
      2: 'Alarme',
      3: 'Airbag',
      4: 'Freio ABS',
      5: 'MP3 Player'
    };
    
    return featureMap[featureValue] || null;
  }
}