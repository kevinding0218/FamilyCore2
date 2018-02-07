import { Component } from '@angular/core';
import 'app/utility/extensions/stringExtension';
import 'app/utility/extensions/dateExtension';

@Component({
  // tslint:disable-next-line
  selector: 'body',
  templateUrl: './app.component.html'
})
export class AppComponent { 
  constructor() {
    localStorage.setItem('currentUser', 'false');
    localStorage.setItem('WebApiFtp', 'http://familycoredev.azurewebsites.net');
    //localStorage.setItem('WebApiPath', 'http://localhost:49934/api');
    localStorage.setItem('WebApiPath', 'http://familycoredev.azurewebsites.net/api');
    
  }
}
