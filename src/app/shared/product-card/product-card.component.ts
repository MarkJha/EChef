import { ShoppingCart } from '../../models/shoppingCart';
import { ProductDetail } from '../../models/productDetail';
import { Component, Input, OnInit } from '@angular/core';
import { CartService } from '../../shopping-cart/cart.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {

  @Input('product') product: ProductDetail;
  @Input('show-actions') showAction: boolean = true;
  @Input('shopping-cart') shoppingCart: ShoppingCart;

  constructor(private cartService: CartService) { }

  ngOnInit() {
  }

  addItem(product) {
    let itemsCount = this.cartService.addToCart(product);
    console.log(itemsCount);
  }
}
