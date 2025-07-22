import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SummaryToolbarComponent } from './summary-toolbar.component';

describe('SummaryToolbarComponent', () => {
  let component: SummaryToolbarComponent;
  let fixture: ComponentFixture<SummaryToolbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SummaryToolbarComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SummaryToolbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
