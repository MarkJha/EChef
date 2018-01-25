import { ShowNotificationService } from '../common/show-notification';
import { AdminSubProductsService } from '../../admin/admin-sub-products/admin-sub-products.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-product-filter',
  templateUrl: './product-filter.component.html',
  styleUrls: ['./product-filter.component.css']
})
export class ProductFilterComponent implements OnInit {

  @Input('category') category;
  @Output() categoryClicked: EventEmitter<number> = new EventEmitter<number>();
  subProductList;
  constructor(private _subProductService: AdminSubProductsService,
    private _notify: ShowNotificationService) { }

  ngOnInit() {
    this._subProductService.selectSubCusineList("Menu/SubCusine/WithCusineDetailCount/")
      .subscribe(
      data => this.bindSubProductList(data),
      error => {
        this._notify.showError(error)
      });

  }

  bindSubProductList(data) {
    this.subProductList = data.model;
  }

  onClick(id: number): void {
    console.log(id);
    this.categoryClicked.emit(id);
  }

}
