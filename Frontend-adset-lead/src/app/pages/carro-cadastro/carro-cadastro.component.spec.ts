import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarroCadastroComponent } from './carro-cadastro.component';

describe('CarroCadastroComponent', () => {
  let component: CarroCadastroComponent;
  let fixture: ComponentFixture<CarroCadastroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarroCadastroComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarroCadastroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
