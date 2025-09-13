import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehiclePackagesComponent } from './vehicle-packages.component';

describe('VehiclePackagesComponent', () => {
  let component: VehiclePackagesComponent;
  let fixture: ComponentFixture<VehiclePackagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VehiclePackagesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VehiclePackagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
