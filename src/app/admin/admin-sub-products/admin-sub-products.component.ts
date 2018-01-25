import { AdminSubProductsService } from './admin-sub-products.service';
import { ShowNotificationService } from '../../shared/common/show-notification';
import { DataTableResource } from 'angular-4-data-table/dist';
import { ApiResponse } from '../../models/apiResponse';
import { SubProduct } from '../../models/subProduct';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-sub-products',
  templateUrl: './admin-sub-products.component.html',
  styleUrls: ['./admin-sub-products.component.css']
})
export class AdminSubProductsComponent implements OnInit {

  //*** declared private variables
  imageWidth: number = 180;
  imageMargin: number = 6;
  pageTitle: string = "Sub Cuisine List";
  itemCount: number;
  items: SubProduct[] = [];
  products: SubProduct[] = [];
  response: ApiResponse<SubProduct>;
  isError: boolean = false;
  errorMessage: string;
  loading: boolean = false;
  tableResource: DataTableResource<SubProduct>;

  constructor(
    private _notify: ShowNotificationService,
    private _service: AdminSubProductsService) { }

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

  private initializeDataTable(products: SubProduct[]) {
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
