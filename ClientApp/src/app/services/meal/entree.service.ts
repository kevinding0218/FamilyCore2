import { SaveEntree } from './../../viewModels/meal/entree';
import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class EntreeService {
  private readonly apiPort: string = localStorage.getItem('WebApiPath').toString();
  private readonly apiEndPoint: string = this.apiPort + '/entree';
  constructor(private _http: Http) { }

  getEntree(id) {
    return this._http.get(this.apiEndPoint + '/' + id)
      .map(res => res.json());
  }


  //api/entree/group?splitBy=a&id=b
  getEntrees(splitBy, id) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let authToken = localStorage.getItem('auth_token');
    headers.append('Authorization', `Bearer ${authToken}`);
    console.log('headers is ', headers);

    return this._http.get(this.apiEndPoint + '/group?splitBy=' + splitBy + '&id=' + id, {headers})
      .map(res => res.json());
  }

  createEntree(entree: SaveEntree) {
    return this._http.post(this.apiEndPoint, entree)
      .map(res => res.json());
  }

  updateEntree(entree: SaveEntree) {
    return this._http.put(this.apiEndPoint + '/' + entree.id, entree)
      .map(res => res.json());
  }

  deleteEntree(id) {
    return this._http.delete(this.apiEndPoint + '/' + id)
      .map(res => res.json());
  }

}