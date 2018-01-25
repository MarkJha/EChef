import { ExDialog } from '../NgExDialog/dialog.module';
import { AppError } from '../shared/common/app-error';
import { ShowNotificationService } from '../shared/common/show-notification';
import { ProductDetail } from '../models/productDetail';
import { AdminSubProductsDetailService } from '../admin/admin-sub-products-detail/admin-sub-products-detail.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'search-food',
  templateUrl: './search-food.component.html',
  styleUrls: ['./search-food.component.css']
})
export class SearchFoodComponent implements OnInit {
  errorMessage: any;

  constructor(
    private _service: AdminSubProductsDetailService,
    private _notify: ShowNotificationService,
    private _exDialog: ExDialog, ) { }

  ngOnInit() {
  }

  products;

  async search(searchText: string) {
    if (!searchText) {
      this._exDialog.openMessage("Please enter your search text", "Food Picky", "error");
      return;
    }
    await this._service.search(searchText)
      .subscribe(product => {
        this.onSearchComplete(product)
      }, (error: AppError) => {
        this.errorMessage = <any>error
        this._notify.showError(error)
      })
  }

  onSearchComplete(productList): any {
    console.log(productList);
    if(productList.model.length>0){
      this.products = productList.model;
      this._notify.showInfo('Product detail loaded successfully !')      
    }else{
      this._exDialog.openMessage("OOps no record found, try search something other key words");
    }    
  }

}
