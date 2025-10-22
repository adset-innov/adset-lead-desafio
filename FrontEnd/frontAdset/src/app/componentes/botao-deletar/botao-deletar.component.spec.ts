import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BotaoDeletarComponent } from './botao-deletar.component';

describe('BotaoDeletarComponent', () => {
  let component: BotaoDeletarComponent;
  let fixture: ComponentFixture<BotaoDeletarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BotaoDeletarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BotaoDeletarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
