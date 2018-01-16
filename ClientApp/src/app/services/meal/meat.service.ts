import { SaveEntreeDetail } from './../../viewModels/meal/entreeDetail';

import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class MeatService {
  private readonly serviceApiEndPoint: string = 'http://localhost:49934/api/entreeDetail';
  private readonly debugApiEndPoint: string = 'http://localhost:5000/api/entreeDetail';
  private isDebug: boolean = false;
  private readonly apiEndPoint: string = this.isDebug ? this.debugApiEndPoint : this.serviceApiEndPoint;
  constructor(private _http: Http) { }

  getMeat(id) {
    return this._http.get(this.apiEndPoint + '/' + id)
      .map(res => res.json());
  }

  create(meat: SaveEntreeDetail) {
    console.log('In Create');
    console.log(meat);
    return this._http.post(this.apiEndPoint, meat)
      .map(res => res.json());
  }

  update(meat: SaveEntreeDetail) {
    return this._http.put(this.apiEndPoint + '/' + meat.keyValuePairInfo.id, meat)
      .map(res => res.json());
  }

  delete(id) {
    return this._http.delete(this.apiEndPoint + '/' + id)
      .map(res => res.json());
  }

  getMeats() {
    return this._http.get(this.apiEndPoint + '/meat')
      .map(res => res.json());
  }
}
