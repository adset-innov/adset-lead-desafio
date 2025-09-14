import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehiclesStatsBarComponent } from './vehicles-stats-bar.component';

describe('VehiclesStatsBarComponent', () => {
  let component: VehiclesStatsBarComponent;
  let fixture: ComponentFixture<VehiclesStatsBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VehiclesStatsBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VehiclesStatsBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
