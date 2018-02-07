import { SaveEntreeDetail } from './../../viewModels/meal/entreeDetail';

import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class EntreeDetailService {
  private readonly apiPort: string = localStorage.getItem('WebApiPath').toString();
  private readonly apiEndPoint: string = this.apiPort + '/entreeDetail';
  constructor(private _http: Http) { }

  getEntreeDetail(id) {
    return this._http.get(this.apiEndPoint + '/id?=' + id)
      .map(res => res.json());
  }

  create(entreeDetail: SaveEntreeDetail) {
    return this._http.post(this.apiEndPoint, entreeDetail)
      .map(res => res.json());
  }

  update(entreeDetail: SaveEntreeDetail) {
    return this._http.put(this.apiEndPoint + '/' + entreeDetail.keyValuePairInfo.id, entreeDetail)
      .map(res => res.json());
  }

  delete(id) {
    return this._http.delete(this.apiEndPoint + '/' + id)
      .map(res => res.json());
  }

  getEntreeDetails(entreeDetailType) {
    return this._http.get(this.apiEndPoint + '/' + entreeDetailType)
      .map(res => res.json());
  }
}