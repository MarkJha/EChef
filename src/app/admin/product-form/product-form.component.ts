import { ExDialog } from '../../NgExDialog/dialog.module';
import { ShowNotificationService } from '../../shared/common/show-notification';
import 'rxjs/add/operator/take';

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Product } from '../../models/product';
import { AppError } from '../../shared/common/app-error';
import { BadInput } from '../../shared/common/bad-input';
import { NotFoundError } from '../../shared/common/not-found-error';
import { ServerDown } from '../../shared/common/server-down';
import { AdminProductService } from '../admin-products/admin-products.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {

  product = {};
  id;
  flag;
  errorMessage: any;
  headerTitle: string;
  viewOnly: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private _exDialog: ExDialog,
    private _notify: ShowNotificationService,
    private _service: AdminProductService) {

  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.flag = this.route.snapshot.paramMap.get('flag');

    if (this.flag === "D") this.delete(this.id);

    if (this.id) this.getProduct(this.id);
    else this.headerTitle = "Create Product";

  }

  initializeProduct(): Product {
    // Return an initialized object
    return {
      id: 0,
      name: "",
      description: "",
      guid: "",
      imagePath: "",
      isActive: false
    };
  }

  save(product) {
    console.log(product);
    if (this.id) this.updateProduct(product);
    else this.createProduct(product);
  }

  getProduct(id) {
    if (this.id) this._service.get(this.id).take(1)
      .subscribe(
      data => this.product = data.model,
      (error: AppError) => this._notify.showError(error));

    this.headerTitle = "Update Product";
    if (this.flag === "V") {
      this.viewOnly = true;
      this.headerTitle = "View Product (view only)"
    }
  }

  createProduct(product: Product) {
    this._service.create(product)
      .subscribe(() => {
        this.onSaveComplete()
        this._notify.showInfo('Product added successfully !')
      }, (error: AppError) => this._notify.showError(error));
  }

  updateProduct(product: Product): void {
    product.id = this.id;
    this._service.update(product)
      .subscribe(() => {
        this._notify.showInfo('Product updated successfully !');
        this.onSaveComplete()
      }, (error: AppError) => this._notify.showError(error));
  }

  delete(id: number) {
    this._exDialog.openConfirm('Are you sure you want to delete this product?')
      .subscribe((result) => {
        if (result) {
          this._service.delete(this.id)
            .subscribe(() => {
              this._notify.showInfo('Product deleted successfully !');
              this.onSaveComplete()
            }, (error: AppError) => this._notify.showError(error));
        }
      });
    this.redirectToProductList();
  }

  partiallyUpdate() {
    let product = [
      {
        "value": "Maxician ..",
        "path": "/name",
        "op": "replace"
      }
    ]
  }

  onSaveComplete(): void {
    this.redirectToProductList();
  }

  redirectToProductList() {
    this.router.navigate(['/admin/products']);
  }

}
