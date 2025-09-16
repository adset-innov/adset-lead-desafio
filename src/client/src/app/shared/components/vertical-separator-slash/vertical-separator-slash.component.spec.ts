import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VerticalSeparatorSlashComponent } from './vertical-separator-slash.component';

describe('VerticalSeparatorSlashComponent', () => {
  let component: VerticalSeparatorSlashComponent;
  let fixture: ComponentFixture<VerticalSeparatorSlashComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VerticalSeparatorSlashComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VerticalSeparatorSlashComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
