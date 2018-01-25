import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { AbstractControl } from '@angular/forms/src/model';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Rx';

import { ProductDetail } from '../../models/productDetail';
import { ExDialog } from '../../NgExDialog/dialog.module';
import { AppError } from '../../shared/common/app-error';
import { GenericValidator } from '../../shared/common/generic-validator';
import { NumberValidators } from '../../shared/common/number-validator';
import { ShowNotificationService } from '../../shared/common/show-notification';
import { AdminProductService } from '../admin-products/admin-products.service';
import { AdminSubProductsDetailService } from '../admin-sub-products-detail/admin-sub-products-detail.service';
import { AdminSubProductsService } from '../admin-sub-products/admin-sub-products.service';

@Component({
  selector: 'app-sub-product-detail-form',
  templateUrl: './sub-product-detail-form.component.html',
  styleUrls: ['./sub-product-detail-form.component.css']
})
export class SubProductDetailFormComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  id;
  subCusineId;
  flag;
  errorMessage: string;
  productDetailForm: FormGroup;
  productDetail: ProductDetail = new ProductDetail();
  imageUrlMessage: string;
  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;
  headerTitle: string
  cusineList;
  subCusineList;
  cusineSelected: any[] = [];
  subCusineSelected: any[] = [];
  cusineName: string;
  subCusineName: string;
  // cusineSelected: SelectOption = {
  //   id: 0,
  //   value: ""
  // };

  private imageUrlValidationMessage = {
    required: "Please enter image Url",
    pattern: "Please enter valid image Url"
  };

  get images(): FormArray {
    return <FormArray>this.productDetailForm.get('images');
  }

  constructor(private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private _exDialog: ExDialog,
    private _notify: ShowNotificationService,
    private _cusineService: AdminProductService,
    private _subCusineService: AdminSubProductsService,
    private _service: AdminSubProductsDetailService) {

    // Defines all of the validation messages for the form.
    // These could instead be retrieved from a file or database.
    this.validationMessages = {
      productName: {
        required: 'Menu name is required.',
        minlength: 'Menu name must be at least 3 characters.',
        maxlength: 'Menu name cannot exceed 50 characters.'
      },
      productDescription: {
        required: 'Menu description is required.',
        minlength: 'Menu description must be at least 20 characters.',
      },
      rate: {
        range: 'Rate the Menu between 50 (lowest) and 500 (highest).'
      },
      imageUrl: {
        required: "Please enter image Url",
        pattern: "Please enter valid image Url"
      },
      servePeoples: {
        required: "This field is required",
      }
    };

    // Define an instance of the validator for use with this form, 
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);

    this.getCusineList();

  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
    this.flag = this.route.snapshot.paramMap.get('flag');

    if (this.flag === "D") this.deleteProduct();

    this.headerTitle = 'Add Menu Detail';
    if (this.id) this.getProduct(this.id);

    this.productDetailForm = this.fb.group({
      cusineId: 0,
      subCusineId: 0,
      productName: ['', [Validators.required, Validators.minLength(3)]],
      productDescription: ['n/a', [Validators.required, Validators.minLength(20)]],
      ingredients: '',
      receipe: '',
      rateGroup: this.fb.group({
        rate: [0, NumberValidators.range(50, 500)],
        discount: 0,
        servePeoples: ['', Validators.required],
        quantity: '',
      }),
      images: this.fb.array(['']),
      // imageUrl: ['', [Validators.required, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+')]],
      isSpecial: false,
      isVeg: true,
      isActive: true
    });

    //Watching the changes for product name
    this.productDetailForm
      .get('productName')
      .valueChanges
      .subscribe(value => console.log(value));

    const cusineListControl = this.productDetailForm.get('cusineId');
    cusineListControl.valueChanges.subscribe(value => this.getSubCusineList(this.cusineSelected));

    const subCusineListControl = this.productDetailForm.get('subCusineId');
    subCusineListControl.valueChanges.subscribe(value => this.setSubCusineId(this.subCusineSelected));
  }

  setSubCusineId(subCusineId){
    this.subCusineId =subCusineId.id;
  }

  getCusineList() {
    this._cusineService
      .selectCusineList("http://localhost:49765/api/MainMenu/SelectCuisine/")
      .subscribe(
      data => this.cusineList = data.model,
      error => {
        this._notify.showError(error)
      });
  }

  getSubCusineList(cusineId) {
    console.log(cusineId.id);
    if (cusineId.id) {
      this._subCusineService
        .selectSubCusineList("http://localhost:49765/api/Menu/SelectSubCuisine" + '/' + cusineId.id)
        .subscribe(
        data => this.subCusineList = data.model,
        error => {
          this._notify.showError(error)
        });
    }
  }

  addImage(): void {
    this.images.push(new FormControl());
  }

  removeImage = (i: number): void => {
    this.images.removeAt(i);
  }

  setMessage(c: AbstractControl): void {
    this.imageUrlMessage = '';
    if ((c.touched || c.dirty) && c.errors) {
      console.log('fire');
      this.imageUrlMessage = Object.keys(c.errors).map(key => this.imageUrlValidationMessage[key]).join(' ');
    }
  }

  setNotification(notifyVia: string): void {
    const recipeControl = this.productDetailForm.get('recipe');
    if (notifyVia === 'text') {
      recipeControl.setValidators(Validators.required);
    } else {
      recipeControl.clearValidators();
    }
    recipeControl.updateValueAndValidity();
  }

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

    // Merge the blur event observable with the valueChanges observable
    Observable.merge(this.productDetailForm.valueChanges, ...controlBlurs).debounceTime(800).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.productDetailForm);
    });
  }

  async getProduct(id: number): Promise<void> {
    await this._service.get(id)
      .subscribe(
      (product: ProductDetail) => this.onProductRetrieved(product),
      (error: any) => this.errorMessage = <any>error);
  }

  onProductRetrieved(product: any): void {
    // if (this.productDetailForm) {
    //   this.productDetailForm.reset();
    // }
    this.productDetail = product.model;

    if (this.id === 0) {
      this.headerTitle = 'Add Menu';
    } else {
      this.headerTitle = 'Edit Menu: ' + this.productDetail.name
    }

    // Update the data on the form
    this.productDetailForm.patchValue({
      productName: this.productDetail.name,
      productDescription: this.productDetail.description,
      ingredients: this.productDetail.ingredients,
      receipe: this.productDetail.receipe,
      // rate: this.productDetail.rate,
      // discount: this.productDetail.discount,
      // servePeoples: this.productDetail.servePeoples,
      // quantity: this.productDetail.quantity,
      isSpecial: this.productDetail.isSpecial,
      isVeg: this.productDetail.isVeg,
      isActive: this.productDetail.isActive
    });
    this.cusineName = this.productDetail.mainMenuName;
    this.subCusineName = this.productDetail.menuName;
    this.subCusineId=this.productDetail.menuId;
    let rateGroup: any = this.productDetailForm.controls['rateGroup'];
    rateGroup.controls['rate'].setValue(this.productDetail.rate);
    rateGroup.controls['discount'].setValue(this.productDetail.discount);
    rateGroup.controls['servePeoples'].setValue(this.productDetail.servePeoples);
    rateGroup.controls['quantity'].setValue(this.productDetail.quantity);

    //this.productDetailForm.setControl('tags', this.fb.array(this.product.tags || []));
    this.productDetailForm.setControl('images', this.fb.array([this.productDetail.imagePath]));
  }

  async deleteProduct(): Promise<void> {
    if (this.productDetail.id === 0) {
      // Don't delete, it was never saved.
      this.onSaveComplete();
    } else {
      if (confirm('Really delete the product: ${this.productDetail.name}?')) {
        await this._service.delete(this.productDetail.id)
          .subscribe(
          () => this.onSaveComplete(),
          (error: any) => this.errorMessage = <any>error
          );
      }
    }
  }

  convertProductDetail(product): any {
    return {
      "mainMenuId": 0,
      "mainMenuName": "string",
      "id": product.id,
      "menuId": this.subCusineId,
      "menuName": this.subCusineId.value,
      "name": product.productName,
      "description": product.productDescription,
      "ingredients": product.ingredients,
      "receipe": product.receipe,
      "rate": product.rateGroup.rate,
      "discount": product.rateGroup.discount,
      "servePeoples": product.rateGroup.servePeoples,
      "quantity": product.rateGroup.quantity,
      "isSpecial": product.isSpecial,
      "isVeg": product.isVeg,
      "imagePath": product.images[0],
      "imageDetails": [
        {
          "menuDetailId": 0,
          "imagePath": product.images[0],
          "imageDesc": "string"
        }
      ],
      "isActive": product.isActive,
    }
  }


  async save(): Promise<void> {
    if (this.productDetailForm.dirty && this.productDetailForm.valid) {
      // Copy the form values over the product object values
      let productDetail = Object.assign({}, this.productDetail, this.productDetailForm.value);

      if (this.id) this.updateProduct(productDetail);
      else this.createProduct(productDetail);

    } else if (!this.productDetailForm.dirty) {
      this.onSaveComplete();
    }
  }

  async createProduct(productDetail: ProductDetail) {
    await this._service.create(this.convertProductDetail(productDetail))
      .subscribe(() => {
        this.onSaveComplete()
        this._notify.showInfo('Product detail added successfully !')
      }, (error: AppError) => {
        this.errorMessage = <any>error
        this._notify.showError(error)
      })
  };

  async updateProduct(productDetail: ProductDetail) {
    productDetail.id = this.id;
    await this._service.update(this.convertProductDetail(productDetail))
      .subscribe(() => {
        this.onSaveComplete()
        this._notify.showInfo('Product detail updated successfully !')
      }, (error: AppError) => {
        this.errorMessage = <any>error
        this._notify.showError(error)
      })
  }

  onSaveComplete(): void {
    // Reset the form to clear the flags
    //this.productDetailForm.reset();
    this.router.navigate(['/admin/productDetails']);
  }
}
