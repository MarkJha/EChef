import { CartService } from '../../shopping-cart/cart.service';
import { Component, Input, OnInit } from '@angular/core';
import { ProductDetail } from '../../models/productDetail';

@Component({
  selector: 'product-quantity',
  templateUrl: './product-quantity.component.html',
  styleUrls: ['./product-quantity.component.css']
})
export class ProductQuantityComponent  {
  @Input('product') product: ProductDetail;
  @Input('shopping-cart') shoppingCart; 

  constructor(private cartService: CartService) { }

  addToCart() {
    this.cartService.addToCart(this.product);
  }

  removeFromCart() {
    this.cartService.removeFromCart(this.product);
  }


}