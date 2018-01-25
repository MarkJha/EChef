import { TestBed, inject } from '@angular/core/testing';

import { AdminOrdersService } from './admin-orders.service';

describe('AdminOrdersService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdminOrdersService]
    });
  });

  it('should be created', inject([AdminOrdersService], (service: AdminOrdersService) => {
    expect(service).toBeTruthy();
  }));
});
