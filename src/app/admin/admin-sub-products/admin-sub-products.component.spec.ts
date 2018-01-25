import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSubProductsComponent } from './admin-sub-products.component';

describe('AdminSubProductsComponent', () => {
  let component: AdminSubProductsComponent;
  let fixture: ComponentFixture<AdminSubProductsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminSubProductsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminSubProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
