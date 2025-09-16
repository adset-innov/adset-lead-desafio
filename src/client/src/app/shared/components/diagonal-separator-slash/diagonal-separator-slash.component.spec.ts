import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiagonalSeparatorSlashComponent } from './diagonal-separator-slash.component';

describe('DiagonalSeparatorSlash', () => {
  let component: DiagonalSeparatorSlashComponent;
  let fixture: ComponentFixture<DiagonalSeparatorSlashComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DiagonalSeparatorSlashComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DiagonalSeparatorSlashComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
