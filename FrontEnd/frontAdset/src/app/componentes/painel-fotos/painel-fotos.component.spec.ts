import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelFotosComponent } from './painel-fotos.component';

describe('PainelFotosComponent', () => {
  let component: PainelFotosComponent;
  let fixture: ComponentFixture<PainelFotosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PainelFotosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PainelFotosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
