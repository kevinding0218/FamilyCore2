import { SaveEntreeDetail } from './../../viewModels/meal/entreeDetail';

import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class StaplefoodService {
  private readonly apiPort: string = localStorage.getItem('WebApiPath').toString();
  private readonly apiEndPoint: string = this.apiPort + '/stapleFood';
  constructor(private _http: Http) { }

  getStaplefood(id) {
    return this._http.get(this.apiEndPoint + '/' + id)
      .map(res => res.json());
  }

  create(staplefood: SaveEntreeDetail) {
    console.log('In Create');
    console.log(staplefood);
    return this._http.post(this.apiEndPoint, staplefood)
      .map(res => res.json());
  }

  update(staplefood: SaveEntreeDetail) {
    return this._http.put(this.apiEndPoint + '/' + staplefood.keyValuePairInfo.id, staplefood)
      .map(res => res.json());
  }

  delete(id) {
    return this._http.delete(this.apiEndPoint + '/' + id)
      .map(res => res.json());
  }

  getStaplefoods() {
    return this._http.get(this.apiEndPoint)
      .map(res => res.json());
  }
}
