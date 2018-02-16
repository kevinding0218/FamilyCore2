import { SaveEntree } from './../../viewModels/meal/entree';
import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { HelperMethod } from '../../utility/helper/helperMethod';


@Injectable()
export class EntreeService {
  private readonly apiPort: string = localStorage.getItem('WebApiPath').toString();
  private readonly apiEndPoint: string = this.apiPort + '/entree';
  headers = new Headers();
  opts = new RequestOptions();
  
  constructor(private _http: Http) { 
    HelperMethod.generateHttpHeaderWithJwtToken(this.headers);
    this.opts.headers = this.headers;
  }

  getEntree(id) {
    return this._http.get(this.apiEndPoint + '/' + id)
      .map(res => res.json());
  }


  //api/entree/group?splitBy=a&id=b
  getEntrees(splitBy, id) {
    // let headers = new Headers();
    // headers.append('Content-Type', 'application/json');
    // let authToken = localStorage.getItem('auth_token');
    // headers.append('Authorization', `Bearer ${authToken}`);
    // console.log('headers is ', headers);
    //HelperMethod.generateHttpHeaderWithJwtToken(headers);

    return this._http.get(this.apiEndPoint + '/group?splitBy=' + splitBy + '&id=' + id, this.opts)
      .map(res => res.json());
  }

  createEntree(entree: SaveEntree) {
    return this._http.post(this.apiEndPoint, entree, this.opts)
      .map(res => res.json());
  }

  updateEntree(entree: SaveEntree) {
    return this._http.put(this.apiEndPoint + '/' + entree.id, entree, this.opts)
      .map(res => res.json());
  }

  deleteEntree(id) {
    return this._http.delete(this.apiEndPoint + '/' + id)
      .map(res => res.json());
  }

}