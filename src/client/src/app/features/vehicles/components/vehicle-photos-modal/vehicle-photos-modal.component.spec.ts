import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehiclePhotosModalComponent } from './vehicle-photos-modal.component';

describe('VehiclePhotosModalComponent', () => {
  let component: VehiclePhotosModalComponent;
  let fixture: ComponentFixture<VehiclePhotosModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VehiclePhotosModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VehiclePhotosModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
