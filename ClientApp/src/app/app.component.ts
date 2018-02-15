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
    localStorage.setItem('WebApiFtp', 'http://localhost:49934');
    localStorage.setItem('WebApiPath', 'http://localhost:49934/api');

    // localStorage.setItem('WebApiFtp', 'https://familycoredevapi.azurewebsites.net');
    // localStorage.setItem('WebApiPath', 'https://familycoredevapi.azurewebsites.net/api');
    
  }
}
