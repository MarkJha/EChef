import { Observable } from 'rxjs/Rx';
import { ShoppingCart } from '../models/shoppingCart';
import { Injectable } from '@angular/core';
import { ProductDetail } from '../models/productDetail';

@Injectable()
export class CartService {
  db: any;
  cartList = [];

  constructor() { }

  async getCart(): Promise<Observable<ShoppingCart>> {
    let cartId = await this.getOrCreateCartId();
    return this.db.object('/shopping-carts/' + cartId)
      .map(x => new ShoppingCart(x.items));
  }

  async addToCart(product: ProductDetail) {
    // this.updateItem(product, 1);
    this.cartList.push(product);
    return this.getCartTotal();
  }

  async getCartTotal() {
    return this.cartList.length;
  }

  async removeFromCart(product: ProductDetail) {
    // this.updateItem(product, -1);   
  }

  async clearCart() {
    let cartId = await this.getOrCreateCartId();
    this.db.object('/shopping-carts/' + cartId + '/items').remove();
  }


  private create() {
    return this.db.list('/shopping-carts').push({
      dateCreated: new Date().getTime()
    });
  }

  private getItem(cartId: string, productId: string) {
    return this.db.object('/shopping-carts/' + cartId + '/items/' + productId);
  }

  private async getOrCreateCartId(): Promise<string> {
    let cartId = localStorage.getItem('cartId');
    if (cartId) return cartId;

    let result = await this.create();
    localStorage.setItem('cartId', result.key);
    return result.key;
  }

  private async updateItem(product: ProductDetail, change: number) {
    let cartId = await this.getOrCreateCartId();
    let item$ = this.getItem(cartId, String(product.id));
    item$.take(1).subscribe(item => {
      let quantity = (item.quantity || 0) + change;
      if (quantity === 0) item$.remove();
      else item$.update({
        title: product.name,
        imageUrl: product.imagePath,
        price: product.rate,
        quantity: quantity
      });
    });
  }
}
