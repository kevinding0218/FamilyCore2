import { HelperMethod } from './../../utility/helper/helperMethod';
import { SaveEntreeDetail } from './../../viewModels/meal/entreeDetail';

import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class EntreeDetailService {
  private readonly apiPort: string = localStorage.getItem('WebApiPath').toString();
  private readonly apiEndPoint: string = this.apiPort + '/entreeDetail';
  headers = new Headers();
  opts = new RequestOptions();
  
  constructor(private _http: Http) { 
    HelperMethod.generateHttpHeaderWithJwtToken(this.headers);
    this.opts.headers = this.headers;
  }

  getEntreeDetail(id) {
    return this._http.get(this.apiEndPoint + '/id?=' + id, this.opts)
      .map(res => res.json());
  }

  create(entreeDetail: SaveEntreeDetail) {
    return this._http.post(this.apiEndPoint, entreeDetail, this.opts)
      .map(res => res.json());
  }

  update(entreeDetail: SaveEntreeDetail) {
    return this._http.put(this.apiEndPoint + '/' + entreeDetail.keyValuePairInfo.id, entreeDetail, this.opts)
      .map(res => res.json());
  }

  delete(id) {
    return this._http.delete(this.apiEndPoint + '/' + id, this.opts)
      .map(res => res.json());
  }

  getEntreeDetails(entreeDetailType) {
    return this._http.get(this.apiEndPoint + '/' + entreeDetailType, this.opts)
      .map(res => res.json());
  }
}