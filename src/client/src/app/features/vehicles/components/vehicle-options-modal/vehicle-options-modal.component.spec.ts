import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleOptionsModal.TsComponent } from './vehicle-options-modal.component';

describe('VehicleOptionsModal.TsComponent', () => {
  let component: VehicleOptionsModal.TsComponent;
  let fixture: ComponentFixture<VehicleOptionsModal.TsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VehicleOptionsModal.TsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VehicleOptionsModal.TsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
