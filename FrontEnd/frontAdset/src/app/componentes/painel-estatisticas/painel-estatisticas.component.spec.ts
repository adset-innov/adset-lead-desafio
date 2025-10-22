import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelEstatisticasComponent } from './painel-estatisticas.component';

describe('PainelEstatisticasComponent', () => {
  let component: PainelEstatisticasComponent;
  let fixture: ComponentFixture<PainelEstatisticasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PainelEstatisticasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PainelEstatisticasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
