import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateVehicleModalComponent } from './create-vehicle-modal.component';

describe('CreateVehicleModalComponent', () => {
  let component: CreateVehicleModalComponent;
  let fixture: ComponentFixture<CreateVehicleModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateVehicleModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateVehicleModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
