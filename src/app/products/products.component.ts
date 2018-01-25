import { Observable } from 'rxjs/Rx';
import { ShoppingCart } from '../models/shoppingCart';
import { ActivatedRoute } from '@angular/router';
import { AdminSubProductsDetailService } from '../admin/admin-sub-products-detail/admin-sub-products-detail.service';
import { ShowNotificationService } from '../shared/common/show-notification';
import { Component, OnInit } from '@angular/core';
import { ProductDetail } from '../models/productDetail';
import { CartService } from '../shopping-cart/cart.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  cart$: Observable<ShoppingCart>;
  category: string;
  categoryId: number;
  productList: ProductDetail[] = [];
  loading: boolean = false;
  noRecordFound: boolean;
  constructor(
    private route: ActivatedRoute,
    private _notify: ShowNotificationService,
    private _service: AdminSubProductsDetailService,
 ) {
  //private cartService:CartService
    this.allProducts();
    this.route.queryParamMap.subscribe(params => {
      this.category = params.get('category');
      this.categoryId = +params.get('id');      
    })
  }

  async ngOnInit() {
   // this.cart$ = await this.cartService.getCart();   
  }

  allProducts(): any {
    this.loading = true;
    return this._service.getAll()
      .subscribe(
      data => {
        this.handleSuccess(data)
      },
      error => {
        this._notify.showError(error)
        this.loading = false;
      });
  }

  selectedProducts(id: number) {
    this.loading = true;
    let url = "MenuDetail/CusineDetail/SubMenu/" + id;
    return this._service.getAllProductBySubMenu(url)
      .subscribe(
      data => this.handleSuccess(data),
      error => {
        this._notify.showError(error)
        this.loading = false;
      });
  }

  handleSuccess(productList): any {
    if (productList.model.length === 0) {
      this.noRecordFound = true;
      this.productList = [];
    } else {
      this.productList = productList.model;
      this.noRecordFound = false;
    }
    this.loading = false;
  }

}
