import { Component, OnInit } from '@angular/core';
import { DataTableResource } from 'angular-4-data-table';
import { ShowNotificationService } from '../../shared/common/show-notification';

import { ApiResponse } from '../../models/apiResponse';
import { Product } from '../../models/product';
import { AdminProductService } from './admin-products.service';

@Component({
  selector: 'admin-products',
  templateUrl: './admin-products.component.html',
  styleUrls: ['./admin-products.component.css']
})
export class AdminProductsComponent implements OnInit {

  //*** declared private variables
  imageWidth: number = 180;
  imageMargin: number = 6;
  pageTitle: string = "Cuisine List";
  itemCount: number;
  items: Product[] = [];
  products: Product[] = [];
  response: ApiResponse<Product>;
  isError: boolean = false;
  errorMessage: string;
  loading: boolean = false;
  tableResource: DataTableResource<Product>;

  constructor(
    private _notify: ShowNotificationService,
    private _service: AdminProductService) { }

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

  private initializeDataTable(products: Product[]) {
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
