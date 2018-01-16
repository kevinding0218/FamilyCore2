import { SaveMeat } from './../../viewModels/meal/meat';

import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class MeatService {
  private readonly serviceApiEndPoint: string = 'http://localhost:49934/api/meat';
  private readonly debugApiEndPoint: string = 'http://localhost:5000/api/meat';
  private isDebug: boolean = false;
  private readonly apiEndPoint: string = this.isDebug ? this.debugApiEndPoint : this.serviceApiEndPoint;
  constructor(private _http: Http) { }

  getMeat(id) {
    return this._http.get(this.apiEndPoint + '/' + id)
      .map(res => res.json());
  }

  create(meat: SaveMeat) {
    console.log('In Create');
    console.log(meat);
    return this._http.post(this.apiEndPoint, meat)
      .map(res => res.json());
  }

  update(meat: SaveMeat) {
    return this._http.put(this.apiEndPoint + '/' + meat.keyValuePairInfo.id, meat)
      .map(res => res.json());
  }

  delete(id) {
    return this._http.delete(this.apiEndPoint + '/' + id)
      .map(res => res.json());
  }

  getMeats() {
    return this._http.get(this.apiEndPoint)
      .map(res => res.json());
  }
}
