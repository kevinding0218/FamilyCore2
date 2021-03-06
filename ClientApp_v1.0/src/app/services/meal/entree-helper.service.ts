import { SimilarEntreeInputObj } from './../../viewModels/meal/entree';
import { HttpParams, HttpClient, HttpHeaders  } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class EntreeHelperService {
  private readonly apiPort: string = localStorage.getItem('WebApiPath').toString();
  private readonly apiEndPoint: string = this.apiPort + '/entreeHelper';
  constructor(private _http: Http, private _httpClient: HttpClient) { }

  getEntreeHelperDropdownItems(attribute, currentEntreeId) {
    // return this._http.get(this.apiEndPoint + '/getEntreeHelperDropdownItems?attribute=' + attribute + '&currentEntreeId=' + currentEntreeId)
    //   .map(res => res.json());

    // Initialize Params Object
    let httpParams = new HttpParams()
      .set('attribute', attribute)
      .set('currentEntreeId', currentEntreeId);

    // Make the API call using the new parameters, here instead of using _http from Http, we should use HttpClient
    // constructor(private _http: HttpClient) { }
    // also in app.module, need to import HttpClientModule 
    // import { HttpClientModule } from '@angular/common/http';

    return this._httpClient.get(this.apiEndPoint + '/getEntreeHelperDropdownItems?', { params: httpParams });
  }

  getSimilarEntreeList(entreeInputObj: SimilarEntreeInputObj) {
    return this._http.post(this.apiEndPoint + '/getSimilarEntreeList', entreeInputObj)
      .map(res => res.json());
  }

  getEntreeStyleOrCatagoryById(splitBy, splitId) {
    return this._http.get(this.apiEndPoint + '/getEntreeStyleOrCatagory?splitBy=' + splitBy + '&splitId=' + splitId)
      .map(res => res.json());
  }

  getEntreeCountBySplit(splitBy) {
    return this._http.get(this.apiEndPoint + '/getEntreeCountBySplit?splitBy=' + splitBy)
      .map(res => res.json());
  }
}