import { HttpClient } from '@angular/common/http';
import {
  Component,
  EventEmitter,
  Inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { CarsService } from 'src/app/api/carServices/cars.service';
import { DefaultReturn } from 'src/app/shared/models/DefaultReturn';

export enum EnumCor {
  Branco = 0,
  Preto = 1,
  Prata = 2,
  Cinza = 3,
  Vermelho = 4,
  Azul = 5,
  Verde = 6,
  Amarelo = 7,
  Marrom = 8,
  Laranja = 9,
  Outra = 99,
}

// Definir enum para portais
export enum EnumPortal {
  ICarros = 'ICarros',
  WebMotors = 'WebMotors',
}

// Definir categorias fixas
export enum EnumCategoria {
  Basico,
  Bronze,
  Diamante,
  Platinum,
}

export enum EnumOptionsCar {
  None = 0,
  ArCondicionado = 1,
  Alarme = 2,
  Airbag = 3,
  FreioABS = 4,
}

// DTOs
export interface CategoryPortalDTO {
  nomePortal: string;
  categoria: number;
}

export interface CarsDTO {
  id: number;
  marca: string;
  modelo: string;
  ano: number;
  placa: string;
  km: number;
  cor: EnumCor;
  preco: number;
  opcionaisVeiculo: EnumOptionsCar[];
  portaisCategorias: CategoryPortalDTO[];
}
@Component({
  selector: 'app-post-vehicle',
  templateUrl: './post-vehicle.component.html',
  styleUrls: ['./post-vehicle.component.css'],
})
export class PostVehicleComponent implements OnInit {
  @Input() carData: CarsDTO | null = null;
  @Output() onSubmit = new EventEmitter<CarsDTO>();
  @Output() onCancel = new EventEmitter<void>();

  carForm: FormGroup;

  // Enums para uso no template
  EnumCor = EnumCor;
  EnumOptionsCar = EnumOptionsCar;

  portals = [
    { value: EnumPortal.ICarros, label: 'ICarros' },
    { value: EnumPortal.WebMotors, label: 'WebMotors' },
  ];

  categorias = [
    { value: EnumCategoria.Basico, label: 'Básico' },
    { value: EnumCategoria.Bronze, label: 'Bronze' },
    { value: EnumCategoria.Diamante, label: 'Diamante' },
    { value: EnumCategoria.Platinum, label: 'Platinum' },
  ];

  // Arrays para os dropdowns
  cores = [
    { value: EnumCor.Branco, label: 'Branco' },
    { value: EnumCor.Preto, label: 'Preto' },
    { value: EnumCor.Prata, label: 'Prata' },
    { value: EnumCor.Cinza, label: 'Cinza' },
    { value: EnumCor.Vermelho, label: 'Vermelho' },
    { value: EnumCor.Azul, label: 'Azul' },
    { value: EnumCor.Verde, label: 'Verde' },
    { value: EnumCor.Amarelo, label: 'Amarelo' },
    { value: EnumCor.Marrom, label: 'Marrom' },
    { value: EnumCor.Laranja, label: 'Laranja' },
    { value: EnumCor.Outra, label: 'Outra' },
  ];

  opcionais = [
    { value: EnumOptionsCar.ArCondicionado, label: 'Ar Condicionado' },
    { value: EnumOptionsCar.Alarme, label: 'Alarme' },
    { value: EnumOptionsCar.Airbag, label: 'Airbag' },
    { value: EnumOptionsCar.FreioABS, label: 'Freio ABS' },
  ];

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    @Inject(MAT_DIALOG_DATA) public dataextra: any,
    private dialogRef: MatDialogRef<PostVehicleComponent>,
    private carsService: CarsService
  ) {
    this.carForm = this.createForm();
  }

 ngOnInit(): void {
  if (this.dataextra.carData && this.dataextra.carData > 0) {
    this.carsService.getVehicleById(this.dataextra.carData).subscribe({
      next: (car:DefaultReturn) => {
        this.populateForm(car.data);
      },
      error: (err) => {
        console.error('Erro ao buscar veículo:', err);
      },
    });
  } else if (this.carData) {
    this.populateForm(this.carData);
  }
}

getCarById(id: number): Observable<CarsDTO> {
  return this.http.get<CarsDTO>(`/api/cars/${id}`);
}

  createForm(): FormGroup {
    return this.fb.group({
      id: [0],
      marca: ['', [Validators.required, Validators.minLength(2)]],
      modelo: ['', [Validators.required, Validators.minLength(2)]],
      ano: [
        new Date().getFullYear(),
        [
          Validators.required,
          Validators.min(1900),
          Validators.max(new Date().getFullYear() + 1),
        ],
      ],
      placa: [
        '',
        [
          Validators.required,
          Validators.pattern(/^[A-Z]{3}[0-9]{4}$|^[A-Z]{3}[0-9][A-Z][0-9]{2}$/),
        ],
      ],
      km: [0, [Validators.min(0)]],
      cor: [EnumCor.Branco, Validators.required],
      preco: [0, [Validators.required, Validators.min(0)]],
      opcionaisVeiculo: this.fb.array([]),
      portal: [EnumPortal.ICarros, Validators.required],
      categoriaPortal: [EnumCategoria.Bronze, Validators.required],
    });
  }

  populateForm(data: CarsDTO): void {
    this.carForm.patchValue({
      id: data.id,
      marca: data.marca,
      modelo: data.modelo,
      ano: data.ano,
      placa: data.placa,
      km: data.km,
      cor: data.cor,
      preco: data.preco,
    });

    const opcionaisArray = this.carForm.get('opcionaisVeiculo') as FormArray;
    opcionaisArray.clear();
    data.opcionaisVeiculo.forEach((opcional) => {
      opcionaisArray.push(this.fb.control(opcional));
    });

    const portaisArray = this.carForm.get('portaisCategorias') as FormArray;
    portaisArray.clear();
    data.portaisCategorias.forEach((portal) => {
      portaisArray.push(
        this.fb.group({
          nomePortal: [portal.nomePortal, Validators.required],
          categoria: [portal.categoria, Validators.required],
        })
      );
    });
  }

  get opcionaisVeiculoArray(): FormArray {
    return this.carForm.get('opcionaisVeiculo') as FormArray;
  }

  get portaisCategoriasArray(): FormArray {
    return this.carForm.get('portaisCategorias') as FormArray;
  }

  onOpcionalChange(event: any, opcional: EnumOptionsCar): void {
    const opcionaisArray = this.opcionaisVeiculoArray;

    if (event.target.checked) {
      opcionaisArray.push(this.fb.control(opcional));
    } else {
      const index = opcionaisArray.controls.findIndex(
        (x) => x.value === opcional
      );
      if (index !== -1) {
        opcionaisArray.removeAt(index);
      }
    }
  }

  isOpcionalSelected(opcional: EnumOptionsCar): boolean {
    return this.opcionaisVeiculoArray.controls.some(
      (control) => control.value === opcional
    );
  }

  addPortalCategoria(): void {
    const portaisArray = this.portaisCategoriasArray;
    portaisArray.push(
      this.fb.group({
        nomePortal: ['', Validators.required],
        categoria: [0, Validators.required],
      })
    );
  }

  removePortalCategoria(index: number): void {
    this.portaisCategoriasArray.removeAt(index);
  }

  onSubmitForm(): void {
    const formValue = this.carForm.value;
    const payload: CarsDTO = {
      id: formValue.id,
      marca: formValue.marca,
      modelo: formValue.modelo,
      ano: formValue.ano,
      placa: formValue.placa,
      km: formValue.km,
      cor: Number(formValue.cor),
      preco: formValue.preco,
      opcionaisVeiculo: formValue.opcionaisVeiculo,
      portaisCategorias: [
        {
          nomePortal: formValue.portal,
          categoria: Number(formValue.categoriaPortal),
        },
      ],
    };

    this.carsService.PutVehicle(payload).subscribe({
      next: () => {
        this.dialogRef.close(true);
      },
      error: (err) => {
        console.error('Erro ao enviar:', err);
        
      },
    });
  }

  onCancelForm(): void {
    this.onCancel.emit();
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach((key) => {
      const control = formGroup.get(key);
      control?.markAsTouched();

      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }

  getFieldError(fieldName: string): string | null {
    const field = this.carForm.get(fieldName);
    if (field && field.errors && field.touched) {
      if (field.errors['required']) return `${fieldName} é obrigatório`;
      if (field.errors['minlength'])
        return `${fieldName} deve ter pelo menos ${field.errors['minlength'].requiredLength} caracteres`;
      if (field.errors['min'])
        return `${fieldName} deve ser maior que ${field.errors['min'].min}`;
      if (field.errors['max'])
        return `${fieldName} deve ser menor que ${field.errors['max'].max}`;
    }
    return null;
  }
}
