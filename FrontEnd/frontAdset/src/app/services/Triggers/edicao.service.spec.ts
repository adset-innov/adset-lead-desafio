import { TestBed } from '@angular/core/testing';

import { EdicaoService } from './edicao.service';

describe('EdicaoService', () => {
  let service: EdicaoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EdicaoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
