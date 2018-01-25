import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Product } from '../../models/product';
import { ApiDataService } from '../../shared/services/api-data.service';

@Injectable()
export class AdminProductService extends ApiDataService<Product> {

  constructor(http: HttpClient) {
    super('MainMenu/Cuisine', http);    
   }

   selectCusineList(url){  
     return super.getAllByCustomUrl(url);
   }

}
