import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSubProductsDetailComponent } from './admin-sub-products-detail.component';

describe('AdminSubProductsDetailComponent', () => {
  let component: AdminSubProductsDetailComponent;
  let fixture: ComponentFixture<AdminSubProductsDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminSubProductsDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminSubProductsDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
