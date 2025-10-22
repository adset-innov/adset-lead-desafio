import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BotaoBuscarComponent } from './botao-buscar.component';

describe('BotaoBuscarComponent', () => {
  let component: BotaoBuscarComponent;
  let fixture: ComponentFixture<BotaoBuscarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BotaoBuscarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BotaoBuscarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
