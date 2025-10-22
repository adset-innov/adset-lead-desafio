import { TestBed } from '@angular/core/testing';

import { RemocaoService } from './remocao.service';

describe('RemocaoService', () => {
  let service: RemocaoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RemocaoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
