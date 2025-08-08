import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarroEdicaoComponent } from './carro-edicao.component';

describe('CarroEdicaoComponent', () => {
  let component: CarroEdicaoComponent;
  let fixture: ComponentFixture<CarroEdicaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarroEdicaoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarroEdicaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
