import { UserService } from './user.service';
// auth.guard.ts
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

@Injectable()
export class MemberAuthGuard implements CanActivate {
  constructor(private _userService: UserService,private router: Router) {}

  canActivate() {

    if(!this._userService.isLoggedIn())
    {
       this.router.navigate(['/pages/login']);
       return false;
    }

    return true;
  }
}