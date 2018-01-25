import { SpinnerService } from './shared/spinner/spinner.service';
import { SpinnerComponent } from './shared/spinner/spinner.component';
import { SearchPanelComponent } from './shared/search-panel/search-panel.component';
import { AdminSubProductsDetailService } from './admin/admin-sub-products-detail/admin-sub-products-detail.service';
import { ShowNotificationService } from './shared/common/show-notification';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTableModule } from 'angular-4-data-table';
import { CustomFormsModule } from 'ng2-validation';
import { ToastrModule } from 'ngx-toastr';
import { DialogModule } from './NgExDialog/dialog.module';

import { AdminOrdersComponent } from './admin/admin-orders/admin-orders.component';
import { AdminProductsComponent } from './admin/admin-products/admin-products.component';
import { AdminProductService } from './admin/admin-products/admin-products.service';
import { ProductFormComponent } from './admin/product-form/product-form.component';
import { AppComponent } from './app.component';
import { CheckOutComponent } from './check-out/check-out.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { OrderSuccessComponent } from './order-success/order-success.component';
import { ProductsComponent } from './products/products.component';
import { SearchFoodComponent } from './search-food/search-food.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { SummeryTextPipe } from './shared/pipes/summery-text.pipe';
import { ConvertToSpacesPipe } from './shared/pipes/convert-to-spaces.pipe';
import { AdminSubProductsComponent } from './admin/admin-sub-products/admin-sub-products.component';
import { AdminSubProductsDetailComponent } from './admin/admin-sub-products-detail/admin-sub-products-detail.component';
import { AdminSubProductsService } from './admin/admin-sub-products/admin-sub-products.service';
import { SubProductFormComponent } from './admin/sub-product-form/sub-product-form.component';
import { SubProductDetailFormComponent } from './admin/sub-product-detail-form/sub-product-detail-form.component';
import { PaginationComponent } from './shared/pagination/pagination.component';
import { ProductFilterComponent } from './shared/product-filter/product-filter.component';
import { ProductCardComponent } from './shared/product-card/product-card.component';
import { CartService } from './shopping-cart/cart.service';
import { ProductQuantityComponent } from './shared/product-quantity/product-quantity.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    ProductsComponent,
    ShoppingCartComponent,
    CheckOutComponent,
    OrderSuccessComponent,
    MyOrdersComponent,
    AdminProductsComponent,
    AdminOrdersComponent,
    LoginComponent,
    SearchFoodComponent,
    ProductFormComponent,
    SummeryTextPipe,
    ConvertToSpacesPipe,
    AdminSubProductsComponent,
    AdminSubProductsDetailComponent,
    SearchPanelComponent,
    SpinnerComponent,
    SubProductFormComponent,
    SubProductDetailFormComponent,
    PaginationComponent,
    ProductFilterComponent,
    ProductCardComponent,
    ProductQuantityComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    CustomFormsModule,
    HttpModule,
    HttpClientModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
      newestOnTop: true,
      autoDismiss: true,
      closeButton: true,
      progressBar: true
    }), // ToastrModule added
    DataTableModule,
    DialogModule,
    NgbModule.forRoot(),
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent },
      { path: 'products', component: ProductsComponent },
      { path: 'shopping-cart', component: ShoppingCartComponent },
      { path: 'check-out', component: CheckOutComponent },
      { path: 'order-success', component: OrderSuccessComponent },
      { path: 'search', component: SearchFoodComponent },
      { path: 'login', component: LoginComponent },
      { 
        path: 'admin/products/new', 
        component: ProductFormComponent 
      },
      { 
        path: 'admin/products/:id', 
        component: ProductFormComponent 
      },
      { 
        path: 'admin/products/:id/:flag', 
        component: ProductFormComponent 
      },      
      { path: 'admin/products', component: AdminProductsComponent }, 
      { 
        path: 'admin/subproducts/new', 
        component: SubProductFormComponent 
      },
      { 
        path: 'admin/subproducts/:id', 
        component: SubProductFormComponent 
      },
      { 
        path: 'admin/subproducts/:id/:flag', 
        component: SubProductFormComponent 
      },       
      { path: 'admin/subproducts', component: AdminSubProductsComponent },
      { 
        path: 'admin/productDetails/new', 
        component: SubProductDetailFormComponent 
      },
      { 
        path: 'admin/productDetails/:id', 
        component: SubProductDetailFormComponent 
      },
      { 
        path: 'admin/productDetails/:id/:flag', 
        component: SubProductDetailFormComponent 
      },
      { path: 'admin/productDetails', component: AdminSubProductsDetailComponent },
      { path: 'admin/orders', component: AdminOrdersComponent },
      { path: '', component: ProductsComponent },
    ])
  ],
  providers: [
    AdminProductService,
    ShowNotificationService,
    AdminSubProductsService,
    AdminSubProductsDetailService,
    SpinnerService,
    CartService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
