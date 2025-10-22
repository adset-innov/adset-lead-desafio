import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FiltroVeiculosComponent } from './filtro-veiculos.component';

describe('FiltroVeiculosComponent', () => {
  let component: FiltroVeiculosComponent;
  let fixture: ComponentFixture<FiltroVeiculosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FiltroVeiculosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FiltroVeiculosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
