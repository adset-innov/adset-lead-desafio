import { VehicleOptionsService } from '../../../../core/services/vehicle-options.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { VehicleOption } from '../../../../core/models/vehicle-option';
import { Observable, of } from 'rxjs';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { debounceTime, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-create-vehicle-modal',
  templateUrl: './create-vehicle-modal.component.html',
  styleUrls: ['./create-vehicle-modal.component.scss'],
})
export class CreateVehicleModalComponent implements OnInit {
  @Output() vehicleCreated = new EventEmitter<any>();

  form!: FormGroup;
  filteredOptions$: Observable<VehicleOption[]> = of([]);
  selectedOptions: VehicleOption[] = [];
  files: File[] = [];

  constructor(
    private fb: FormBuilder,
    private optionsService: VehicleOptionsService,
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      brand: [''],
      model: [''],
      year: [''],
      licensePlate: [''],
      color: [''],
      price: [''],
      mileage: [''],
      optionSearch: [''],
    });

    this.filteredOptions$ = this.form.get('optionSearch')!.valueChanges.pipe(
      debounceTime(300),
      switchMap((value) =>
        value ? this.optionsService.search(value) : of([]),
      ),
    );
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.files = Array.from(input.files);
    }
  }

  addOption(option: VehicleOption | null | undefined) {
    if (!option || !option.id || !option.name) return;

    if (!this.selectedOptions.find((o) => o.id === option.id)) {
      this.selectedOptions.push({ id: option.id, name: option.name });
    }
    this.form.get('optionSearch')!.setValue('');
  }

  createNewOption(name: string | VehicleOption) {
    const optionName =
      typeof name === 'string' ? name.trim() : name?.name?.trim();

    console.log('createNewOption recebeu:', name, '->', optionName);

    if (!optionName) return;

    this.optionsService.create(optionName).subscribe({
      next: (res) => {
        console.log('Novo option criado:', res);
        if (res && res.name) {
          this.addOption({ id: res.id, name: res.name });
        }
      },
      error: (err) => {
        console.error('Erro ao criar option', err);
      },
    });
  }

  removeOption(option: VehicleOption) {
    this.selectedOptions = this.selectedOptions.filter(
      (o) => o.id !== option.id,
    );
  }

  save() {
    const vehicle = {
      ...this.form.value,
      options: this.selectedOptions.map((o) => o.name),
      files: this.files,
    };

    this.vehicleCreated.emit(vehicle);
  }
}
