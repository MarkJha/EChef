import { SubProduct } from '../../models/subProduct';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiDataService } from '../../shared/services/api-data.service';

@Injectable()
export class AdminSubProductsService extends ApiDataService<SubProduct> {

  constructor(http: HttpClient) {
    super('Menu/SubCusine', http);    
   }

   selectSubCusineList(url){  
    return super.getAllByCustomUrl(url);
  }

}
