import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Vehicle } from '../../../../core/models/vehicle';
import { VehicleOption } from '../../../../core/models/vehicle-option';
import { VehicleOptionsService } from '../../../../core/services/vehicle-options.service';
import { Observable, of } from 'rxjs';
import { debounceTime, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-edit-vehicle-modal',
  templateUrl: './edit-vehicle-modal.component.html',
  styleUrls: ['./edit-vehicle-modal.component.scss'],
})
export class EditVehicleModalComponent implements OnInit {
  @Output() vehicleUpdated = new EventEmitter<any>();

  form!: FormGroup;
  filteredOptions$: Observable<VehicleOption[]> = of([]);
  selectedOptions: VehicleOption[] = [];
  files: File[] = [];

  constructor(
    private fb: FormBuilder,
    private optionsService: VehicleOptionsService,
    private dialogRef: MatDialogRef<EditVehicleModalComponent>,
    @Inject(MAT_DIALOG_DATA) public vehicle: Vehicle,
  ) {}

  ngOnInit(): void {
    this.form = this.fb.group({
      brand: [this.vehicle.brand],
      model: [this.vehicle.model],
      year: [this.vehicle.year],
      licensePlate: [this.vehicle.licensePlate],
      color: [this.vehicle.color],
      price: [this.vehicle.price],
      mileage: [this.vehicle.mileage],
      optionSearch: [''],
      iCarrosPackage: [
        this.vehicle.portalPackages?.find((p) => p.portal === 'ICarros')
          ?.package ?? '',
      ],
      webMotorsPackage: [
        this.vehicle.portalPackages?.find((p) => p.portal === 'WebMotors')
          ?.package ?? '',
      ],
    });

    this.selectedOptions = this.vehicle.options ?? [];

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
    if (!option || !option.name) return;

    if (!this.selectedOptions.find((o) => o.name === option.name)) {
      this.selectedOptions.push({ id: option.id ?? '', name: option.name });
    }
    this.form.get('optionSearch')!.setValue('');
  }

  createNewOption(name: string) {
    if (!name?.trim()) return;

    this.optionsService.create(name.trim()).subscribe({
      next: (newOpt) => {
        if (newOpt?.name) {
          this.addOption(newOpt);
        }
      },
      error: (err) => console.error('Erro ao criar option', err),
    });
  }

  removeOption(option: VehicleOption) {
    this.selectedOptions = this.selectedOptions.filter(
      (o) => o.name !== option.name,
    );
  }

  save() {
    const portalPackages: any[] = [];

    if (this.form.value.iCarrosPackage) {
      portalPackages.push({
        portal: 'ICarros',
        package: this.form.value.iCarrosPackage,
      });
    }

    if (this.form.value.webMotorsPackage) {
      portalPackages.push({
        portal: 'WebMotors',
        package: this.form.value.webMotorsPackage,
      });
    }

    const vehicle = {
      id: this.vehicle.id,
      ...this.form.value,
      options: this.selectedOptions.map((o) => o.name),
      files: this.files,
      portalPackages,
    };

    this.vehicleUpdated.emit(vehicle);
    this.dialogRef.close();
  }

  cancel() {
    this.dialogRef.close();
  }
}
