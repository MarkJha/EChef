import { TestBed, inject } from '@angular/core/testing';

import { AdminSubProductsService } from './admin-sub-products.service';

describe('AdminSubProductsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdminSubProductsService]
    });
  });

  it('should be created', inject([AdminSubProductsService], (service: AdminSubProductsService) => {
    expect(service).toBeTruthy();
  }));
});
