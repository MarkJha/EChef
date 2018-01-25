import { CartOrder, orderList } from '../../admin/admin-orders/cart-order';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent {

  filteredItems: CartOrder[];
  pages: number = 4;
  pageSize: number = 5;
  pageNumber: number = 0;
  currentIndex: number = 1;
  items: CartOrder[];
  pagesIndex: Array<number>;
  pageStart: number = 1;
  inputName: string = '';

  constructor() {
    this.filteredItems = orderList;
    this.init();
  };

  init() {
    this.currentIndex = 1;
    this.pageStart = 1;
    this.pages = 4;

    this.pageNumber = parseInt("" + (this.filteredItems.length / this.pageSize));
    if (this.filteredItems.length % this.pageSize != 0) {
      this.pageNumber++;
    }

    if (this.pageNumber < this.pages) {
      this.pages = this.pageNumber;
    }

    this.refreshItems();
    console.log("this.pageNumber :  " + this.pageNumber);
  }

  FilterByName() {
    this.filteredItems = [];
    if (this.inputName != "") {
      orderList.forEach(element => {
        if (element.name.toUpperCase().indexOf(this.inputName.toUpperCase()) >= 0) {
          this.filteredItems.push(element);
        }
      });
    } else {
      this.filteredItems = orderList;
    }
    console.log(this.filteredItems);
    this.init();
  }

  //sorting
  key: string = 'name';
  reverse: boolean = false;
  iconClass:string;
  sort(key) {    
    this.key = key;
    this.reverse = !this.reverse;
    if(this.reverse){
      this.iconClass="fa fa-chevron-up";
    }else{
      this.iconClass="fa fa-chevron-down";
    }
    console.log(this.reverse);
    this.filteredItems = orderList.sort(this.sort_by(this.key, this.reverse, function (a) { return a.toUpperCase() }))
    this.init();
  }

  sort_by(field, reverse, primer) {
    var key = primer ?
      function (x) { return primer(x[field]) } :
      function (x) { return x[field] };

    reverse = !reverse ? 1 : -1;
    return function (a, b) {
      //return a = key(a), b = key(b), reverse * ((a > b) - (b > a));
      return (a < b ? -1 : (a > b ? 1 : 0)) * [1, -1][+!!reverse];
    }
  }

  fillArray(): any {
    var obj = new Array();
    for (var index = this.pageStart; index < this.pageStart + this.pages; index++) {
      obj.push(index);
    }
    return obj;
  }

  refreshItems() {
    this.items = this.filteredItems.slice((this.currentIndex - 1) * this.pageSize, (this.currentIndex) * this.pageSize);
    this.pagesIndex = this.fillArray();
  }

  prevPage() {
    if (this.currentIndex > 1) {
      this.currentIndex--;
    }
    if (this.currentIndex < this.pageStart) {
      this.pageStart = this.currentIndex;
    }
    this.refreshItems();
  }

  nextPage() {
    if (this.currentIndex < this.pageNumber) {
      this.currentIndex++;
    }
    if (this.currentIndex >= (this.pageStart + this.pages)) {
      this.pageStart = this.currentIndex - this.pages + 1;
    }

    this.refreshItems();
  }

  setPage(index: number) {
    this.currentIndex = index;
    this.refreshItems();
  }



}
