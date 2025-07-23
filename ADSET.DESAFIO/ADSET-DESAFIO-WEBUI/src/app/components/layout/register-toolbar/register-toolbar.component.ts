import { Component, EventEmitter, Output, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { CarService } from '../../../services/car.service';
import { Car } from '../../../models/car.model';
import { Portal, PackageType, PackageTypeApiMap } from '../../../enums/car-portal-package.enums';

@Component({
  selector: 'app-register-toolbar',
  templateUrl: './register-toolbar.component.html',
  styleUrls: ['./register-toolbar.component.scss']
})
export class RegisterToolbarComponent implements OnChanges {
  form: FormGroup;
  photos: File[] = [];
  public packageTypes: PackageType[] = Object.values(PackageType);
  public Portal = Portal;
  portalPackages: { [key in Portal]?: PackageType } = {};

  @Input() car: Car | null = null;
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
      icarrosPackage: [''],
      webmotorsPackage: [''],
      optionals: this.fb.array([])
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['car'] && this.car) {
      this.form.patchValue({
        brand: this.car.brand,
        model: this.car.model,
        year: this.car.year,
        plate: this.car.plate,
        km: this.car.km,
        color: this.car.color,
        price: this.car.price
      });

      const map = new Map<Portal, PackageType>();
      if (Array.isArray(this.car.portalPackages)) {
        for (const pkg of this.car.portalPackages) {
          map.set(pkg.portal, pkg.package);
        }
      }
      this.form.patchValue({
        icarrosPackage: map.get(Portal.iCarros) ?? '',
        webmotorsPackage: map.get(Portal.WebMotors) ?? ''
      });


      this.optionals.clear();
      if (Array.isArray(this.car.optionals)) {
        this.car.optionals.forEach(() => this.optionals.push(this.fb.control('')));
      }
      
      const pkgObj: { [key in Portal]?: PackageType } = {};
      if (Array.isArray(this.car.portalPackages)) {
        this.car.portalPackages.forEach(p => {
          pkgObj[p.portal] = p.package;
        });
      }
      this.portalPackages = pkgObj;
    }
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

  onPackageSelect(portal: Portal, pkg: string): void {
    const type = pkg as PackageType;
    if (type) {
      this.portalPackages[portal] = type;
    } else {
      delete this.portalPackages[portal];
    }
  }

  onFilesChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.photos = [
        ...this.photos,
        ...Array.from(input.files)
      ];
      input.value = '';
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
    const portalPackages: { [key: string]: PackageType } = {};
    if (value.icarrosPackage) {
      portalPackages[Portal.iCarros] = value.icarrosPackage;
    }
    if (value.webmotorsPackage) {
      portalPackages[Portal.WebMotors] = value.webmotorsPackage;
    }
    formData.append('portalPackages', JSON.stringify(this.portalPackages));
    formData.append('optionals', JSON.stringify(this.optionals.value));
    this.photos.forEach(file => formData.append('photos', file));

    const request = this.car ? this.carService.update(this.car.id, formData) : this.carService.create(formData);
    request.subscribe(() => {
      this.form.reset();
      this.photos = [];
      this.optionals.clear();
      this.submitted.emit();
    });
  }

  private getPortalPackagesForApi(): Record<Portal, string> {
    const result: Record<Portal, string> = {} as Record<Portal, string>;
    Object.entries(this.portalPackages).forEach(([portal, pkg]) => {
      result[portal as Portal] = PackageTypeApiMap[pkg as PackageType];
    });
    return result;
  }
}