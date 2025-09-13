import { Component, EventEmitter, Input, Output, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VehicleService } from '../../services/vehicle.service';
import { Vehicle } from '../../models/VehicleModel';

const CURRENT_YEAR = new Date().getFullYear();

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.scss']
})
export class VehicleFormComponent implements OnInit {
  @Input() initial: Vehicle | null = null;
  @Output() saved = new EventEmitter<Vehicle>();

  form!: FormGroup;
  optionals : any[] = [];
  imagePreviews: string[] = [];
  selectedFiles: File[] = [];
  maxImages = 15;
  colors: string[] = [
  'Branco',
  'Preto',
  'Prata',
  'Cinza',
  'Vermelho',
  'Azul',
  'Verde',
  'Amarelo'
];

  constructor(private fb: FormBuilder, private svc: VehicleService) {}

  ngOnInit(): void {
    this.buildForm();
    this.loadOpcionais();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['initial'] && this.form) {
      this.patchForm();
    }
  }
    private buildForm() {
    this.form = this.fb.group({
      make: [this.initial?.make || '', Validators.required],
      model: [this.initial?.model || '', Validators.required],
      year: [this.initial?.year || '', Validators.required],
      plate: [this.initial?.plate || '', Validators.required],
      km: [this.initial?.km || ''],
      color: [this.initial?.color || '', Validators.required],
      price: [this.initial?.price || '', Validators.required],
      optionals: [this.initial?.optionals || []],
    });

     this.loadOpcionais();

    if (this.initial?.imageUrls) {
      this.imagePreviews = this.initial.imageUrls;
    }
  }

    private patchForm() {
    this.form.patchValue({
      make: this.initial?.make || '',
      model: this.initial?.model || '',
      year: this.initial?.year || '',
      plate: this.initial?.plate || '',
      km: this.initial?.km || '',
      color: this.initial?.color || '',
      price: this.initial?.price || '',
      optionals: this.initial?.optionals || [],
    });

    this.imagePreviews = this.initial?.imageUrls || [];
  }

  loadOpcionais() {
    this.svc.getOpcionais().subscribe(data => {
      this.optionals = data;
    });
  }


  toggleOptional(opt: any) {
    const optionals = [...this.form.value.optionals];
    const index = optionals.indexOf(opt.id);
    if (index > -1) {
      optionals.splice(index, 1);
    } else {
      optionals.push(opt.id);
    }
    this.form.patchValue({ optionals });
  }

  onFilesPicked(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files) return;

    const files = Array.from(input.files);
    const allowed = files.slice(0, this.maxImages - this.imagePreviews.length);

    allowed.forEach(file => {
      this.selectedFiles.push(file);

      const reader = new FileReader();
      reader.onload = () => this.imagePreviews.push(reader.result as string);
      reader.readAsDataURL(file);
    });
  }
  get formTitle(): string {
  return this.initial?.id ? 'Editar Veículo' : 'Cadastrar Veículo';
  }

    save() {
      if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
      const vehicle = this.form.value;

      const formData = new FormData();

      formData.append('make', vehicle.make);
      formData.append('model', vehicle.model);
      formData.append('year', vehicle.year);
      formData.append('plate', vehicle.plate);
      formData.append('km', vehicle.km);
      formData.append('color', vehicle.color);
      formData.append('price', vehicle.price);

      vehicle.optionals.forEach((optId: number) => {
        formData.append('optionals', optId.toString());
      });

      this.selectedFiles.forEach(file => {
        formData.append('Images', file);
      });

    if (this.initial?.id) {
      this.svc.update(this.initial.id, formData).subscribe({
        next: (res) => {
          console.log('Veículo atualizado!', res);
          this.saved.emit(res);
        },
        error: (err) => console.error('Erro ao atualizar', err)
      });
    } else {
      this.svc.create(formData).subscribe({
        next: (res) => {
          console.log('Veículo salvo!', res);
          this.saved.emit(res);
        },
        error: (err) => console.error('Erro ao salvar', err)
      });
    }
  }
}
