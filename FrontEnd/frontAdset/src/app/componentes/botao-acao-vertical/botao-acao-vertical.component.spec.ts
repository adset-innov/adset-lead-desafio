import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BotaoAcaoVerticalComponent } from './botao-acao-vertical.component';

describe('BotaoAcaoVerticalComponent', () => {
  let component: BotaoAcaoVerticalComponent;
  let fixture: ComponentFixture<BotaoAcaoVerticalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BotaoAcaoVerticalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BotaoAcaoVerticalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
