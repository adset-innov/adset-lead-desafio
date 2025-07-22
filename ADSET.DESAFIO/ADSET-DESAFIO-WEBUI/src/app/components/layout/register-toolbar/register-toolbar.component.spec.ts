import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterToolbarComponent } from './register-toolbar.component';
import { ReactiveFormsModule } from '@angular/forms';

describe('RegisterToolbarComponent', () => {
  let component: RegisterToolbarComponent;
  let fixture: ComponentFixture<RegisterToolbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegisterToolbarComponent],
      imports: [ReactiveFormsModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});