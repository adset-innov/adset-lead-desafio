import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BotaoLimparComponent } from './botao-limpar.component';

describe('BotaoLimparComponent', () => {
  let component: BotaoLimparComponent;
  let fixture: ComponentFixture<BotaoLimparComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BotaoLimparComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BotaoLimparComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
