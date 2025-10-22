import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaVeiculosComponent } from './lista-veiculos.component';

describe('ListaVeiculosComponent', () => {
  let component: ListaVeiculosComponent;
  let fixture: ComponentFixture<ListaVeiculosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListaVeiculosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListaVeiculosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
