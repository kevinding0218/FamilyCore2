import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class EntreeService {
  private readonly apiEndPoint: string = 'http://localhost:49934/api/entree';
  constructor(private _http: Http) { }

  getEntree(id) {
    return this._http.get(this.apiEndPoint + '/id?=' + id)
      .map(res => res.json());
  }

  //api/entree/group?splitBy=a&id=b
  getEntrees(splitBy, id) {
    return this._http.get(this.apiEndPoint + '/group?splitBy=' + splitBy + '&id=' + id)
      .map(res => res.json());
  }
}