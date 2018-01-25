import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubProductDetailFormComponent } from './sub-product-detail-form.component';

describe('SubProductDetailFormComponent', () => {
  let component: SubProductDetailFormComponent;
  let fixture: ComponentFixture<SubProductDetailFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubProductDetailFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubProductDetailFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
