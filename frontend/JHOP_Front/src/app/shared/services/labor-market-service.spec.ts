import { TestBed } from '@angular/core/testing';

import { LaborMarketService } from './labor-market-service';

describe('LaborMarketService', () => {
  let service: LaborMarketService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LaborMarketService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
