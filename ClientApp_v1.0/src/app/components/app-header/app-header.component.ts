import { Component } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './app-header.component.html'
})
export class AppHeaderComponent { 
  constructor(public auth: AuthService) {
    auth.handleAuthentication();
   
    //auth.handleAuthenticationWithHash();
  } 
}
