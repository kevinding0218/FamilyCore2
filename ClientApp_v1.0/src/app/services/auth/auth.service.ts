import { Injectable } from '@angular/core';
import { AUTH_CONFIG } from './auth0-variables';
import { Router, NavigationStart } from '@angular/router';
import * as auth0 from 'auth0-js';
import Auth0Lock from 'auth0-lock';

@Injectable()
export class AuthService {
    auth0 = new auth0.WebAuth({
        clientID: AUTH_CONFIG.clientID,
        domain: AUTH_CONFIG.domain,
        responseType: 'token id_token',
        audience: `https://${AUTH_CONFIG.domain}/userinfo`,
        redirectUri: AUTH_CONFIG.callbackURL,
        scope: 'openid email profile'
    });

    userProfile: any;
    constructor(public router: Router) {}

    public login(): void {
        this.auth0.authorize();
    }

    public handleAuthentication(): void {
        this.auth0.parseHash((err, authResult) => {
            console.log('authResult: ', authResult);
            if (authResult && authResult.accessToken && authResult.idToken) {
                this.setSession(authResult);
                this.router.navigate(['/dashboard']);
            } else if (err) {
                this.router.navigate(['/pages/login']);
                console.log(err);
                alert(`Error: ${err.error}. Check the console for further details.`);
            } else {
                this.router.navigate(['/dashboard']);
            }
        });
    }

    public getProfile(cb): void {
        const accessToken = localStorage.getItem('access_token');
        if (!accessToken) {
          throw new Error('Access token must exist to fetch profile');
        }
    
        const self = this;
        this.auth0.client.userInfo(accessToken, (err, profile) => {
          if (profile) {
            console.log('accessToken: ', accessToken);
            console.log('profile: ', profile);
            self.userProfile = profile;
          }
          cb(err, profile);
        });
      }

    private setSession(authResult): void {
        // Set the time that the access token will expire at
        const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
        localStorage.setItem('access_token', authResult.accessToken);
        localStorage.setItem('id_token', authResult.idToken);
        localStorage.setItem('expires_at', expiresAt);
    }

    public logout(): void {
        // Remove tokens and expiry time from localStorage
        localStorage.removeItem('access_token');
        localStorage.removeItem('id_token');
        localStorage.removeItem('expires_at');
        this.userProfile = null;
        // Go back to the home route
        this.router.navigate(['/']);
    }

    public isAuthenticated(): boolean {
        // Check whether the current time is past the
        // access token's expiry time
        const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
        return new Date().getTime() < expiresAt;
    }

    private roles: string[] = [];
    public isInRole(roleName) {
        return this.roles.indexOf(roleName) > -1;
    }
}


// import { Injectable } from '@angular/core';
// import { AUTH_CONFIG } from './auth0-variables';
// import { Router, NavigationStart } from '@angular/router';
// import 'rxjs/add/operator/filter';
// import Auth0Lock from 'auth0-lock';
// import * as auth0 from 'auth0-js';

// @Injectable()
// export class AuthService {
//     //lock = new Auth0Lock(AUTH0_CLIENT_ID, AUTH0_DOMAIN, {});
//   auth0 = new auth0.WebAuth({clientID: AUTH_CONFIG.clientID, domain: AUTH_CONFIG.domain});

//   lock = new Auth0Lock(AUTH_CONFIG.clientID, AUTH_CONFIG.domain, {
//     autoclose: true,
//     auth: {
//       redirectUrl: AUTH_CONFIG.callbackURL,
//       responseType: 'token id_token',
//       audience: `https://${AUTH_CONFIG.domain}/userinfo`,
//       params: {
//         scope: 'openid'
//       }
//     }
//   });

//   constructor(public router: Router) {
//      this.profile = JSON.parse(localStorage.getItem('profile'));
//      this.lock.on('authenticated', authResult => {
//         localStorage.setItem('id_token', authResult.idToken);

        //     this.lock.getUserInfo(authResult.accessToken, (error, profile) => {
        //         if (error)
        //             throw error;
                
        //         //localStorage.setItem('profile', JSON.stringify(profile));
        //         console.log('profile:', profile);

        //     })
//       });

//       this.lock.on('authorization_error', authResult => {
//         console.log(authResult);
//       });

//       this.handleRedirectWithHash();
//   }

//   public login(): void {
//     this.lock.show();
//   }

//   private handleRedirectWithHash() {
//     this.router.events.take(1).subscribe(event => {
//       if (/access_token/.test(event.url) || /error/.test(event.url)) {  

//         let authResult = this.auth0.parseHash(window.location.hash);

//         if (authResult && authResult.idToken) {
//           this.lock.emit('authenticated', authResult);
//         }

//         if (authResult && authResult.error) {
//           this.lock.emit('authorization_error', authResult);
//         }
//       }
//     });
//   }

//   // Call this method in app.component.ts
//   // if using path-based routing
//   public handleAuthentication(): void {
//     this.lock.on('authenticated', (authResult) => {
//       if (authResult && authResult.accessToken && authResult.idToken) {
//         this.setSession(authResult);
//         this.router.navigate(['/']);
//       }
//     });
//     this.lock.on('authorization_error', (err) => {
//       this.router.navigate(['/']);
//       console.log(err);
//       alert(`Error: ${err.error}. Check the console for further details.`);
//     });
//   }

//   // Call this method in app.component.ts
//   // if using hash-based routing
//   public handleAuthenticationWithHash(): void {
//     this
//       .router
//       .events
//       .filter(event => event instanceof NavigationStart)
//       .filter((event: NavigationStart) => (/access_token|id_token|error/).test(event.url))
//       .subscribe(() => {
//         this.lock.resumeAuth(window.location.hash, (err, authResult) => {
//           if (err) {
//             this.router.navigate(['/']);
//             console.log(err);
//             alert(`Error: ${err.error}. Check the console for further details.`);
//             return;
//           }
//           this.setSession(authResult);
//           this.router.navigate(['/']);
//         });
//     });
//   }

//   private setSession(authResult): void {
//     // Set the time that the access token will expire at
//     const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
//     localStorage.setItem('access_token', authResult.accessToken);
//     localStorage.setItem('id_token', authResult.idToken);
//     localStorage.setItem('expires_at', expiresAt);
//   }

//   public logout(): void {
//     // Remove tokens and expiry time from localStorage
//     localStorage.removeItem('access_token');
//     localStorage.removeItem('id_token');
//     localStorage.removeItem('expires_at');
//     // Go back to the home route
//     this.router.navigate(['/']);
//   }

//   public isAuthenticated(): boolean {
//     // Check whether the current time is past the
//     // access token's expiry time
//     const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
//     return new Date().getTime() < expiresAt;
//   }

// }
