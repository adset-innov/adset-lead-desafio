import { Component, EventEmitter, Output } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { CarService } from '../../../services/car.service';

@Component({
  selector: 'app-register-toolbar',
  templateUrl: './register-toolbar.component.html',
  styleUrls: ['./register-toolbar.component.scss']
})
export class RegisterToolbarComponent {
  form: FormGroup;
  photos: File[] = [];

  @Output() submitted = new EventEmitter<void>();

  constructor(private fb: FormBuilder, private carService: CarService) {
    this.form = this.fb.group({
      brand: [''],
      model: [''],
      year: [''],
      plate: [''],
      km: [''],
      color: [''],
      price: [''],
      optionals: this.fb.array([])
    });
  }

  get optionals(): FormArray {
    return this.form.get('optionals') as FormArray;
  }

  addOptional(): void {
    this.optionals.push(this.fb.control(''));
  }

  removeOptional(index: number): void {
    this.optionals.removeAt(index);
  }

  onFilesChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.photos = Array.from(input.files);
    }
  }

  onSubmit(): void {
    const formData = new FormData();
    const value = this.form.value;

    formData.append('brand', value.brand);
    formData.append('model', value.model);
    formData.append('year', value.year);
    formData.append('plate', value.plate);
    if (value.km) {
      formData.append('km', value.km);
    }
    formData.append('color', value.color);
    formData.append('price', value.price);
    formData.append('optionals', JSON.stringify(this.optionals.value));
    this.photos.forEach(file => formData.append('photos', file));

    this.carService.create(formData).subscribe(() => {
      this.form.reset();
      this.photos = [];
      this.optionals.clear();
      this.submitted.emit();
    });
  }
}