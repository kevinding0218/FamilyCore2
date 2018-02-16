import { Auth0Lock } from 'auth0-lock';
import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router/src/interfaces';
import { AuthService } from '../auth/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(public auth: AuthService) { }

    canActivate() {
        if (this.auth.isAuthenticated())
            return true;

        window.location.href = "https://familycore.auth0.com/login?client=l21dXakumBhIcZrzLzGu8QaSzb7sDrAj";
    }
}