import { AdminSubProductsDetailService } from './admin-sub-products-detail.service';
import { AdminSubProductsService } from '../admin-sub-products/admin-sub-products.service';
import { ShowNotificationService } from '../../shared/common/show-notification';
import { DataTableResource } from 'angular-4-data-table/dist';
import { ApiResponse } from '../../models/apiResponse';
import { ProductDetail } from '../../models/productDetail';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-sub-products-detail',
  templateUrl: './admin-sub-products-detail.component.html',
  styleUrls: ['./admin-sub-products-detail.component.css']
})
export class AdminSubProductsDetailComponent implements OnInit {

 //*** declared private variables
 imageWidth: number = 180;
 imageMargin: number = 6;
 pageTitle: string = "All Menu List";
 itemCount: number;
 items: ProductDetail[] = [];
 products: ProductDetail[] = [];
 response: ApiResponse<ProductDetail>;
 isError: boolean = false;
 errorMessage: string;
 loading: boolean = false;
 tableResource: DataTableResource<ProductDetail>;

 constructor(
   private _notify: ShowNotificationService,
   private _service: AdminSubProductsDetailService) { }

 ngOnInit() {
   this.loading = true;
   this._service.getAll()
     .subscribe(
     data => this.handleSuccess(data),
     error => {
       this._notify.showError(error)
       this.loading = false;
       this.isError=true;
     });
 }

 private initializeDataTable(products: ProductDetail[]) {
   this.tableResource = new DataTableResource(products);
   this.tableResource
     .query({ offset: 0 })
     .then(items => this.items = items);

   this.tableResource
     .count()
     .then(count => this.itemCount = count);
 }

 reloadItems(params) {
   if (!this.tableResource) return;

   this.tableResource
     .query(params)
     .then(items => this.items = items);
 }

 filter(query: string) {
   console.log(query);
   let filteredProducts = (query)
     ? this.products.filter(p => p.name.toLowerCase().includes(query.toLowerCase()))
     : this.products

   this.initializeDataTable(filteredProducts);
 }

 handleSuccess(data) {
   this.products = data.model;
   this.initializeDataTable(this.products);
   this.loading = false;
 }

 rowTooltip(item) {
   return item.name;
 }

}
