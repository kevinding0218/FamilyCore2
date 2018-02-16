import { RegisterInfo } from './../../../viewModels/user/user';
import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class UserPasswordService {
private readonly apiPort: string = localStorage.getItem('WebApiPath').toString();
private readonly apiEndPoint: string = this.apiPort + '/userpassword';
  constructor(private _http: Http) { }

  create(newUser: RegisterInfo) {
    return this._http.post(this.apiEndPoint + '/register', newUser)
      .map(res => res.json());
  }

  verify(loginUser: RegisterInfo) {
    return this._http.post(this.apiEndPoint + '/verify', loginUser)
      .map(res => res.json());
  }
}
