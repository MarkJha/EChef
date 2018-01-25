import { HttpClient } from '@angular/common/http';
import { AdminSubProductsService } from '../admin-sub-products/admin-sub-products.service';
import { AppError } from '../../shared/common/app-error';
import { SubProduct } from '../../models/subProduct';
import { AdminProductService } from '../admin-products/admin-products.service';
import { ShowNotificationService } from '../../shared/common/show-notification';
import { ExDialog } from '../../NgExDialog/dialog.module';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sub-product-form',
  templateUrl: './sub-product-form.component.html',
  styleUrls: ['./sub-product-form.component.css']
})
export class SubProductFormComponent implements OnInit {

  product = {};
  id;
  flag;
  errorMessage: any;
  headerTitle: string;
  viewOnly: boolean;
  cusineList;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private _exDialog: ExDialog,
    private _notify: ShowNotificationService,
    private _service: AdminProductService,
    private _subProductService: AdminSubProductsService) {
    this.loadCusineList();
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.flag = this.route.snapshot.paramMap.get('flag');

    if (this.flag === "D") this.delete(this.id);

    if (this.id) this.getProduct(this.id);
    else this.headerTitle = "Create Sub Product";
  }

  loadCusineList() {
    this._service
      .selectCusineList("http://localhost:49765/api/MainMenu/SelectCuisine/")
      .subscribe(
      data => this.cusineList = data.model,
      error => {
        this._notify.showError(error)
      });
  }

  initializeSubProduct(): SubProduct {
    // Return an initialized object
    return {
      id: 0,
      ProductId: 0,
      name: "",
      description: "",
      guid: "",
      imagePath: "",
      isActive: false
    };
  }

  save(subProduct) {
    console.log(subProduct);
    if (this.id) this.updateProduct(subProduct);
    else this.createProduct(subProduct);
  }

  getProduct(id) {
    if (this.id) this._subProductService.get(this.id).take(1)
      .subscribe(
      data => this.product = data.model,
      (error: AppError) => this._notify.showError(error));

    this.headerTitle = "Update Sub-Product";
    if (this.flag === "V") {
      this.viewOnly = true;
      this.headerTitle = "View Sub-Product (view only)"
    }
  }

  createProduct(subProduct: SubProduct) {
    this._subProductService.create(subProduct)
      .subscribe(() => {
        this.onSaveComplete()
        this._notify.showInfo('Sub-Product added successfully !')
      }, (error: AppError) => this._notify.showError(error));
  }

  updateProduct(subProduct: SubProduct): void {
    subProduct.id = this.id;
    this._subProductService.update(subProduct)
      .subscribe(() => {
        this._notify.showInfo('Sub-Product updated successfully !');
        this.onSaveComplete()
      }, (error: AppError) => this._notify.showError(error));
  }

  delete(id: number) {
    this._exDialog.openConfirm('Are you sure you want to delete this product?')
      .subscribe((result) => {
        if (result) {
          this._subProductService.delete(this.id)
            .subscribe(() => {
              this._notify.showInfo('Sub-Product deleted successfully !');
              this.onSaveComplete()
            }, (error: AppError) => this._notify.showError(error));
        }
      });
    this.redirectToProductList();
  }


  onSaveComplete(): void {
    this.redirectToProductList();
  }

  redirectToProductList() {
    this.router.navigate(['/admin/subproducts']);
  }
}


