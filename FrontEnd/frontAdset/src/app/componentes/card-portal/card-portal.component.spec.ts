import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardPortalComponent } from './card-portal.component';

describe('CardPortalComponent', () => {
  let component: CardPortalComponent;
  let fixture: ComponentFixture<CardPortalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardPortalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CardPortalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
