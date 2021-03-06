import { UserRegistration } from './../../viewModels/member/user.registration';
import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';


import { BaseService } from "./base.service";

import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';

// Add the RxJS Observable operators we need in this app.
//import '../../rxjs-operators';

@Injectable()

export class UserService extends BaseService {

    private readonly apiPort: string = localStorage.getItem('WebApiPath').toString();

    // Observable navItem source
    private _authNavStatusSource = new BehaviorSubject<boolean>(false);
    // Observable navItem stream and broadcast this _authNavStatusSource so we can monitor from anywhere
    authNavStatus$ = this._authNavStatusSource.asObservable();

    private loggedIn = false;

    constructor(private http: Http) {
        super();
        this.loggedIn = !!localStorage.getItem('auth_token');
        // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
        // header component resulting in authed user nav links disappearing despite the fact user is still logged in
        // next: assign this.loggedIn to _authNavStatusSource
        this._authNavStatusSource.next(this.loggedIn);
    }

    register(email: string, password: string, firstName: string, lastName: string, location: string): Observable<UserRegistration> {
        let body = JSON.stringify({ email, password, firstName, lastName, location });
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.apiPort + "/member", body, options)
            .map(res => true)
            .catch(this.handleError);
    }

    login(userName, password) {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this.http
            .post(
            this.apiPort + '/auth/login',
            JSON.stringify({ userName, password }), { headers }
            )
            .map(res => res.json())
            .map(res => {
                localStorage.setItem('auth_token', res.auth_token);
                this.loggedIn = true;
                this._authNavStatusSource.next(true);
                return true;
            })
            .catch(this.handleError);
    }

    logout() {
        localStorage.removeItem('auth_token');
        this.loggedIn = false;
        this._authNavStatusSource.next(false);
    }

    isLoggedIn() {
        return !!localStorage.getItem('auth_token');;
    }

    //   facebookLogin(accessToken:string) {
    //     let headers = new Headers();
    //     headers.append('Content-Type', 'application/json');
    //     let body = JSON.stringify({ accessToken });  
    //     return this.http
    //       .post(
    //       this.baseUrl + '/externalauth/facebook', body, { headers })
    //       .map(res => res.json())
    //       .map(res => {
    //         localStorage.setItem('auth_token', res.auth_token);
    //         this.loggedIn = true;
    //         this._authNavStatusSource.next(true);
    //         return true;
    //       })
    //       .catch(this.handleError);
    //   }
}

