import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubProductFormComponent } from './sub-product-form.component';

describe('SubProductFormComponent', () => {
  let component: SubProductFormComponent;
  let fixture: ComponentFixture<SubProductFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubProductFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubProductFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
