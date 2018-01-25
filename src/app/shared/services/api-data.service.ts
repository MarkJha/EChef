import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { ApiResponse } from '../../models/apiResponse';
import { ServerDown } from '../common/server-down';
import { AppError } from './../common/app-error';
import { BadInput } from './../common/bad-input';
import { NotFoundError } from './../common/not-found-error';


@Injectable()
export class ApiDataService<T> {
  baseUrl: string = "http://localhost:49765/api/";
  constructor(private url: string,
    private http: HttpClient) {
    this.url = this.baseUrl + url;
    console.log(this.url);
  }

  getAllByCustomUrl(customUrl: string) {
    customUrl = this.baseUrl + customUrl;
    console.log(customUrl);
    return this.http
      .get<ApiResponse<T>>(customUrl)
      .do(data => console.log('SelectList: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  getAll() {
    return this.http
      .get<ApiResponse<T>>(this.url)
      //.do(data => console.log('All: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  get(id: number) {
    return this.http.get<ApiResponse<T>>(this.url + '/' + id)
      .do(data => console.log('get: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  search(searchText: string) {
    return this.http.get<ApiResponse<T>>(this.url + '/Search/' + searchText)
      .do(data => console.log('get all by search: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  create(resource) {
    return this.http.post(this.url, resource)
      .do(data => console.log('create: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }


  update(resource) {
    return this.http.put(this.url, resource)
      .do(data => console.log('update: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  patch(resource) {
    return this.http.patch(this.url + '/' + resource.id, resource)
      .do(data => console.log('patch: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  delete(id: number) {
    return this.http.delete(this.url + '/' + id)
      .do(data => console.log('delete: ' + JSON.stringify(data)))
      .catch(this.handleError);
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0)
      return Observable.throw(new ServerDown(error));

    if (error.status === 400)
      return Observable.throw(new BadInput(error));

    if (error.status === 404)
      return Observable.throw(new NotFoundError(error));

    return Observable.throw(new AppError(error));
  }
}
