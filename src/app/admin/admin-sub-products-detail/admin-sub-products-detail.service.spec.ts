import { TestBed, inject } from '@angular/core/testing';

import { AdminSubProductsDetailService } from './admin-sub-products-detail.service';

describe('AdminSubProductsDetailService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdminSubProductsDetailService]
    });
  });

  it('should be created', inject([AdminSubProductsDetailService], (service: AdminSubProductsDetailService) => {
    expect(service).toBeTruthy();
  }));
});
