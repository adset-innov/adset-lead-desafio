import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehiclesFiltersBarComponent } from './vehicles-filters-bar.component';

describe('VehiclesFiltersBarComponent', () => {
  let component: VehiclesFiltersBarComponent;
  let fixture: ComponentFixture<VehiclesFiltersBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VehiclesFiltersBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VehiclesFiltersBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
