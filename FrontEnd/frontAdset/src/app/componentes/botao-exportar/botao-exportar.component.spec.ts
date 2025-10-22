import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BotaoExportarComponent } from './botao-exportar.component';

describe('BotaoExportarComponent', () => {
  let component: BotaoExportarComponent;
  let fixture: ComponentFixture<BotaoExportarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BotaoExportarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BotaoExportarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
