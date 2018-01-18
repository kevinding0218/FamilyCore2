import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class EntreeHelperService {
  private readonly apiEndPoint: string = 'http://localhost:49934/api/entreeHelper';
  constructor(private _http: Http) { }

  getEntreeHelperDropdownItems(attribute, currentEntreeId) {
    return this._http.get(this.apiEndPoint + '/attribute?attribute=' + attribute + '&currentEntreeId=' + currentEntreeId)
      .map(res => res.json());

    /* // Initialize Params Object
    let httpParams = new HttpParams()
      .set('attribute', attribute)
      .set('currentEntreeId', currentEntreeId);

    // Make the API call using the new parameters.
    return this._http.get(this.apiEndPoint + '/attribute', { params: httpParams })
      .map(res => res.json()); */
  }
}