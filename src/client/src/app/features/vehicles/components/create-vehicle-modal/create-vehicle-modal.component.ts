import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreateVehicleRequest } from '../../../../core/dtos/requests/create-vehicle-request';

@Component({
  selector: 'app-create-vehicle-modal',
  templateUrl: './create-vehicle-modal.component.html',
  styleUrls: ['./create-vehicle-modal.component.scss'],
})
export class CreateVehicleModalComponent {
  @Output() save = new EventEmitter<CreateVehicleRequest>();
  form: FormGroup;
  selectedFiles: File[] = [];

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      brand: ['', Validators.required],
      model: ['', Validators.required],
      year: ['', [Validators.required, Validators.min(1900)]],
      licensePlate: ['', Validators.required],
      color: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(1)]],
      mileage: [0, [Validators.required, Validators.min(0)]],
      options: [[]],
      portalPackages: [[]],
    });
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.selectedFiles = Array.from(input.files);
    }
  }

  submit() {
    if (this.form.invalid) return;

    const payload = {
      ...this.form.value,
      files: this.selectedFiles,
    };

    this.save.emit(payload);
  }
}
