import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehiclesSearchBarComponent } from './vehicles-search-bar.component';

describe('VehiclesSearchBarComponent', () => {
  let component: VehiclesSearchBarComponent;
  let fixture: ComponentFixture<VehiclesSearchBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VehiclesSearchBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VehiclesSearchBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
