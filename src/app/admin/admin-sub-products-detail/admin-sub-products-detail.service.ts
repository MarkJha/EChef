import { ProductDetail } from '../../models/productDetail';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiDataService } from '../../shared/services/api-data.service';

@Injectable()
export class AdminSubProductsDetailService extends ApiDataService<ProductDetail> {

  constructor(http: HttpClient) {
    super('MenuDetail/CusineDetail', http);
   }

   getAllProductBySubMenu(url:string){
     return super.getAllByCustomUrl(url);
   }

}
