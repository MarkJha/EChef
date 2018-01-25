import { TestBed, inject } from '@angular/core/testing';

import { AdminProductsService } from './admin-products.service';

describe('AdminProductsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdminProductsService]
    });
  });

  it('should be created', inject([AdminProductsService], (service: AdminProductsService) => {
    expect(service).toBeTruthy();
  }));
});
